using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDestroy : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Water")
        {
            Destroy(gameObject);
        }
    }
}
