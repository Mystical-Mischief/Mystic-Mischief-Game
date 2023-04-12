using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSphere : MonoBehaviour
{
    public GameObject Mask;
    // Start is called before the first frame update
    void Start()
    {
        //LayerMask mask = LayerMask.GetMask("Default");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("Default");
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 30))
        {
            if(hit.collider.tag=="Player")
            {
                
                Mask.gameObject.SetActive(false);
            }
            if(hit.collider.tag=="wall")
            {
               
                Mask.gameObject.SetActive(true);
                
            }
            else
            {
                Mask.gameObject.SetActive(false);
            }
        }
        else
        {
            Mask.gameObject.SetActive(false);
        }
    }
}
