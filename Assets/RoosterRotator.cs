using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoosterRotator : MonoBehaviour
{
    public ExplorersHat expHat;
    void Update()
    {
        transform.forward = new Vector3(expHat.closestItem.transform.position.x - transform.position.x, 0, expHat.closestItem.transform.position.z - transform.position.z); 
    }
}
