using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WDAttackScript : MonoBehaviour
{
    public GameObject Base;
    public bool HitPlayer = false;
    public Animator anim;
    public float maxHeight;
    private Vector3 height;

    void Update()
    {
        // height =  new Vector3 (transform.position.x, Base.GetComponent<WaterDragonAi>().Player.transform.position.y, transform.position.z);
        // transform.position = height;
        // if (transform.position.y >= maxHeight)
        // {
        //     transform.position = new Vector3 (transform.position.x, maxHeight, transform.position.z);
        // }
    }
    
    private void OnCollisionEnter(Collision other)
    {
            if (other.gameObject.tag == "Player")
        {  
            other.gameObject.GetComponent<ThirdPersonController>().TakeDamage(1);
            float force = 100;
            Debug.Log("DAMAGED!");
            gameObject.GetComponentInParent<WaterDragonAi>().rangedAttacked = false;
            HitPlayer = true;
            Base.GetComponent<WaterDragonAi>().HitPlayer = true;
            Base.GetComponent<WaterDragonAi>().attacked = true;
            // anim.SetTrigger("Bite");
        // Vector3 dir = other.contacts[0].point - transform.position;
        // other.gameObject.GetComponent<ThirdPersonController>().TakeDamage(2);
        //  // We then get the opposite (-Vector3) and normalize it
        //  dir = -dir.normalized;
        //  // And finally we add force in the direction of dir and multiply it by force. 
        //  // This will push back the player
        //  GetComponent<Rigidbody>().AddForce(dir*force);
        }

    }
}
