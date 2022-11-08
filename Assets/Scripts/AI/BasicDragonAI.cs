using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.AI;

[Serializable]
public class AllPatrolPoints
{
    public Transform[] WanderPoints;
}
public abstract class BasicDragonAI : BaseEnemyAI
{
    //update all arrays to lists
    public AllPatrolPoints[] AllPatrolPoints;
    //private int currentWaypoint = 0;
    public int waypointFollowing = 0;
    public float waypointDistance = 3f;
    public float Speed = 15f;
    public int randomNumber;
    int lastNumber;
    //bool lastItem;
    private Transform start;
    public int lastPosition;
    public bool isGroundedD;
    internal Rigidbody rb;
    private bool finishedPatrolling;

    public new void Start()
    {
        //base.Start();
        NewRandomNumber();
        rb = GetComponent<Rigidbody>();
        ai = this.GetComponent<NavMeshAgent>();
        foreach (Transform trans in AllPatrolPoints[randomNumber].WanderPoints)
        {
            PatrolPoints[0] = trans;
        }
        target = PatrolPoints[0];
        UpdateDestination(PatrolPoints[0].position);
    }

    public new void Update()
    {
        IsGrounded();
        if (finishedPatrolling)
        {
            finishedPatrolling = false;
            print("points cleared");
            NewRandomNumber();
            PatrolPoints = AllPatrolPoints[randomNumber].WanderPoints;
            UpdateDestination(PatrolPoints[0].position);
            lastPosition = PatrolPoints.Length - 1;
            patrolNum = 0;
        }
        base.Update();
    }

    public override void Patrol()
    {
        if (ai.enabled && ai.remainingDistance < 0.5f && atDestination == false)
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
                finishedPatrolling = true;
            }
            target = PatrolPoints[patrolNum];
            if (target != null)
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
        float groundCheckDistance = (GetComponent<CapsuleCollider>().height / 2) + bufferDistance;
        Debug.DrawLine(transform.position, -transform.up, Color.green, groundCheckDistance);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance))
        {
            Debug.Log(hit.transform.gameObject);
            isGroundedD = true;
        }
    }
    //public float groundCheckDistance;
    public virtual void IsGrounded()
    {
        float bufferDistance = 0.1f;
        float groundCheckDistance = ((GetComponent<CapsuleCollider>().height/2)+bufferDistance) * 1.5f;
        Debug.DrawRay(transform.position, -Vector3.up * (groundCheckDistance), Color.green);
        RaycastHit hit;

        if(Physics.Raycast(transform.position, -Vector3.up, out hit, groundCheckDistance))
        {
            if (hit.collider)
            {
                isGroundedD=true;
            }
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
            randomNumber = UnityEngine.Random.Range(0, AllPatrolPoints.Length);
            if (randomNumber == lastNumber)
            {
                randomNumber = UnityEngine.Random.Range(0, AllPatrolPoints.Length);
            }
            lastNumber = randomNumber;
        }
    }
}
