using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoboldProtectAi : BaseEnemyAI
{
    float attackCooldown = 1f;
    float currentAttack = 1f;

    [SerializeField]
    bool attackedPlayer = false;

    ThirdPersonController player;

    [SerializeField]
    GameObject Item;


    public bool Protect;

    private bool holdingItem;

    public GameObject HeldItem;
    public Animator anim;

    [SerializeField]
    private Transform fleeLocation;

    [SerializeField]
    private float knockbackForce;

    new void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
        Protect = false;
        holdingItem = false;
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
            }
        }
        if (Item != null)
        {
            if (Protect)
            {
                anim.SetBool("HasItem", true);
                ProtectObject(Item);
            }
            else
            {
                Patrol();
                // anim.SetBool("HasItem", false);
            }
        }
        Flee();
                if (base.spottedPlayer == true)
        {
            anim.SetBool("FoundPlayer", true);
        }
        else {anim.SetBool("FoundPlayer", false);}
        if (base.ai.speed > 0.1)
        {
            anim.SetFloat("RunSpeed", 1f);
        }
        if (dist <= 2f)
        {
            anim.SetTrigger("Bite");
        }

    }
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (!attackedPlayer && collision.gameObject.tag == "Player")
        {
            attackedPlayer = true;
            player.currentHealth--;
            //Knockback
            collision.transform.position += transform.forward * Time.deltaTime * knockbackForce;

        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (Protect && collider.gameObject.tag == "PickUp")
        {
            anim.SetTrigger("Bite");
            Protect = false;
            HeldItem = collider.gameObject;
            HeldItem.transform.SetParent(this.transform, true);
            holdingItem = true;
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
            UpdateDestination(fleeLocation.position);
        }
    }
}
