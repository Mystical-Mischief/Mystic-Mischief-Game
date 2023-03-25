using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SnakeVariant : BaseEnemyAI
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
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        currentAttack = attackCooldown;
        patrolNum = (int)Random.Range(0, PatrolPoints.Count - 1);
    }

    // Update is called once per frame
    new void Update()
    {
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
            LostPlayer();
            attackedPlayer = true;
            if (player.godMode == false)
            {
                player.currentHealth--;
            }
            //Knockback
            collision.transform.position += transform.forward * Time.deltaTime * knockbackForce;

        }
    }
    public override void Patrol()
    {
        int currentPatolPoint = patrolNum;
        if (GetComponent<NavMeshAgent>().remainingDistance < 0.5f && atDestination == false)
        {
            atDestination = true;
            patrolNum =(int) Random.Range(0, PatrolPoints.Count-1);
            //if the player isnt at the last point
            if (patrolNum == currentPatolPoint)
            {
                patrolNum = (int)Random.Range(0, PatrolPoints.Count - 1);
            }
            
            target = PatrolPoints[patrolNum];
            UpdateDestination(target.position);
        }
        else
        {
            atDestination = false;
        }
    }
}
