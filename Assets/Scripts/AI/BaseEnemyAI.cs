using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemyAI : MonoBehaviour
{
    public Transform[] PatrolPoints;
    public bool spottedPlayer;
    public int patrolNum;
    public float SightDistance;

    private Transform target;
    private NavMeshAgent ai;
    public void Start()
    {
        ai = GetComponent<NavMeshAgent>();

        if(target == null)
        {
            target = PatrolPoints[0].transform;
            UpdateDestination(target.position);
        }
    }

    public void Update()
    {
        if (!spottedPlayer)
        {
            EnemyDetection();
            Patrol();
        }
        else
        {
            FoundPlayer();
        }
    }
    public virtual void UpdateDestination(Vector3 newDestination)
    {
        ai.destination = newDestination;
    }
    public virtual void EnemyDetection()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * SightDistance, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, SightDistance))
        {
            if (hit.transform.gameObject.tag == "Player")
            {
                spottedPlayer = true;
                target = hit.transform;
                
            }
        }
    }
    private bool atDestination;
    public virtual void Patrol()
    {
        if (GetComponent<NavMeshAgent>().remainingDistance < 0.5f && atDestination == false)
        {
            atDestination = true;
            if (patrolNum < PatrolPoints.Length - 1)
            {
                patrolNum++;
            }
            else
            {
                patrolNum = 0;

            }
            target = PatrolPoints[patrolNum];
            UpdateDestination(target.position);
        }
        else
        {
            atDestination = false;
        }
    }
    public virtual void FoundPlayer()
    {
        Debug.DrawRay(transform.position, (target.position - transform.position).normalized * SightDistance, Color.green);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, target.position - transform.position, out hit, SightDistance))
        {
            UpdateDestination(target.position);
            if (hit.transform.gameObject.tag != "Player")
            {
                LostPlayer();
            }
        }
        else
        {
            LostPlayer();
        }
    }
    public virtual void LostPlayer()
    {
        spottedPlayer = false;
        target = PatrolPoints[0];
    }
}
