 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemyAI : MonoBehaviour
{
    public Transform[] PatrolPoints;
    public bool spottedPlayer;
    [SerializeField]
    internal Transform[] PatrolPoints = new Transform[10];
    internal bool spottedPlayer;
    public int patrolNum;
    public float SightDistance;
    public string EnemyType;
    private int Health;
    // ControlsforPlayer controls;
    public GameObject Player;
    private bool Saved;
    public Vector3 targetPosition;

    public Transform target;
    internal NavMeshAgent ai;
    
    //To note ANY OF THESE CAN BE OVERRIDDEN. This is a template for the AI not all ai will do this. change and override what you need in the inheritied script
    //start used to set up nav mesh and set target if its null
    public void Start()
    {
        Saved = false;
        // controls = new ControlsforPlayer();
        ai = GetComponent<NavMeshAgent>();

        if(target == null)
        {
            target = PatrolPoints[0].transform;
            UpdateDestination(target.position);
        }
    }

    public void Update()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * SightDistance, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, SightDistance))
        {
            if (hit.transform.gameObject.tag == "Player")
            {
                spottedPlayer = true;
                target = hit.transform;

            }
        }
        if (target == Player)
        {
            Player.GetComponent<ThirdPersonController>().Targeted = true;
        }
        targetPosition = target.transform.position;
        if (Saved = false && Player.GetComponent<ThirdPersonController>().Saved == true)
        {
            SaveEnemy();
            Saved = true;
        }
        if (Player.GetComponent<ThirdPersonController>().Loaded == true)
        {
            Saved = false;
            LoadEnemy();
        }
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
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * SightDistance, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, SightDistance))
        {
            if (hit.transform.gameObject.tag == "Player")
            {
                spottedPlayer = true;
                target = hit.transform;
                
            }
        }
    }
    internal bool atDestination;
    //if the ai doesnt see the player it will patrol between all the points
    public virtual void Patrol()
    {
        if (GetComponent<NavMeshAgent>().remainingDistance < 0.5f && atDestination == false)
        {
            atDestination = true;
            //if the player isnt at the last point
            if (patrolNum < PatrolPoints.Length - 1)
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
    //if the ai found the player it will run this. This follows the player until the enemy cant see them with the raycast.
    private Vector3 PlayerDirection;
    public virtual void FoundPlayer()
    {
        PlayerDirection = target.transform.position - transform.position;
        Debug.DrawRay(transform.position, (PlayerDirection).normalized * SightDistance, Color.green);
        RaycastHit hit;
        
        
        if (Physics.Raycast(transform.position, PlayerDirection, out hit, SightDistance))
        {
            UpdateDestination(target.position);
            //if the ai cant see the player
            if (hit.transform.gameObject.tag != "Player")
            {
                LostPlayer();
            }
        }
        //if the ai cant see the player
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

    //     private void OnEnable()
    // {
    //     controls.Enable();
    // }
    // private void OnDisable()
    // {
    //     controls.Disable();
    // }

    public void SaveEnemy ()
    {
        SaveSystem.SaveEnemy(this);
        Debug.Log("Saved");
    }
    public void LoadEnemy ()
    {
        EnemyData data = SaveSystem.LoadEnemy(this);
        patrolNum = data.patrolNum;
        target = PatrolPoints[patrolNum];
        UpdateDestination(target.position);

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
        spottedPlayer = data.spottedPlayer;
    }

}
