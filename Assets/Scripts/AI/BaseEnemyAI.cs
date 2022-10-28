using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemyAI : MonoBehaviour
{
    public Transform[] PatrolPoints;
    private Vector3 PlayerDirection;
    public bool spottedPlayer;
    public int patrolNum;
    public float SightDistance;
    public string EnemyType;

    private Transform target;
    private NavMeshAgent ai;
    
    //To note ANY OF THESE CAN BE OVERRIDDEN. This is a template for the AI not all ai will do this. change and override what you need in the inheritied script
    //start used to set up nav mesh and set target if its null
    public void Start()
    {
        ai = GetComponent<NavMeshAgent>();

        if(target == null)
        {
            target = PatrolPoints[0].transform;
            UpdateDestination(target.position);
        }
    }

    public void Update()
    {
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
    private bool atDestination;
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
}
