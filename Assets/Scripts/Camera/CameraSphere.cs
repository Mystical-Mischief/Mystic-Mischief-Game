using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSphere : MonoBehaviour
{
    public GameObject Mask;
    public Transform Mammon;
    private Vector3 facingplayer;

    // Update is called once per frame
    void LateUpdate()
    {
       //transform.LookAt(Mammon, Vector3.up);
        RaycastHit hit;


        if (Mask != null)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Vector3.Distance(Mammon.position, transform.position)))
            {
                if (hit.collider.tag == "Player")
                {

                    Mask.gameObject.SetActive(false);
                }
                if (hit.collider.tag == "StaticObstacle")
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
}
