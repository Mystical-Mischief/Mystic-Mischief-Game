using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDragonAnimations : MonoBehaviour
{
    public FireDragonScript dragon;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        // dragon = GameObject.FindGameObjectWithTag("Dragon");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //If the dragon is chasing the player it sets the animator to play the run animation.
        if (dragon.target == dragon.Player && dragon.onGround == true)
        {
            anim.SetFloat("RunSpeed", 2f);
            anim.SetBool("ChasePlayer", true);
        }
        if (dragon.target != dragon.Player || dragon.onGround == false)
        {
            anim.SetBool("ChasePlayer", false);
        }

        //This sets the dragon to play the walk animation if the dragon is patroling on the ground.
        if (dragon.atDestination == false && dragon.onGround == true && dragon.target != dragon.Player)
        {
            anim.SetFloat("RunSpeed", 2f);
        }
        else 
        {
            anim.SetFloat("RunSpeed", 0f);
        }

        //This sets the flying animations to play.
        if (dragon.inAir == true)
        {
            anim.SetBool("Flying", true);
        }
        if (dragon.inAir == false)
        {
            anim.SetBool("Flying", false);
        }

        //This sets the fire breath animations to play.
        if (dragon.fireBreath == true)
        {
            anim.SetBool("FireBreath", true);
        }
        if (dragon.fireBreath == false)
        {
            anim.SetBool("FireBreath", false);
        }
    }
}
