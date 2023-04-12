using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    private CameraLogic camLogic;
    public GameObject GroundCam;
    public GameObject FlyingCam;
    public GameObject DragonGroundCam;
    public GameObject DragonAirCam;
    private GameObject currentCamera;

    private PlayerController PC;
    private DragonLockOn dragonLO;
    float sensitivity;
    static float oldSpeed;
    private void Start()
    {
        camLogic = FindObjectOfType<CameraLogic>();
        PC = GetComponent<PlayerController>();
        dragonLO = GetComponent<DragonLockOn>();
        currentCamera = GroundCam;
        sensitivity = PlayerPrefs.GetFloat("Sensitivity");
        oldSpeed = GroundCam.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed;
        GroundCam.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed = sensitivity * oldSpeed;
    }
    void Update()
    {
        if (PC.onGround)
        {
            if (!dragonLO.lockOnCamera && currentCamera != GroundCam)
            {
                ChangeCamera(GroundCam);
                camLogic.enabled = false;
            }
            if (dragonLO.lockOnCamera && currentCamera != DragonGroundCam)
            {
                ChangeCamera(DragonGroundCam);
                camLogic.enabled = true;
            }
        }
        else
        {
            if (!dragonLO.lockOnCamera && currentCamera != FlyingCam)
            {
                ChangeCamera(FlyingCam);
                camLogic.enabled = true;
            }
            if (dragonLO.lockOnCamera && currentCamera != DragonAirCam)
            {
                ChangeCamera(DragonAirCam);
                camLogic.enabled = true;
            }
        }
    }
    void ChangeCamera(GameObject newCamera)
    {
        currentCamera.SetActive(false);
        currentCamera = newCamera;
        currentCamera.SetActive(true);
    }
    public void updateSensitivity()
    {
        GroundCam.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed = sensitivity * oldSpeed;
    }
}
