 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemyAI : MonoBehaviour
{
    [SerializeField]
    public List<Transform> PatrolPoints = new List<Transform>();
    internal bool spottedPlayer;
    public int patrolNum;
    public float SightDistance;
    [HideInInspector]
    public string EnemyType;
    private int Health;

    public bool isStunned;
    public GameObject Player;
    internal bool stunned;
    private bool Saved;
    [HideInInspector]
    public Vector3 targetPosition;

    internal EnemyVision EnemyVision;

    public Transform target;
    internal NavMeshAgent ai;
    
    //To note ANY OF THESE CAN BE OVERRIDDEN. This is a template for the AI not all ai will do this. change and override what you need in the inheritied script
    //start used to set up nav mesh and set target if its null
    public void Start()
    {
        EnemyVision = GetComponent<EnemyVision>();
        Saved = false;
        // controls = new ControlsforPlayer();
        ai = GetComponent<NavMeshAgent>();
        spottedPlayer = EnemyVision.PlayerDetected;
        if (target == null)
        {
            target = PatrolPoints[0].transform;
            UpdateDestination(target.position);
        }
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Update()
    {

        if (target == Player)
        {
            Player.GetComponent<ThirdPersonController>().Targeted = true;
        }
        targetPosition = target.transform.position;
        // if (Saved = false && Player.GetComponent<ThirdPersonController>().Saved == true)
        // {
        //     SaveEnemy();
        //     Saved = true;
        // }
        // if (Player.GetComponent<ThirdPersonController>().Loaded == true)
        // {
        //     Saved = false;
        //     LoadEnemy();
        // }
        //if the ai doesnt see the player then it will patrol and look for it
        if (!spottedPlayer)
        {
            EnemyDetection();
            Patrol();
        }
        //if the ai finds the player it will do what it does when it sees the player
        else
        {
            FoundPlayer();
        }
    }
    //updates where the player goes to save code
    public virtual void UpdateDestination(Vector3 newDestination)
    {
        ai.destination = newDestination;
    }
    //uses raycasts to look for the player. if the player touches the raycast the player it will update it to where the player is found
    public virtual void EnemyDetection()
    {
        EnemyVision.FieldOfView();
        spottedPlayer = EnemyVision.PlayerDetected; //checks if the ai saw the player
        if(spottedPlayer)
        {
            target = EnemyVision.LocationTarget; //the ai would move towards the player, and the target would be also the current player location
        }
    }
    internal bool atDestination;
    //if the ai doesnt see the player it will patrol between all the points
    public virtual void Patrol()
    {
        if (!stunned)
        {
            if (GetComponent<NavMeshAgent>().remainingDistance < 0.5f && atDestination == false)
            {
                atDestination = true;
                //if the player isnt at the last point
                if (patrolNum < PatrolPoints.Count - 1)
                {
                    patrolNum++;
                }
                //if the player is at the last point go to the first one
                else
                {
                    patrolNum = 0;

                }
                target = PatrolPoints[patrolNum];
                UpdateDestination(target.position);
            }
            else
            {
                atDestination = false;
            }
        }
    }
    public virtual void Stun(float time)
    {
        if (!isStunned)
        {
            float currSpeed = GetComponent<NavMeshAgent>().speed;
            GetComponent<NavMeshAgent>().speed = 0;
            StartCoroutine(stunTimer(time, currSpeed));
            isStunned = true;
        }
    }
    IEnumerator stunTimer(float time, float speed)
    {
        yield return new WaitForSeconds(time);
        print(speed);
        GetComponent<NavMeshAgent>().speed = speed;
        isStunned = false;
    }
    //if the ai found the player it will run this. This follows the player until the enemy cant see them with the raycast.
    private Vector3 PlayerDirection;
    public virtual void FoundPlayer()
    {
        EnemyVision.FieldOfView();
        spottedPlayer = EnemyVision.PlayerDetected;
        if(spottedPlayer)
        {
            UpdateDestination(target.position);
            //checks if the ai can see the player
            EnemyVision.FieldOfView();
            spottedPlayer = EnemyVision.PlayerDetected;
            //This would happen if the player goes out of range while the ai was targeting the player
            if(!spottedPlayer)
            {
                LostPlayer();
            }
        }
        else
        {
            LostPlayer();
        }
    }
    //this runs when the player is lost by the ai. some basic logic.
    public virtual void LostPlayer()
    {
        spottedPlayer = false;
        target = PatrolPoints[0];
    }
    public void SaveEnemy ()
    {
        SaveSystem.SaveEnemy(this);
        Debug.Log("Saved");
    }
    public void LoadEnemy ()
    {
        EnemyData data = SaveSystem.LoadEnemy(this);
        patrolNum = data.patrolNum;
        Patrol();

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
        spottedPlayer = data.spottedPlayer;
    }

}
