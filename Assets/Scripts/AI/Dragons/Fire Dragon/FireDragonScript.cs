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
    private float distance;
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
    public Rigidbody  projectile;
    public bool fireAttack;
    public float resetAttackTime;
    public float projectileSpeed;
    public Transform firePosition;

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
        //If the dragon is in the air and spots the player it shoots fire breath.
        if (base.inAir == true && base.spottedPlayer == true)
        {
            if (dist < attackDist)
            {
                ps.Play(true);
                fireBreath = true;
            }
            if (dist > attackDist && fireAttack == false)
            {
                    Ranged();
            }
            if (dist > attackDist || base.spottedPlayer == false)
            {
                ps.Stop(true);
                fireBreath = false;
            }

        }
        // else 
        // {

        // }
        // if (dist > attackDist && base.spottedPlayer == true && fireAttack == false && fireBreath == false)
        // {
        //     Ranged();
        // }
    }

    void Ranged()
    {
        Rigidbody clone;
        clone = Instantiate(projectile, firePosition.position, Player.transform.rotation);
        // Speed = 0;
        //projectile.LookAt(Player.transform);

        clone.velocity = (Player.transform.position - clone.position).normalized * projectileSpeed;
        Invoke(nameof(ResetAttack), resetAttackTime);
        fireAttack = true;
    }

    public void ResetAttack()
    {
        fireAttack = false;
    }
}
