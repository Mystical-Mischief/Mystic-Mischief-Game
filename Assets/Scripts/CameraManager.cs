using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private void Start()
    {
        camLogic = FindObjectOfType<CameraLogic>();
        PC = GetComponent<PlayerController>();
        dragonLO = GetComponent<DragonLockOn>();
        currentCamera = GroundCam;
    }
    void Update()
    {
        if (PC.onGround)
        {
            if (!dragonLO.lockOnCamera && currentCamera != GroundCam)
            {
                camLogic.enabled = false;
                ChangeCamera(GroundCam);
            }
            if (dragonLO.lockOnCamera && currentCamera != DragonGroundCam)
            {
                ChangeCamera(DragonGroundCam);
            }
        }
        else
        {
            if (!dragonLO.lockOnCamera && currentCamera != FlyingCam)
            {
                camLogic.enabled = true;
                ChangeCamera(FlyingCam);
            }
            if (dragonLO.lockOnCamera && currentCamera != DragonAirCam)
            {
                ChangeCamera(DragonAirCam);
            }
        }
    }
    void ChangeCamera(GameObject newCamera)
    {
        currentCamera.SetActive(false);
        currentCamera = newCamera;
        currentCamera.SetActive(true);
    }
}
