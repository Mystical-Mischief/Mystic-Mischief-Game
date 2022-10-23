using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private bool FoundPlayer;
    public Transform Base;

    [HideInInspector]
    public bool attacked;
    [HideInInspector]
    public bool HitPlayer;
    [HideInInspector]
    public bool meleeAttack;
    [HideInInspector]
    public bool Jumped;

    // Start is called before the first frame update
    void Start()
    {

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
    void Update()
    {
        // base.ai.enabled = false;
        // GetComponent<UnityEngine.AI.NavMeshAgent>().baseOffset = -5;   
        //GetComponent<ConstantForce>().force = new Vector3(0, 10, 0);
        PlayerPos.x = Player.transform.position.x;
        PlayerPos.z = Player.transform.position.z;
        PlayerPos.y = 0;
        base.Update();
        float dist = Vector3.Distance(Player.transform.position, transform.position);
        //Debug.Log(dist);
        if (Player.transform.position.y > transform.position.y && dist < 5f)
        {
            if (Jumped == false)
            {
                //Jumped = true;
                //Jump();
                //Jumped = true;
            }
        }

        if (Player.GetComponent<ThirdPersonController>().rbSpeed >= 9 && dist < 17f)
        {
            FoundPlayer = true;
        }
        else if (dist > 17f)
        {
            FoundPlayer = false;
        }

        if  (FoundPlayer == true && attackTimes < 4)
        {
            base.ai.enabled = false;
        }
        else if (FoundPlayer == false)
        {
            base.ai.enabled = true;
        }
        if (FoundPlayer == true && attackTimes >= 5)
        {
            base.ai.enabled = true;
        }

        //UNCOMENT THIS IF YOU WANT THE JUMP //
        if (Jumped == true)
        {
                if (base.ai.enabled)
            {
                // set the agents target to where you are before the jump
                // this stops her before she jumps. Alternatively, you could
                // cache this value, and set it again once the jump is complete
                // to continue the original move
                base.ai.SetDestination(Player.transform.position);
                // disable the agent
                //base.ai = disabled;
            }

            // make the jump
            base.PatrolPoints = null;
            Debug.Log("Jump");
            //GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<UnityEngine.AI.NavMeshAgent>().baseOffset = -5;
            base.rb.velocity = new Vector3( 0f, jumpForce, 0f);
            Invoke(nameof(ResetAttack3), 1f);
            Jumped = false;
        }

         if (dist > 12f && dist <17f && attackTimes < 5)
        {
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
            base.target = Player.transform;
            base.UpdateDestination(target.position);
        }
         if (dist > 2f && dist <12f)
         {
            transform.LookAt(PlayerPos);
            if (meleeAttack == false)
            {
            attackPos.SetActive(true);
            meleeAttack = true;
         }
         }

    }
    void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(0, 5f, 0), ForceMode.Impulse);
    }

    void BaseLostPlayer()
    {

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

    void resetJump()
    {
        if(base.isGroundedD == false){
        transform.position = Vector3.MoveTowards(transform.position, (transform.position - new Vector3(0f, 2f, 0f) + Vector3.forward), speed * Time.deltaTime);
        }
    }
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

    //     foreach (Collider enemy in hitObjects)
    //     {
    //         if(enemy.gameObject.tag == "Player"){
    //             Debug.Log("DAMAGED!");
    //         }
    //     }
    //     Invoke(nameof(ResetAttack), timeBetweenAttacks);
    // }
        public virtual void ResetAttack()
    {
        attacked = false;
        attackTimes += 1;
    }
            public virtual void ResetAttack2()
    {
        meleeAttack = false;
        attackPos.SetActive(false);
        attackTimes = 0;
        base.LostPlayer();
        HitPlayer = false;
        base.target = base.PatrolPoints[0];
    }
    public virtual void ResetAttack3()
    {
        resetJump();
        Jumped = false;
    }
}
