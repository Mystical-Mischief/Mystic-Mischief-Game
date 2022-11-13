using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WDAttackScript : MonoBehaviour
{
    public GameObject Base;
    public bool HitPlayer = false;
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
