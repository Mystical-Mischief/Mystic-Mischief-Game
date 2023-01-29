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
    public Animator anim;
    
    new void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>();
        currentAttack = attackCooldown;
    } 

    new void Update()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
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
    private void OnCollisionEnter (Collision collision)
    {
        if(!attackedPlayer && collision.gameObject.tag == "Player")
        {
            if(!stunned)
            {
                attackedPlayer = true;
                LostPlayer();
                player.currentHealth--;
                //Knockback
                collision.transform.position += transform.forward * Time.deltaTime * knockbackForce;
            }
            
        }
    }

}
