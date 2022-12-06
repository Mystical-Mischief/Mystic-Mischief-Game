using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeStretcher : MonoBehaviour
{
    //I obtain the two public gameobjects
    public GameObject StartOfRope;
    public GameObject LassoProjectile;
    //the distance between the two gameobjects
    float distance;


   

    
    void Update()
    {
        //I made the code this way so that we can disable the method if we need to
        Stretch();
    }
    void Stretch()
    {
        //I check if the value is null in case the gameobject slots are empty
        if(distance != null)
        {
            //this function gives us the distance between the two gameobjects
            distance = Vector3.Distance(StartOfRope.transform.position, LassoProjectile.transform.position);
        }
        
        //The script only runs if the distance is greater than 2 to prevent the rope from shrinking
        if(distance >2)
        {
            this.gameObject.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, distance/2);
        }
        
    }
}
