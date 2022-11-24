using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemProtectAi : MonoBehaviour
{
    public KoboldProtectAi kobold;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag =="Player")
        {
            kobold.Protect = true;
        }
    }
}
