using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaterDragonAi : BasicDragonAI
{
    //private CapsuleCollider Ranged;
    private SphereCollider jump;
    //private BoxCollider Melee;
    public Rigidbody  projectile;
    private bool ranged;
    private Vector3 PlayerPos;
    public float timeBetweenAttacks;
    // public float attackRange;
    public bool detectedPlayer;
    public Transform[] Enemy;
    public GameObject attackPos;
    // public LayerMask whatIsEnemy;
    private int speed = 5;
    public float waterSpeed;
    public int attackTimes;
    public float jumpAttackHieght;
    [HideInInspector]
    public Vector3 jumpHieght;
    private float jumpForce = 10f;
    private Vector3 forceDirection = Vector3.zero;
    private float bo;
    [HideInInspector]
    private new bool FoundPlayer;
    [HideInInspector]
    public Transform Base;
    [HideInInspector]
    public float randomWaitTime;
    float lastWaitTime;
    public bool attacked;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private int state;
    private bool WaterChase;
    public float jumpDist;
    public bool inWater;
    public bool Jumping;
    public Animator anim;

    [HideInInspector]
    public bool rangedAttacked;
    public bool detectForGround = true;
    public bool CanJump;

    public bool HitPlayer;
    [HideInInspector]
    public bool meleeAttack;
    public bool Jumped;
    public float rangedDist;
    public float meleeDist;
    public float heightDist;
    public float chaseTime;
    public float chaseWaterTimer;
    public float projectileSpeed;
    public float ResetAttackTime;
    public int ResetJumpTime;
    public bool CanAttack;

    private UnityEngine.AI.NavMeshAgent ai2;

    // Start is called before the first frame update
    new void Start()
    {
        Jumping = false;
        ai2 = GetComponent<UnityEngine.AI.NavMeshAgent>();
        state = 0;
        RandomNumber();
        state = 0;
        NewRandomNumber();
        HitPlayer = false;
        attackTimes = 0;
        attacked = false;
        meleeAttack = false;
        // anim = GetComponent<Animator>();
        base.Start();
        CanAttack = true;
        CanJump = true;
    }

    // Update is called once per frame
    new void Update()
    {
        if (isGroundedD == true)
        {
            anim.SetBool("Grounded", true);
        }
        else {anim.SetBool("Grounded", false);}

        PlayerPos.x = Player.transform.position.x;
        PlayerPos.z = Player.transform.position.z;
        PlayerPos.y = transform.position.y;
        base.Update();
        float dist = Vector3.Distance(base.Player.transform.position, transform.position);
        // Debug.Log(dist);

        // This will make the dragon jump if the player is higher than jumpDist and in the chaseWaterDistance.
        if (Player.transform.position.y > transform.position.y && dist <= jumpDist)
        {
            if (gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled == true && Jumped == false)
            {
                        base.ai.enabled = false;
                Jumped = true;
                Jump();
            }
        }

        // This will make the dragon jump if the player is higher than jumpDist and in the JumpDistance.
        // if (CanJump == false && CanAttack == false && dist > rangedDist)
        // {
        //     transform.LookAt(base.target);
        // }
        if (CanJump == true)
        {
            // Jumped = false;
        }
        //If the player is in jump distance which should be the farthest distance and is lower than the dragon
        //       if (Player.transform.position.y < (transform.position.y + jumpDist) && dist <= chaseWaterDistance && dist > meleeDist)
        // {
        //     if (gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled == true && Jumped == false)
        //     {
        //         // ai.speed = 0;
        //         anim.SetTrigger("Scream");
        //         // Invoke(nameof(ResetAttack), 1f);
                
        //     }

        if (attacked == false)
        {
            randomWaitTime -= Time.deltaTime;
        }
        if (randomWaitTime <= 0 &&  isRotatingLeft==false)
        {
            state = 3;
        }
        if (Jumping == false)
        {
        IsGrounded();
        }

        if (state == 2) {
        transform.Rotate(new Vector3(0,10,0));
        state = state-1;
    } else if (state == 1) {
        transform.Rotate(new Vector3(0,-10,0));
        state = state-1;
    } else { // state is zero
        transform.Rotate(new Vector3(0,0,0));
    }
        // If the player moves to fast and is close enough to the dragon it has found the player.
        if (Player.GetComponent<ThirdPersonController>().rbSpeed >= 9 && dist < 17f)
        {
            FoundPlayer = true;
        }
        else if (dist > 17f)
        {
            FoundPlayer = false;
        }

        // if  (FoundPlayer == true && attackTimes < 4)
        // {
        //     base.ai.enabled = false;
        // }
        // else if (FoundPlayer == false)
        // {
        //     base.ai.enabled = true;
        // }
        // if (FoundPlayer == true && attackTimes >= 5)
        // {
        //     base.ai.enabled = true;
        // }


        // If the dragon has not jumped and is less than the rangedDist away than it looks at the player and does the Ranged attack function.
         if (!Jumped && dist > meleeDist && dist <= rangedDist && attackTimes < 5)
         {
            if (CanAttack == true)
            {
            transform.LookAt(PlayerPos);
            if (rangedAttacked == false && WaterChase == false && attacked == false)
            {Ranged();}
            ///attackTimes = attackTimes + 1;
            //Debug.Log("Ranged");
            }
         }
         // If the player was hit by a melee attack (in the WDAttackScript) Then it resets everything and goes back to patrolling.
        if (HitPlayer == true)
        {
            attackTimes = 0;
            base.target = base.PatrolPoints[0].transform;
            UpdateDestination(base.target.position);
            Invoke(nameof(ResetAttack2), 5f);
        }
        //If the dragon has done 5 ranged attacks than it chases the player.
        if (attackTimes >= 5)
        {
            ChasePlayer();
        }
        // If it is close to the dragon it sets the attack to true.
         if (dist > 2f && dist < meleeDist)
         {
            //transform.LookAt(PlayerPos);
            if (meleeAttack == false)
            {
            attackPos.SetActive(true);
            meleeAttack = true;
            attacked = true;
            }
         }
         if (Player.GetComponent<ThirdPersonController>().inWater == true && dist <= rangedDist)
         {
            ChasePlayerWater();
         }
         // If the dragon can do a ranged attack and it is in the water than it plays an animation for the HeadAboveWater.
         if (inWater == true && dist <= rangedDist && dist > meleeDist)
         {
            anim.SetBool("HeadAboveWater", true);
         }
         else {anim.SetBool("HeadAboveWater", false);}
        if (isGroundedD == true)
        {
            // Jumped = false;
            // if (CanJump == false)
            // {
            //     Invoke(nameof(ResetJump), ResetJumpTime);
            // }
        }
    }
    void Jump()
    {
        anim.SetTrigger("Jump");
        if (detectForGround == true){
        detectForGround = false;
        Vector3 jumpVec = Player.transform.position - transform.position;
        Vector3 JumpDistance = new Vector3 (jumpVec.x, jumpDist, jumpVec.z);
        //print(jumpVec);
        // rb.AddForce((jumpVec * 100) * jumpAttackHieght, ForceMode.Impulse);
        rb.AddForce(JumpDistance * jumpAttackHieght, ForceMode.Impulse);
        StartCoroutine(jumpTimer());
        Jumping = true;
        // Invoke(nameof(ResetJump), ResetJumpTime);
        }
    }
    // public void ResetJump1() { Invoke(nameof(ResetJump), ResetJumpTime); }
    // public void ResetJump()
    // {
    //     Jumping = false;
    // }
    //This is for chasing the player on land.
    void ChasePlayer()
    {
        target = Player.transform;
        UpdateDestination(target.position); 
        Invoke(nameof(ResetAttack2), chaseTime); 
    }
    //This is for chasing the plyer in water.
        void ChasePlayerWater()
    {
        if (inWater == true){
        WaterChase = true;
        target = Player.transform;
        UpdateDestination(target.position);
        Invoke(nameof(ResetAttack2), chaseWaterTimer); 
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if (other.gameObject.tag == "Player" && meleeAttack == true)
        // {  
        //     Debug.Log("DAMAGED!");
        //     meleeAttack = false;
        //     Invoke(nameof(ResetAttack2), 0f);
        // }

        //This raises the speed of the dragon when it is in water and sets the animation bool for when it is in water.
        if(other.gameObject.CompareTag("Water")){
                base.ai.speed = waterSpeed;
                inWater = true;
                anim.SetBool("InWater", true);
        }

    }
    //This resets the water variables when it leaves the water.
    void OnCollisionExit(Collision other)
    {
            if(other.gameObject.CompareTag("Water")){
                base.ai.speed = 3.5f;
                base.Speed = 3.5f;
                anim.SetBool("InWater", false);
        } 
    }
    void Ranged()
    {
        anim.SetTrigger("Spit");
        Rigidbody clone;
        Vector3 jumpVec = Player.transform.position - transform.position;
        clone = Instantiate(projectile, transform.position, Player.transform.rotation);
        base.ai.speed = 0;
        //projectile.LookAt(Player.transform);
        var step =  speed * Time.deltaTime; // calculate distance to move

        clone.velocity = (Player.transform.position - clone.position).normalized * projectileSpeed;
        Invoke(nameof(ResetAttack), 1f);
        rangedAttacked = true;
        attacked = true;
    }

    //This is for reseting the ranged attack and it rasies the attack counter.
    public virtual void ResetAttack()
    {
        rangedAttacked = false;
        attacked = false;
        attackTimes += 1;
        base.ai.speed = Speed;
    }

    public override void IsGrounded()
    {
        base.IsGrounded();
        if (isGroundedD == true)
        {
            Jumped = false;
            print("Touched Ground");
            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
        }
    }
    //This is for reseting the melee attack.
    public virtual void ResetAttack2()
    {
        Invoke(nameof(ResetAttacks), ResetAttackTime);
        CanAttack = false;
        meleeAttack = false;
        attacked = false;
        attackPos.SetActive(false);
        attackTimes = 0;
        HitPlayer = false;
        NewRandomNumber();
            base.target = base.PatrolPoints[0].transform;
            UpdateDestination(base.target.position);
        
    }
    //This is a timer to not let the dragon instantly chase the player.
    public virtual void ResetAttacks()
    {
        CanAttack = true;
    }
    public virtual void RandomNumber()
    {
        randomWaitTime = Random.Range(1, 3);
        if (randomWaitTime == lastWaitTime)
        {
            randomWaitTime = Random.Range(1, 3);
        }
        lastWaitTime = randomWaitTime;
    }
    IEnumerator jumpTimer()
    {
        yield return new WaitForSeconds(ResetJumpTime);
        detectForGround = true;
        Jumping = false;
    }
    //     IEnumerator jumpReseterTimer()
    // {
    //     yield return new WaitForSeconds(10);
    //     Jumping = false;
    // }
}
