using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BasicDragonAI : MonoBehaviour
{  
    public List<Transform> PatrolPoints = new List<Transform>();
    public List<Transform> PatrolPoints1 = new List<Transform>();
    public List<Transform> PatrolPoints2 = new List<Transform>();
    public List<Transform> PatrolPoints3 = new List<Transform>();
    private int currentWaypoint = 0;
    public int waypointFollowing = 0;
    public float waypointDistance = 3f;
    public float Speed = 15f;
    public int randomNumber;
    int lastNumber;
    bool lastItem;
    public bool spottedPlayer;
    public int patrolNum;
    public float SightDistance;
    private Transform start;
    public int lastPosition;
    public bool isGroundedD;
    public Rigidbody rb;

    public UnityEngine.AI.NavMeshAgent ai;

        [HideInInspector]
    public Transform target;
    
    //To not ANY OF THESE CAN BE OVERRIDDEN. This is a template for the AI not all ai will do this. change and override what you need in the inheritied script
    //start used to set up nav mesh and set target if its null
    public void Start()
    {
        NewRandomNumber();
        ai = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //start.position = transform.position;
        rb = GetComponent<Rigidbody>();
        if(target == null)
        {
            target = PatrolPoints[0].transform;
            UpdateDestination(target.position);
        }
    }

    public void Update()
    {
        IsGrounded();
        if (patrolNum > lastPosition)
        {
            NewRandomNumber();
            patrolNum = 0;
        }
        if (randomNumber == 1)
        {
           PatrolPoints.Clear();
            PatrolPoints.AddRange(PatrolPoints1);
        }
        if (randomNumber == 2)
        {
            PatrolPoints.Clear();
            PatrolPoints.AddRange(PatrolPoints2);
        }
        if (randomNumber == 3)
        {
            PatrolPoints.Clear();
            PatrolPoints.AddRange(PatrolPoints3);
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
    private bool atDestination;
    //if the ai doesnt see the player it will patrol between all the points
    public virtual void Patrol()
    {

        // Transform wp = Waypoints[waypointFollowing];
        // if (Vector3.Distance(transform.position, wp.position) < 0.01f)
        // {
        //     transform.position = wp.position;
        //     waypointFollowing = (waypointFollowing + 1) % Waypoints.Count;
        // }
        // else
        // {
        //     transform.position = Vector3.MoveTowards(transform.position, wp.position, Speed * Time.deltaTime);
        // }

        // if (Waypoints.Count > 0){
        // Transform last = Waypoints.Last();
        //     if (wp == last && Vector3.Distance(transform.position, wp.position) < 0.01f)
        //     {
        //         NewRandomNumber();
        //     }
             
        //}

        if (GetComponent<UnityEngine.AI.NavMeshAgent>().remainingDistance < 0.5f && atDestination == false)
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
    //if the ai found the player it will run this. This follows the player until the enemy cant see them with the raycast.
    public virtual void FoundPlayer()
    {
        Debug.DrawRay(transform.position, (target.position - transform.position).normalized * SightDistance, Color.green);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, target.position - transform.position, out hit, SightDistance))
        {
            Debug.DrawLine(hit.point, hit.point + hit.normal, Color.green, 100, false);
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


    public virtual void IsGrounded()
    {
        float bufferDistance = 0.1f;
        float groundCheckDistance = (GetComponent<CapsuleCollider>().height/2)+bufferDistance;
        Debug.DrawLine(transform.position, -transform.up, Color.green, groundCheckDistance);
        RaycastHit hit;
        if(Physics.Raycast(transform.position,Vector3.down, out hit,groundCheckDistance))
        {
            Debug.Log(hit.transform.gameObject);
            isGroundedD=true;
        }
        else
        {
            isGroundedD = false;
        }
    }
   public virtual void NewRandomNumber()
{
    randomNumber = Random.Range(1, 3);
    if (randomNumber == lastNumber)
    {
        randomNumber = Random.Range(1, 3);
    }
    lastNumber = randomNumber;
}
}
