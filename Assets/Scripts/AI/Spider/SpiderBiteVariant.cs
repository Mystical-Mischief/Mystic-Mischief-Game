using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBiteVariant : BaseEnemyAI
{
    float attackCooldown = 3f;
    float currentAttack;

    [SerializeField]
    bool attackedPlayer = false;

    PlayerController player;

    [SerializeField]
    private float knockbackForce;
    public Animator anim;

    public float wanderRange;

    new void Start()
    {
        base.Start();

        //grabs the Player Controller
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        //sets current attack to 0
        currentAttack = attackCooldown;
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
    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!attackedPlayer && collision.gameObject.tag == "Player")
        {
            attackedPlayer = true;
            LostPlayer();
            player.currentHealth--;

            //Knockback
            collision.transform.position += transform.forward * Time.deltaTime * knockbackForce;
        }
    }

    public override void Patrol()
    {
        if (!stunned)
        {
            if (GetComponent<UnityEngine.AI.NavMeshAgent>().remainingDistance < 0.5f && atDestination == false)
            {
                atDestination = true;
                Vector3 randomDirection = Random.insideUnitSphere * wanderRange;
                randomDirection += transform.position;
                UnityEngine.AI.NavMeshHit hit;
                UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, wanderRange, 1);
                Vector3 finalPosition = hit.position;
                print(finalPosition);
                UpdateDestination(finalPosition);
            }
            else
            {
                atDestination = false;
            }
        }
    }

}
