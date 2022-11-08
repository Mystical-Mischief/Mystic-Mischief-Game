using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoboltAI : BaseEnemyAI
{
    float attackCooldown = 1f;
    float currentAttack = 1f;

    [SerializeField]
    bool attackedPlayer = false;

    ThirdPersonController player;

    [SerializeField]
    GameObject Item;

    [SerializeField]
    bool Protect;

    public bool holdingItem;

    public GameObject HeldItem;

    [SerializeField]
    private Transform fleeLocation;

    [SerializeField]
    private float knockbackForce;
    
    new void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
    } 

    new void Update()
    {
        base.Update();
        if(attackedPlayer)
        {
            LostPlayer();
            currentAttack-=Time.deltaTime;
            if(currentAttack <= 0)
            {
                currentAttack = attackCooldown;
                attackedPlayer = false;
            }
        }
        if(Item != null)
        {
            if(Protect)
            {
                ProtectObject(Item);
            }
            else
            {
                Patrol();
            }
        }
        Flee();

    }
    // Start is called before the first frame update
    private void OnCollisionEnter (Collision collision)
    {
        if(!attackedPlayer && collision.gameObject.tag == "Player")
        {
            attackedPlayer = true;
            player.currentHealth--;
            //Knockback
            collision.transform.position+= transform.forward*Time.deltaTime*knockbackForce;
            print($"Player Health: {player.currentHealth}");
            print("HIt");
            
        }
    }

    private void OnTriggerEnter (Collider collider)
    {
        if(Protect && collider.gameObject.tag == "Gold")
        {
            Protect = false;
            HeldItem = collider.gameObject;
            HeldItem.transform.SetParent(this.transform,true);
            holdingItem =true;
        }
    }

    private void ProtectObject(GameObject obj)
    {
        Vector3 itemDirection = obj.transform.position;
        UpdateDestination(itemDirection);
        
    }

    private void Flee()
    {
        if(holdingItem)
        {
            UpdateDestination(fleeLocation.position);
        }
    } 
}
