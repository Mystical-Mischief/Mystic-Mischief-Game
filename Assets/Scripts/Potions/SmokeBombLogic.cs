using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBombLogic : MonoBehaviour
{
    public GameObject smokeScreen;
    public float stunTimer;
    public GameObject wizHat;

    private void OnCollisionEnter(Collision collision)
    {
        smokeScreen.SetActive(true);
        smokeScreen.transform.parent = null;
        gameObject.SetActive(false);
        gameObject.transform.parent = wizHat.transform;
        transform.localPosition = Vector3.zero;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            other.gameObject.GetComponent<BaseEnemyAI>().Stun(stunTimer);
        }
    }
}
    
