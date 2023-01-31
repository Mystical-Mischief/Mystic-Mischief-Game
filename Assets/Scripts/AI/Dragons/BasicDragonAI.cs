using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.AI;

// [Serializable]
// public class AllPatrolPoints
// {
//     public Transform[] WanderPoints;
// }
public abstract class BasicDragonAI : BaseEnemyAI
{
    //update all arrays to lists
    // public List<Transform> base.PatrolPoints = new List<Transform>();
    public List<Transform> PatrolPoints1 = new List<Transform>();
    public List<Transform> PatrolPoints2 = new List<Transform>();
    public List<Transform> PatrolPoints3 = new List<Transform>();
    private int currentWaypoint = 0;
    public int waypointFollowing = 0;
    public float waypointDistance = 3f;
    public float Speed = 15f;
    public int randomNumber;
    int lastNumber;
    //bool lastItem;
    private Transform start;
    private int lastPosition;
    public bool isGroundedD;
    internal Rigidbody rb;
    private bool finishedPatrolling;

    public new void Start()
    {
        //base.Start();
        NewRandomNumber();
        rb = GetComponent<Rigidbody>();
        ai = this.GetComponent<NavMeshAgent>();
        // if(target == null)
        // {
        //     target = base.PatrolPoints[0].transform;
        //     UpdateDestination(target.position);
        // }
        base.Start();
    }

    public new void Update()
    {
        // if (patrolNum > lastPosition)
        // {
        //     NewRandomNumber();
        //     patrolNum = 0;
        // }
        if (randomNumber == 1)
        {
           base.PatrolPoints.Clear();
            base.PatrolPoints.AddRange(PatrolPoints1);
        }
        if (randomNumber == 2)
        {
            base.PatrolPoints.Clear();
            base.PatrolPoints.AddRange(PatrolPoints2);
        }
        if (randomNumber == 3)
        {
            base.PatrolPoints.Clear();
            base.PatrolPoints.AddRange(PatrolPoints3);
        }
        //if the ai doesnt see the player then it will patrol and look for it
        if (!spottedPlayer)
        {
            EnemyDetection();
            Patrol();
        }
        IsGrounded();
        if (finishedPatrolling)
        {
            print("points cleared");
            NewRandomNumber();
            // PatrolPoints = AllPatrolPoints[randomNumber].WanderPoints;
            UpdateDestination(base.PatrolPoints[0].position);
            lastPosition = base.PatrolPoints.Count - 1;
            patrolNum = 0;
            finishedPatrolling = false;
        }
        base.Update();
    }

    public override void Patrol()
    {
        if (ai.enabled && ai.remainingDistance < 0.5f && atDestination == false)
        {
            atDestination = true;
            //if the player isnt at the last point
            if (patrolNum < base.PatrolPoints.Count - 1)
            {
                patrolNum++;
            }
            //if the player is at the last point go to the first one
            else
            {
                patrolNum = 0;
                finishedPatrolling = true;
            }
            target = base.PatrolPoints[patrolNum];
            if (target != null)
                UpdateDestination(target.position);
        }
        else
        {
            atDestination = false;
        }
    }
    //if the ai found the player it will run this. This follows the player until the enemy cant see them with the raycast.
    public override void FoundPlayer()
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
        randomNumber = UnityEngine.Random.Range(1, 3);
        if (randomNumber == lastNumber)
        {
            randomNumber = UnityEngine.Random.Range(1, 3);
        }
        lastNumber = randomNumber;
    }
    public virtual bool PlayerDetect()
    {
        return spottedPlayer;
    }
}
