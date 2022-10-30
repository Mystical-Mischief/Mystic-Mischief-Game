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
    //private float Speed = 15f;
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
        base.Start();
        NewRandomNumber();
        rb = GetComponent<Rigidbody>();
        foreach(Transform trans in AllPatrolPoints[randomNumber].WanderPoints)
        {
            PatrolPoints[0] = trans;
        }
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

        if (ai.remainingDistance < 0.5f && atDestination == false)
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
            UpdateDestination(target.position);
        }
        else
        {
            atDestination = false;
        }
    }
    public virtual void IsGrounded()
    {
        float bufferDistance = 0.1f;
        float groundCheckDistance = (GetComponent<CapsuleCollider>().height/2)+bufferDistance;
        Debug.DrawLine(transform.position, -gameObject.transform.up, Color.green, groundCheckDistance);
        RaycastHit hit;

        if(Physics.Raycast(transform.position, -transform.up, out hit,groundCheckDistance))
        {
            isGroundedD=true;
        }
        else
        {
            isGroundedD = false;
        }
    }
   public virtual void NewRandomNumber()
    {
        randomNumber = UnityEngine.Random.Range(0, AllPatrolPoints.Length);
        if (randomNumber == lastNumber)
        {
            randomNumber = UnityEngine.Random.Range(0, AllPatrolPoints.Length);
        }
        lastNumber = randomNumber;
    }
}
