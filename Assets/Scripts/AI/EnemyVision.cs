using Palmmedia.ReportGenerator.Core.CodeAnalysis;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{

    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public GameObject Player;

    internal Transform LocationTarget;

    // Any layer that should be obstructing the Enemy's vision
    public LayerMask obstacleMask;

    //Player Layer
    public LayerMask playerMask;

    //Checks if player has been detected of not
    public bool PlayerDetected;
    void Start()
    {
        PlayerDetected = false;
        Player = GameObject.FindGameObjectWithTag("Player");
        FieldOfView();
    }

    void Update()
    {
        FieldOfView();
    }
    public void FieldOfView()
    {
        Vector3 playerTarget = (Player.transform.position - transform.position).normalized;

        if (Vector3.Angle(transform.forward, playerTarget) < viewAngle/2)
        {
            float distanceToTarget = Vector3.Distance(transform.position, Player.transform.position);
            if (distanceToTarget <= viewRadius) //checks if the player is close enough
            {
                if (Physics.Raycast(transform.position, playerTarget, distanceToTarget, obstacleMask) == false) //checks if the enemy can see the player
                {
                    PlayerDetected = true;
                    LocationTarget = Player.transform;
                }
                else
                {
                    PlayerDetected = false;
                }
            }
            else
            {
                PlayerDetected = false;
            }
        }
        else
        {
            PlayerDetected = false;
        }
    }
}
