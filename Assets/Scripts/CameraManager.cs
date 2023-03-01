using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject GroundCam;
    public GameObject FlyingCam;
    public GameObject DragonGroundCam;
    public GameObject DragonAirCam;
    private GameObject currentCamera;

    private PlayerController PC;
    private DragonLockOn dragonLO;
    private void Start()
    {
        PC = GetComponent<PlayerController>();
        dragonLO = GetComponent<DragonLockOn>();
        currentCamera = GroundCam;
    }
    void Update()
    {
        if (PC.onGround)
        {
            if (!dragonLO.lockOnCamera)
            {
                ChangeCamera(GroundCam);
            }
            else
            {
                ChangeCamera(DragonGroundCam);
            }
        }
        else
        {
            if (!dragonLO.lockOnCamera)
            {
                ChangeCamera(FlyingCam);
            }
            else
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
