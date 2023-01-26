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
    public LayerMask layerMask;
    private float dist;
    public bool spotsPlayer;
    public ParticleSystem ps;
    public float maxDistance;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        ps = GetComponent<ParticleSystem>();
        ps.Play(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        // if (spottedPlayer == true)
        // {
            // transform.LookAt(target);
            float maxRange = 5;
        if (Physics.Raycast(transform.position, (base.target.position - transform.position), out hit, maxRange, layerMask))
        {
            Debug.DrawRay(transform.position, (base.target.position - transform.position) * maxDistance, Color.yellow);
            // Debug.Log("Hit Wall");
            if (hit.transform.tag == "Player")
            {
                SeesPlayer = true;
            }
            else
            {
                SeesPlayer = false;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            // Debug.Log("Hit Player");
        }
    }

        void OnParticleCollision(GameObject other)
    {

    }
        //     if (other.gameObject.CompareTag("Player"))
        // {
        //     Debug.Log("HitPlayer");
        // }
}
