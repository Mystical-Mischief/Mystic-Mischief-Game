using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class KoboldProtectAi : BaseEnemyAI
{
    float attackCooldown = 1f;
    float currentAttack = 1f;

    [SerializeField]
    bool attackedPlayer = false;

    PlayerController playerController;

    GameObject player;

    [SerializeField]
    GameObject Item;

    [SerializeField]
    Transform ObjectNewLocation;

    public Transform ItemToProtectPos;


    public bool Protect;

    public bool holdingItem { get; private set; }

    public GameObject HeldItem;
    public GameObject ItemToProtect;
    public Animator anim;

    [SerializeField]
    private Transform fleeLocation;

    [SerializeField]
    private float knockbackForce = 20;

    public bool flee;

    new void Start()
    {
        base.Start();
        fleeLocation = PatrolPoints.First();   
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        Protect = false;
        holdingItem = false;
        flee = false;
        if (HeldItem != null)
        {
            ItemToProtectPos = HeldItem.transform;
            ItemToProtect = Instantiate(HeldItem, ItemToProtectPos.position, Quaternion.identity);

            HeldItem.transform.SetParent(this.transform, false);
            HeldItem.transform.position = ObjectNewLocation.position;
            HeldItem.SetActive(false);
        }



    }

    new void Update()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        base.Update();
        if (attackedPlayer)
        {
            LostPlayer();
            currentAttack -= Time.deltaTime;
            if (currentAttack <= 0)
            {
                currentAttack = attackCooldown;
                attackedPlayer = false;
                ContinuePatrol();
            }
        }
        if (HeldItem != null)
        {
            if (Protect && ItemToProtect != null)
            {
                anim.SetBool("HasItem", true);
                ProtectObject(ItemToProtect);
            }
            else if (flee)
            {
                Flee();
                ContinuePatrol();
            }

            if (flee == false)
            {
                Patrol();
                // anim.SetBool("HasItem", false);
            }

        }

        if (base.stunned == true)
        {
            anim.SetTrigger("Hurt");
        }

        if (base.spottedPlayer == true)
        {
            anim.SetBool("FoundPlayer", true);
        }
        else { anim.SetBool("FoundPlayer", false); }

        if (base.ai.speed > 0.1)
        {
            anim.SetFloat("RunSpeed", 1f);
        }
        if (dist <= 2f)
        {
            anim.SetTrigger("Bite");
        }

        if (ItemToProtect != null && Vector3.Distance(this.transform.position, ItemToProtect.transform.position) < 1 && flee == false)
        {
            anim.SetTrigger("Bite");
            Protect = false;
            Destroy(ItemToProtect);
            holdingItem = true;
            flee = true;
            HeldItem.SetActive(true);
        }

        //test = Vector3.Distance(transform.position, fleeLocation.position);

    }
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {

        if (!attackedPlayer && collision.gameObject.tag == "Player")
        {
            attackedPlayer = true;
            playerController.currentHealth--;
            //Knockback
            // collision.gameObject.GetComponent<PlayerController>().rb.AddForce((-transform.forward) * knockbackForce);

        }
        if (collision.gameObject.tag == "Whip")
        {
            stunned = true;
            // base.ai_Rb.AddForce(Player.transform.position * ai.speed, ForceMode.Impulse);
        }

        if (collision.gameObject.tag == "Poop")
        {
            stunned = true;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Whip")
        {
            stunned = true;
            base.ai_Rb.AddForce(Player.transform.position * ai.speed, ForceMode.Impulse);
        }
    }

    private void ProtectObject(GameObject obj)
    {
        Vector3 itemDirection = obj.transform.position;
        UpdateDestination(itemDirection);


    }

    private void Flee()
    {
        if (holdingItem)
        {
            if (Vector3.Distance(transform.position, fleeLocation.position) < 3)
            {
                ContinuePatrol();
                flee = false;
                //ai.isStopped = true;
                //return;
                LostPlayer();
            }
            else
            {
                targetPosition = fleeLocation.position;
                UpdateDestination(target.position);
            }

            //if(Vector3.Distance(transform.position, fleeLocation.position) <= 1)
            //{
            //    ContinuePatrol();
            //     flee = false;
            //}
        }
    }

    private void ContinuePatrol()
    {
        if (transform.position == fleeLocation.transform.position)
        {
            LostPlayer();
            UpdateDestination(target.position);
            Patrol();
        }
    }
}
