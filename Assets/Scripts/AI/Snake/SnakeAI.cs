using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeAI : BaseEnemyAI
{
    
    bool attackedPlayer = false;

    PlayerController player;
    
    float attackCooldown = 3f;
    float currentAttack;

    [SerializeField]
    private float knockbackForce;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        player =  GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        currentAttack = attackCooldown;
    }

    // Update is called once per frame
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
    }
     private void OnCollisionEnter (Collision collision)
    {
        if(!attackedPlayer && collision.gameObject.tag == "Player")
        {
            LostPlayer();
            attackedPlayer = true;
            player.TakeDamage(1);
            //Knockback
            collision.transform.position+= transform.forward*Time.deltaTime*knockbackForce;
            
        }
    } 
}
