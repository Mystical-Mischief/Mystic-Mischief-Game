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
    private float previousSpeed;
    private float previousAISpeed;
    public bool canMove;
    private float lostPlayerTime;
    public float chaseTime;

    public new void Start()
    {
        // onGround = true;
        //base.Start();
        NewRandomNumber();
        rb = GetComponent<Rigidbody>();
        ai = this.GetComponent<NavMeshAgent>();
        base.Start();
        NewPath();
    }

    public new void Update()
    {

        //if the ai doesnt see the player then it will patrol and look for it
        if (!base.spottedPlayer)
        {
            // EnemyDetection();
            if(canMove == true && onGround == true && inAir == false)
            {
                base.EnemyDetection();
                Patrol();
            }

            if (inAir == true && onGround == false)
            {
                base.EnemyDetection();
                PatrolAir();
            }
        }
        if (base.spottedPlayer == true)
        {
            FoundPlayer();
        }
        IsGrounded();
        if (finishedPatrolling)
        {
            print("points cleared");
            NewRandomNumber();
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
        if (isGroundedD == true)
        {
            
        }

        if (lostPlayerTime == chaseTime)
        {
            ResetTarget();
        }
    }

    public override void FoundPlayer()
    {
        EnemyVision.FieldOfView();
        spottedPlayer = EnemyVision.PlayerDetected;
        if(spottedPlayer)
        {
            target = Player.transform;
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
                // patrolNum = 0;
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

    public void NewPath()
    {
        if (randomNumber == 1 && inAir == false)
        {
            base.PatrolPoints.Clear();
            base.PatrolPoints.AddRange(WanderPointsGround[randomNumber].WanderPoints);
        }
        else if (randomNumber == 1 && inAir == false && onGround == true)
        {
            base.PatrolPoints.Clear();
            base.PatrolPoints.AddRange(WanderPointsAir[randomNumber].WanderPointsInAir);
        }

        if (randomNumber == 2 && inAir == false)
        {
            base.PatrolPoints.Clear();
            base.PatrolPoints.AddRange(WanderPointsGround[randomNumber].WanderPoints);
        }
        else if (randomNumber == 2 && inAir == false && onGround == true)
        {
            base.PatrolPoints.Clear();
            base.PatrolPoints.AddRange(WanderPointsAir[randomNumber].WanderPointsInAir);
        } 

        if (randomNumber == 3 && inAir == false)
        {
            base.PatrolPoints.Clear();
            base.PatrolPoints.AddRange(WanderPointsGround[randomNumber].WanderPoints);
        }
        else if (randomNumber == 3 && inAir == false && onGround == true)
        {
            base.PatrolPoints.Clear();
            base.PatrolPoints.AddRange(WanderPointsAir[randomNumber].WanderPointsInAir);
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
                // patrolNum = 0;
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
        transform.position = Vector3.MoveTowards(transform.position, WanderPointsGround[0].WanderPoints[0].transform.position, Speed * Time.deltaTime);
        if (dist < 1f)
        {
            inAir = false;
            rb.useGravity = true;
            base.ai.enabled = true;
            onGround = true;
            NewRandomNumber();
        }
    }
    
    public override void LostPlayer()
    {
        lostPlayerTime += Time.deltaTime;
    }

    public void ResetTarget()
    {
        spottedPlayer = false;
        target = PatrolPoints[0];
        lostPlayerTime = 0;
    }

    public void StopMoving()
    {
        previousSpeed = Speed;
        previousAISpeed = ai.speed;
        Speed = 0;
        ai.speed = 0;
    }

    public void StartMoving()
    {
        Speed = previousSpeed;
        ai.speed = previousAISpeed;
    }
    
    public override void Stun(float time)
    {

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
