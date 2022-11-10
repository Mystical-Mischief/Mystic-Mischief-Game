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
    private new bool FoundPlayer;
    public Transform Base;
    public float randomWaitTime;
    float lastWaitTime;
    private bool attacked;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private int state;

    [HideInInspector]
    public bool rangedAttacked;
    private bool detectForGround = true;

    [HideInInspector]
    public bool HitPlayer;
    [HideInInspector]
    public bool meleeAttack;
    public bool Jumped;

    private UnityEngine.AI.NavMeshAgent ai2;

    // Start is called before the first frame update
    new void Start()
    {
        ai2 = GetComponent<UnityEngine.AI.NavMeshAgent>();
        state = 0;
        RandomNumber();
        state = 0;
        NewRandomNumber();
        HitPlayer = false;
        attackTimes = 0;
        attacked = false;
        meleeAttack = false;
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        PlayerPos.x = Player.transform.position.x;
        PlayerPos.z = Player.transform.position.z;
        PlayerPos.y = 0;
        base.Update();
        float dist = Vector3.Distance(Player.transform.position, transform.position);
        //Debug.Log(dist);
        if (Player.transform.position.y > transform.position.y)
        {
            if (Jumped == false && gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled == true)
            {
                Jumped = true;
                Jump();
            }
        }
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

        if (state == 2) {
        transform.Rotate(new Vector3(0,10,0));
        state = state-1;
    } else if (state == 1) {
        transform.Rotate(new Vector3(0,-10,0));
        state = state-1;
    } else { // state is zero
        transform.Rotate(new Vector3(0,0,0));
    }

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
         if (!Jumped && dist > 12f && dist <17f && attackTimes < 5)
         {
            target = Player.transform;
            transform.LookAt(PlayerPos);
            if (rangedAttacked == false)
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
         if (Player.GetComponent<ThirdPersonController>().inWater == true)
         {
            attackTimes = 5;
         }

    }
    void Jump()
    {
        gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && meleeAttack == true)
        {  
            Debug.Log("DAMAGED!");
            meleeAttack = false;
            Invoke(nameof(ResetAttack2), 0f);
        }
        if(other.gameObject.CompareTag("Water")){
                base.ai.speed = 10f;
                base.Speed = 10f;
        }

    }
    void OnCollisionExit(Collision other)
    {
            if(other.gameObject.CompareTag("Water")){
                base.ai.speed = 3.5f;
                base.Speed = 3.5f;
        } 
    }
    void Ranged()
    {
        Rigidbody clone;
        clone = Instantiate(projectile, transform.position, Player.transform.rotation);
        //projectile.LookAt(Player.transform);

        clone.velocity = transform.TransformDirection(Vector3.forward * 10);
        Invoke(nameof(ResetAttack), 1f);
        rangedAttacked = true;
        attacked = true;
    }


    public virtual void ResetAttack()
    {
        rangedAttacked = false;
        attacked = false;
        attackTimes += 1;
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
    public virtual void ResetAttack2()
    {
        meleeAttack = false;
        attacked = false;
        attackPos.SetActive(false);
        attackTimes = 0;
        LostPlayer();
        HitPlayer = false;
        target = PatrolPoints[0];
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
        yield return new WaitForSeconds(1);
        detectForGround = true;
    }
}
