using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.AI; 

// [RequireComponent(typeof(ParticleSystem))]
public class FireDragonScript : BasicfireDragonAI
{
    // public Transform target;
    public bool SeesPlayer;
    // public LayerMask layerMask;
    private float dist;
    public bool spotsPlayer;
    // public ParticleSystem ps;
    public float maxDistance;
    // public float meleeDist;
    private bool meleeAttack;
    public GameObject attackPos;
    private bool attacked;
    public float attackDist;
    public float agressionMeter;
    public bool fireBreath;
    public EnemyVision ev;
    public ItemCollector iCollector;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        ev.Player = GameObject.FindGameObjectWithTag("Player");
        // ps = GetComponent<ParticleSystem>();
        
    }

    // Update is called once per frame
    new void Update()
    {
        //This sets the enemy vision range it can detect tot the agression meter.
        if (iCollector.numOfItems > 0)
        {
        ev.viewRadius = agressionMeter * iCollector.numOfItems;
        }
        if (iCollector.numOfItems < 1)
        {
            ev.viewRadius = agressionMeter;
        }
        spotsPlayer = ev.PlayerDetected;
        // float closeDist = Vector3.Distance(base.target.position, transform.position);
        base.Update();
        // RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        // if (spottedPlayer == true)
        // {
            // transform.LookAt(target);
            // float maxRange = 5;
        // if (Physics.Raycast(transform.position, (base.target.position - transform.position), out hit, maxRange, layerMask))
        // {
        //     Debug.DrawRay(transform.position, (base.target.position - transform.position) * maxDistance, Color.yellow);
        //     // Debug.Log("Hit Wall");
        //     if (hit.transform.tag == "Player")
        //     {
        //         SeesPlayer = true;
        //     }
        //     else
        //     {
        //         SeesPlayer = false;
        //     }
        // }
        // else
        // {
        //     Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        //     // Debug.Log("Hit Player");
        // }
        //dist <= attackDist && 
        //If the dragon is in the air and spots the player it shoots fire breath.
        if (base.inAir == true && base.spottedPlayer == true)
        {
            ps.Play(true);
            fireBreath = true;
        }
        else 
        {
            ps.Stop(true);
            fireBreath = false;
        }

        // If it is close to the dragon it sets the attack to true.
        //  if (dist > 2f && dist <= meleeDist)
        //  {
        //     //transform.LookAt(PlayerPos);
        //     if (meleeAttack == false)
        //     {
        //     attackPos.SetActive(true);
        //     meleeAttack = true;
        //     attacked = true;
        //     }
            // else {anim.SetBool("Bite 0", false);}
        //  }
    }
}
