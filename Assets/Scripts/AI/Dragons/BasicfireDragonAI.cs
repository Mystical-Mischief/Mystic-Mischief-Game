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
    public bool airToGround;
    public bool groundToAir;
    float dist;
    // public Animator anim;

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
        //Sets the player to targeted so they dont save.
        if (target == Player)
        {
            Player.GetComponent<ThirdPersonController>().Targeted = true;
            // anim.SetBool("ChasePlayer", true);
        }
        // else
        // {
        //     // anim.SetBool("ChasePlayer", false);
        // }

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
        //If the enemy detection works the dragon chases the player.
        if (base.spottedPlayer == true)
        {
            FoundPlayer();
        }
        //If the Dragon is at the last spot it will generate a new path to follow based on the random number generator.
        if (finishedPatrolling)
        {
            print("points cleared");
            NewRandomNumber();
            NewPath();
            if (onGround == true)
            {
                UpdateDestination(base.PatrolPoints[0].position);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, base.PatrolPoints[0].position, Speed * Time.deltaTime);
            }
            lastPosition = base.PatrolPoints.Count - 1;
            patrolNum = 0;
            finishedPatrolling = false;
        }
    }
    void FixedUpdate()
    {
        IsGrounded();
        //If the bool is true the dragon goes to the air and patrols. Disables the NavMesh Agent.
        if (groundToAir == true)
        {
            onGround = false;
            inAir = true;
            atDestination = false;
            NewRandomNumber();
            NewPath();
            // groundToAir = false;
        }
        //If the bool is set to true it goes to the first patrol point and patrols on the ground.
        if (airToGround == true)
        {
            GoToGround();
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


    //This generates a new path that chooses from a list of on ground or in air patrol points from a random number generator.
    public void NewPath()
    {
        patrolNum = 0;
        if (inAir == false && onGround == true)
        {
            base.PatrolPoints.Clear();
            base.PatrolPoints.AddRange(WanderPointsGround[randomNumber].WanderPoints);
        }
        if (inAir == true && onGround == false)
        {
            base.PatrolPoints.Clear();
            base.PatrolPoints.AddRange(WanderPointsAir[randomNumber].WanderPointsInAir);    
        }
        target = PatrolPoints[patrolNum];
        groundToAir = false;
    }

    //This is for patroling on the ground.
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
    
    //This is for patroling in the air. The gravity and NavMesh Agent for the gameobject are disabled.
    public void PatrolAir()
    {
        rb.useGravity = false;
        base.ai.enabled = false;
        dist = Vector3.Distance(target.position, transform.position);
        transform.position = Vector3.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);
        if (dist < 1f && atDestination == false)
        {
            atDestination = true;
            //if the player isnt at the last point
            if (patrolNum <=  PatrolPoints.Count - 1)
            {
                patrolNum++;
                print(patrolNum);
                target = PatrolPoints[patrolNum];
            }
            //if the player is at the last point go to the first one
            if(patrolNum == PatrolPoints.Count - 1)
            {
                // patrolNum = 0;
                finishedPatrolling = true;
            }
            if (target != null)
            {
                // atDestination = false;
                // transform.position = Vector3.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);
            }
        }
        else
        {
            atDestination = false;
        }
    }

    //If true, it sends the player to the first patrol point and disables the air features. It renables what it needs for patroling on the ground.
    public void GoToGround()
    {
        // float dist = Vector3.Distance(base.target.position, transform.position);

        // Transform startGround = WanderPointsGround[1].WanderPoints[1].position; WanderPointsGround[0].WanderPoints[0].transform.position
        inAir = false;
        // transform.position = Vector3.MoveTowards(transform.position, WanderPointsAir[0].WanderPointsInAir[0].transform.position, Speed * Time.deltaTime);
        // Debug.Log(Vector3.Distance(WanderPointsAir[0].WanderPointsInAir[0].transform.position, transform.position));
        // if (Vector3.Distance(WanderPointsAir[0].WanderPointsInAir[0].transform.position, transform.position) < 0.5f)
        // {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 2.9f, transform.position.z), Speed * Time.deltaTime);
        if (transform.position.y == 2.9f)
        {
            // inAir = false;
            rb.useGravity = true;
            base.ai.enabled = true;
            onGround = true;
            NewRandomNumber();
            airToGround = false;
        }
        // }
    }
    
    //This is a timer for when it loses the player. When the timer meets the lost time it goes back to patroling.
    public override void LostPlayer()
    {
        lostPlayerTime += Time.deltaTime;
    }

    //This resets the target and timer to 0.
    public void ResetTarget()
    {
        spottedPlayer = false;
        target = PatrolPoints[0];
        lostPlayerTime = 0;
    }

    //This just stops the player moving.
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
    
    //This is kept empty so that the stun function from base enemy ai is not used. KEEP IT EMPTY.
    public override void Stun(float time)
    {

    }


    //This is the ground check function similar to the playercontrollers.
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

    //This is the random number generator. This is used for selecting patrol points at random.
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
