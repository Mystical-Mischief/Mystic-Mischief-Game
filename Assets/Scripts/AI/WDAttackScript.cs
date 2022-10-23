using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WDAttackScript : MonoBehaviour
{
    public GameObject Base;
    public bool HitPlayer = false;
    private void OnCollisionEnter(Collision other)
    {
            if (other.gameObject.tag == "Player" && gameObject.GetComponentInParent<WaterDragonAi>().attacked == true)
        {  
            Debug.Log("DAMAGED!");
            gameObject.GetComponentInParent<WaterDragonAi>().attacked = false;
            HitPlayer = true;
            gameObject.GetComponentInParent<WaterDragonAi>().HitPlayer = true;;
        }

    }
}
