using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeAI : BaseEnemyAI
{
    [SerializeField]
    bool attackedPlayer = false;

    ThirdPersonController player;
    [SerializeField]
    bool isAggressive;
    float attackCooldown = 1f;
    float currentAttack = 1f;

    
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        player =  GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
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
            attackedPlayer = true;
            player.currentHealth--;
            
            print($"Player Health: {player.currentHealth}");
            print("HIt");
            
        }
    } 
}
