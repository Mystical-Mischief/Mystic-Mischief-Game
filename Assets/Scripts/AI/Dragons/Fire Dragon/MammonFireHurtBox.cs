using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MammonFireHurtBox : MonoBehaviour
{
    public GameObject Player;
    public List<ParticleCollisionEvent> collisionEvents;
    public bool hurtPlayer;
    public ParticleSystem part;

    //This code is because Mammon needs a seperate object for the dragons fire breath to collide with him. I don't know why the particles dont collide with anything in Player(new).


    void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position;
    }

        void OnParticleCollision(GameObject other)
    {
        // if (other.gameObject.CompareTag("Player"))
        // {
        //     if (hurtPlayer == false)
        //     {
        //     Player.GetComponent<PlayerController>().TakeDamage(1);
        //     hurtPlayer = true;
        //     }
        // }
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
