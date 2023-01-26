using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.AI;

[Serializable]
public class AllPatrolPointsGround
{
    public List<Transform> WanderPoints = new List<Transform>();
}

[Serializable]
public class AllPatrolPointsAir
{
    public List<Transform> WanderPointsInAir = new List<Transform>();
}
public abstract class BasicfireDragonAI : BaseEnemyAI
{
    //update all arrays to lists
    public List<AllPatrolPointsGround> WanderPointsGround = new List<AllPatrolPointsGround>();
    public List<AllPatrolPointsAir> WanderPointsAir = new List<AllPatrolPointsAir>();
    // public List<Transform> PatrolPoints1 = new List<Transform>();
    // public List<Transform> PatrolPoints2 = new List<Transform>();
    // public List<Transform> PatrolPoints3 = new List<Transform>();
    private int currentWaypoint = 0;
    public int waypointFollowing = 0;
    public float waypointDistance = 3f;
    public float Speed = 15f;
    public int randomNumber;
    int lastNumber;
    private Transform start;
    private int lastPosition;
    public bool isGroundedD;
    internal Rigidbody rb;
    private bool finishedPatrolling;
    public bool inAir;
    public bool onGround;

    public new void Start()
    {
        // onGround = true;
        //base.Start();
        NewRandomNumber();
        rb = GetComponent<Rigidbody>();
        ai = this.GetComponent<NavMeshAgent>();
        base.Start();
        // if(target == null)
        // {
        //     target = base.PatrolPoints[0].transform;
        //     UpdateDestination(target.position);
        // }
    }

    public new void Update()
    {
        // if (target == Player)
        // {
        //     Player.GetComponent<ThirdPersonController>().Targeted = true;
        // }
        // targetPosition = target.transform.position;

        if (randomNumber == 1)
        {
            base.PatrolPoints.Clear();
            base.PatrolPoints.AddRange(WanderPointsGround[randomNumber].WanderPoints);
        }
        if (randomNumber == 2)
        {
            base.PatrolPoints.Clear();
            base.PatrolPoints.AddRange(WanderPointsGround[randomNumber].WanderPoints);
        }      
        if (randomNumber == 3)
        {
            base.PatrolPoints.Clear();
            base.PatrolPoints.AddRange(WanderPointsGround[randomNumber].WanderPoints);
        }

        //if the ai doesnt see the player then it will patrol and look for it
        if (!spottedPlayer)
        {
            EnemyDetection();
            if (onGround == true)
            {
            Patrol();
            }
            if (inAir == true)
            {
                PatrolAir();
            }
        }
        IsGrounded();
        if (finishedPatrolling)
        {
            print("points cleared");
            // base.PatrolPoints.Clear();
            NewRandomNumber();
            // PatrolPoints = AllPatrolPoints[randomNumber].WanderPoints;
            UpdateDestination(base.PatrolPoints[0].position);
            lastPosition = base.PatrolPoints.Count - 1;
            patrolNum = 0;
            finishedPatrolling = false;
        }
        if (onGround == true && inAir == true)
        {
            GoToGround();
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
    public void PatrolAir()
    {
        float dist = Vector3.Distance(base.target.position, transform.position);
        rb.useGravity = false;
        base.ai.enabled = false;
        transform.position = Vector3.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);
        if (dist < 0.5f && atDestination == false)
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
                {

                }
        }
        else
        {
            atDestination = false;
        }
    }
    public void GoToGround()
    {
        float dist = Vector3.Distance(base.target.position, transform.position);

        // Transform startGround = WanderPointsGround[1].WanderPoints[1].position;
        transform.position = Vector3.MoveTowards(transform.position, WanderPointsGround[1].WanderPoints[1].transform.position, Speed * Time.deltaTime);
        if (dist < 0.5f && atDestination == false)
        {
            inAir = false;
            rb.useGravity = true;
            base.ai.enabled = true;
            onGround = true;
            NewRandomNumber();
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
}
