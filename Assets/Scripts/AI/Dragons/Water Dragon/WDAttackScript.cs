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
    private bool attackedPlayer;
    // private bool stunned;
    private bool attacked;
    public float knockbackForce;

    void Update()
    {
        if (attackedPlayer == true)
        {
            attacked = true;
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if(!attackedPlayer && other.gameObject.tag == "Player")
        {
            if(!attacked)
            {
                // other.gameObject.GetComponent<ThirdPersonController>().TakeDamage(1);
                attackedPlayer = true;
                // canAttack = true;
                //Knockback
                other.transform.position += transform.forward * Time.deltaTime * knockbackForce;
            }
            
        }

    }
}
