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
    bool HoldingItem;

    [SerializeField]
    GameObject Item;

    [SerializeField]
    bool Protect;
    
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

    }
    // Start is called before the first frame update
    private void OnCollisionEnter (Collision collision)
    {
        if(!attackedPlayer && collision.gameObject.tag == "Player")
        {
            attackedPlayer = true;
            player.currentHealth--;
            print($"Player Health: {player.currentHealth}");
            print("HIt");
            
        }
    }

    private void OnTriggerEnter (Collider collider)
    {
        if(Protect && collider.gameObject.tag == "Gold")
        {
            Protect = false;
            Destroy(collider.gameObject);
        }
    }

    private void ProtectObject(GameObject obj)
    {
        Vector3 itemDirection = obj.transform.position;
        UpdateDestination(itemDirection);
        
    } 
}
