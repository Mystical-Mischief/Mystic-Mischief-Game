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
    private Transform PlayerPos;
    public float timeBetweenAttacks;
    public float attackRange;
    public Transform[] Enemy;
    public GameObject attackPos;
    public LayerMask whatIsEnemy;
    private int speed = 5;

    [HideInInspector]
    public bool attacked;

    // Start is called before the first frame update
    void Start()
    {
        attacked = false;
        base.Start();
        //Ranged = gameObject.GetComponent<CapsuleCollider>();
        jump = gameObject.GetComponent<SphereCollider>();
        //Melee = gameObject.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

       // base.Update();
        float dist = Vector3.Distance(Player.transform.position, transform.position);
        //Debug.Log(dist);
         if (dist > 12f && dist <17f)
        {
            transform.LookAt(Player.transform.position);
            if (attacked == false)
            {Ranged();
            attacked = true;}
            //Debug.Log("Ranged");
        }
        //else {ranged = false;}
         if (dist > 2f && dist <12f)
         {
            transform.LookAt(Player.transform.position);
            if (attacked == false)
            {
            attackPos.SetActive(true);
            attacked = true;}
         }
    }
    private void OnCollisionEnter(Collision other)
    {
            if (other.gameObject.tag == "Player" && attacked == true)
        {  
            Debug.Log("DAMAGED!");
            attacked = false;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

    }
    void Ranged()
    {

        Rigidbody clone;
        clone = Instantiate(projectile, transform.position, Player.transform.rotation);
        //projectile.LookAt(Player.transform);
        clone.velocity = transform.TransformDirection(Vector3.forward * 10);
        Invoke(nameof(ResetAttack), timeBetweenAttacks);
    }
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
        attackPos.SetActive(false);
    }
}
