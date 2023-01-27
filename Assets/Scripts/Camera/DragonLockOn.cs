using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class DragonLockOn : MonoBehaviour
{
    public  GameObject dragonAi;
    public bool canActivate;
    CinemachineVirtualCamera virtualCamera;
    private GameObject playerFollow;
    ControlsforPlayer controls;
    bool lockedToDragon;
    // Start is called before the first frame update
    void Start()
    {
        dragonAi = GameObject.FindGameObjectWithTag("Dragon");
        canActivate = false;
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        playerFollow = GameObject.FindGameObjectWithTag("PlayerFollow");
        lockedToDragon = false;
    }

    // Update is called once per frame
    void Update()
    {
        //canActivate = controls.Actions.DragonLockOn.IsPressed();
        if (canActivate && !lockedToDragon)
        {
            virtualCamera.LookAt = dragonAi.transform;
            lockedToDragon=true;
            
        }
        else 
        {
            virtualCamera.LookAt = playerFollow.transform;
            lockedToDragon = false;
            
        }

    }
}
