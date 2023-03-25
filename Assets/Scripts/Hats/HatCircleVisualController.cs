using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatCircleVisualController : MonoBehaviour
{
    //I obtain the two public gameobjects
    public GameObject CircleVisual;
    public GameObject CirCamera;
    public float Scale;
    //the distance between the two gameobjects
    float distanceFromCamera;




    private void Start()
    {
        CirCamera = Camera.main.gameObject;
    }
    void Update()
    {
        //I made the code this way so that we can disable the method if we need to
        Aim();
    }
    void Aim()
    {
        //I check if the value is null in case the gameobject slots are empty
        if(CircleVisual != null)
        {
            if(CirCamera != null)
            {
                //this function gives us the distance between the two gameobjects
                distanceFromCamera = Vector3.Distance(CircleVisual.transform.position, CirCamera.transform.position);
            }
            
        }
        
        //The script only runs if the distance is greater than 2 to prevent the rope from shrinking
        
        
            this.gameObject.transform.localScale = new Vector3( distanceFromCamera * Scale,  -distanceFromCamera * Scale, distanceFromCamera);

        
        
        
        
        this.transform.LookAt(CirCamera.transform);
        
    }
}
