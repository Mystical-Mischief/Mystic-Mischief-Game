using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBreathScript : MonoBehaviour
{
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;
    public Transform target;
    PlayerController player;
    public bool hurtPlayer;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        transform.LookAt(target);
        if (hurtPlayer == true)
        {
            Invoke(nameof(resetattack), 5f);
        }
    }

    public void resetattack()
    {
        hurtPlayer = false;
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (hurtPlayer == false)
            {
            player.TakeDamage(1);
            hurtPlayer = true;
            }
        }
        // part.Play(false);
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
        Rigidbody rb2 = other.GetComponent<Rigidbody>();
        int i = 0;

        while (i < numCollisionEvents)
        {
            if (rb2)
            {
                Vector3 pos = collisionEvents[i].intersection;
                Vector3 force = collisionEvents[i].velocity * 2;
                rb2.AddForce(force);
                Debug.Log("Hit");
            }
            i++;
        }
    }
}
