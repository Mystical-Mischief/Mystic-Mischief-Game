using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraRecenter : MonoBehaviour
{
    private CinemachineFreeLook cam;
    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<CinemachineFreeLook>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.onGround == false)
        {
            cam.m_RecenterToTargetHeading.m_enabled = true;
        }
        else
        {
            cam.m_RecenterToTargetHeading.m_enabled = false;
        }

    }
}
