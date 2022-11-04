using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaterDragonAi : BasicDragonAI
{
    //private CapsuleCollider Ranged;
    private SphereCollider jump;
    //private BoxCollider Melee;
    public GameObject Player;
    public Rigidbody  projectile;
    private bool ranged;
    private Vector3 PlayerPos;
    public float timeBetweenAttacks;
    public float attackRange;
    public bool detectedPlayer;
    public Transform[] Enemy;
    public GameObject attackPos;
    public LayerMask whatIsEnemy;
    private int speed = 5;
    public int attackTimes;
    public float jumpAttackHieght;
    public Vector3 jumpHieght;
    private float jumpForce = 10f;
    private Vector3 forceDirection = Vector3.zero;
    private float bo;
    //private bool FoundPlayer;
    public Transform Base;
    public float randomWaitTime;
    float lastWaitTime;
    public bool attacked;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private int state;
    private bool detectForGround = true;

    [HideInInspector]
    public bool HitPlayer;
    [HideInInspector]
    public bool meleeAttack;
    public bool Jumped;

    // Start is called before the first frame update
    new void Start()
    {
        state = 0;
        NewRandomNumber();
        HitPlayer = false;
        attackTimes = 0;
        attacked = false;
        meleeAttack = false;
        base.Start();
        //Ranged = gameObject.GetComponent<CapsuleCollider>();
        jump = gameObject.GetComponent<SphereCollider>();
        bo = GetComponent<UnityEngine.AI.NavMeshAgent>().baseOffset;
        //Melee = gameObject.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    new void Update()
    {
        forceDirection += new Vector3(0,2,0) * jumpForce;
        PlayerPos.x = Player.transform.position.x;
        PlayerPos.z = Player.transform.position.z;
        PlayerPos.y = 0;
        base.Update();
        float dist = Vector3.Distance(Player.transform.position, transform.position);
        //Debug.Log(dist);
        if (detectedPlayer && Player.transform.position.y > transform.position.y)
        {
            if (Jumped == false && this.ai.enabled == true && isGroundedD)
            {
                Jumped = true;
                Jump();
            }
        }

        if (attacked == false)
        {
            randomWaitTime -= Time.deltaTime;
        }
        if (randomWaitTime <= 0 &&  isRotatingLeft==false)
        {
            state = 3;
        }

        if (state == 2) 
        {
            transform.Rotate(new Vector3(0,10,0));
            state = state-1;
        } 
        else if (state == 1) 
        {
            transform.Rotate(new Vector3(0,-10,0));
            state = state-1;
        } 
        else 
        { // state is zero
            transform.Rotate(new Vector3(0,0,0));
        }

        if (dist < 17f)
        {
            detectedPlayer = true;
            target = Player.transform;
        }
        else if (dist > 17f)
        {
            detectedPlayer = false;
        }

        // if  (FoundPlayer == true && attackTimes < 4)
        // {
        //     if (Jumped == false)
        //     {
        //         Jumped = true;
        //         //Jump();
        //         //Jumped = true;
        //     }
        // }

        // if (Jumped == true)
        // {
        //         if (base.ai.enabled)
        //     {
        //         // set the agents target to where you are before the jump
        //         // this stops her before she jumps. Alternatively, you could
        //         // cache this value, and set it again once the jump is complete
        //         // to continue the original move
        //         base.ai.SetDestination(Player.transform.position);
        //         // disable the agent
        //         base.ai.updatePosition = false;
        //         base.ai.updateRotation = false;
        //         base.ai.isStopped = true;
        //     }

        //     // make the jump
        //     base.PatrolPoints = null;
        //     Debug.Log("Jump");
        //     GetComponent<Rigidbody>().isKinematic = false;
        //     GetComponent<Rigidbody>().useGravity = true;
        //     GetComponent<UnityEngine.AI.NavMeshAgent>().baseOffset = -5;
        //     transform.position = Vector3.MoveTowards(transform.position, (transform.position + new Vector3(0f, 1f, 1f)), speed * Time.deltaTime);
        //     Invoke(nameof(ResetAttack3), 1f);
        //     Jumped = false;
        // }
         if (!Jumped && dist > 12f && dist <17f && attackTimes < 5)
         {
            target = Player.transform;
            transform.LookAt(PlayerPos);
            if (attacked == false)
            {Ranged();}
            ///attackTimes = attackTimes + 1;
            //Debug.Log("Ranged");
         }
        // if (HitPlayer == true)
        // {
        //     Invoke(nameof(ResetAttack2), 0f);
        // }
        if (attackTimes >= 5)
        {
            target = Player.transform;
            UpdateDestination(target.position);
        }
         if (dist > 2f && dist <12f)
         {
            //transform.LookAt(PlayerPos);
            if (meleeAttack == false)
            {
                attackPos.SetActive(true);
                meleeAttack = true;
                attacked = true;
            }
         }

    }
    void Jump()
    {
        detectForGround = false;
        ai.enabled = false;
        Vector3 jumpVec = Player.transform.position - transform.position;
        print(jumpVec);
        rb.AddForce(jumpVec.normalized * jumpAttackHieght, ForceMode.Impulse);
        rb.AddForce(Vector3.up * jumpAttackHieght, ForceMode.Impulse);
        StartCoroutine(jumpTimer());
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && meleeAttack == true)
        {  
            Debug.Log("DAMAGED!");
            meleeAttack = false;
            Invoke(nameof(ResetAttack2), 0f);
        }

    }
    void Ranged()
    {
        Rigidbody clone;
        clone = Instantiate(projectile, transform.position, Player.transform.rotation);
        //projectile.LookAt(Player.transform);

        clone.velocity = transform.TransformDirection(Vector3.forward * 10);
        Invoke(nameof(ResetAttack), 1f);
        attacked = true;
    }

   //void resetJump()
   //{
   //    if(base.isGroundedD == false){
   //    transform.position = Vector3.MoveTowards(transform.position, (transform.position - new Vector3(0f, 2f, 0f) + Vector3.forward), speed * Time.deltaTime);
   //    }
   //}
    // void Jump()
    // {
    //             if (base.ai.enabled)
    //         {
    //             // set the agents target to where you are before the jump
    //             // this stops her before she jumps. Alternatively, you could
    //             // cache this value, and set it again once the jump is complete
    //             // to continue the original move
    //             base.ai.SetDestination(Player.transform.position);
    //             // disable the agent
    //             base.ai.updatePosition = false;
    //             base.ai.updateRotation = false;
    //             base.ai.isStopped = true;
    //         }

    //         // make the jump
    //         base.PatrolPoints = null;
    //         Debug.Log("Jump");
    //         GetComponent<Rigidbody>().isKinematic = false;
    //         GetComponent<Rigidbody>().useGravity = true;
    //         GetComponent<UnityEngine.AI.NavMeshAgent>().baseOffset = -5;
    //         transform.position = Vector3.MoveTowards(transform.position, (transform.position + Vector3.up), speed * Time.deltaTime);
            
    // }


    // void Melee()
    // {
    //     float step =  speed * Time.deltaTime;
    //     transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, step);
    //     Collider[] hitObjects = Physics.OverlapSphere(attackPos.position, attackRange, whatIsEnemy);

    public virtual void ResetAttack()
    {
        attacked = false;
        attackTimes += 1;
    }
    public override void IsGrounded()
    {
        if (detectForGround)
        {
            base.IsGrounded();
            if (isGroundedD == true)
            {
                Jumped = false;
                print("Touched Ground");
                gameObject.GetComponent<NavMeshAgent>().enabled = true;
                rb.velocity = Vector3.zero;
            }
        }
       
    }
    public virtual void ResetAttack2()
    {
        meleeAttack = false;
        attackPos.SetActive(false);
        attackTimes = 0;
        LostPlayer();
        HitPlayer = false;
        target = PatrolPoints[0];
    }
    public virtual void ResetAttack3()
    {
        //resetJump();
        Jumped = false;
    }
    IEnumerator jumpTimer()
    {
        yield return new WaitForSeconds(1);
        detectForGround = true;
    }
}
