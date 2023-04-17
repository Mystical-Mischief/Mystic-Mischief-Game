using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopReticle : MonoBehaviour
{
    public GameObject reticle;
    private bool groundCheck;
    private RaycastHit hit;
    private Vector3 reticlePoint;
    [SerializeField]
    private PlayerController playerController;

    void FixedUpdate()
    {
        if(!playerController.onGround)
        {
            groundCheck = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 50f);
            reticlePoint = hit.point;
            reticlePoint.y += 0.2f;
            reticle.transform.position = reticlePoint;
            reticle.SetActive(true);
        }
        else
        {
            reticle.SetActive(false);
        }

    }
}
