using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoboltAI : BaseEnemyAI
{
    float attackCooldown = 3f;
    float currentAttack;

    [SerializeField]
    bool attackedPlayer = false;

    ThirdPersonController player;

    [SerializeField]
    private float knockbackForce;
    
    new void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
        currentAttack = attackCooldown;
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
    }
    // Start is called before the first frame update
    private void OnCollisionEnter (Collision collision)
    {
        if(!attackedPlayer && collision.gameObject.tag == "Player")
        {
            attackedPlayer = true;
            LostPlayer();
            player.currentHealth--;
            //Knockback
            collision.transform.position+= transform.forward*Time.deltaTime*knockbackForce;
            
        }
    }

}
