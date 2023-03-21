using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class DragonLockOn : MonoBehaviour
{
    private EnemyVision dragon;
    private WaterDragonAi water;
    private ControlsforPlayer controls;
    public bool CanLockOn; //{ get; set; }
    public GameObject LockOnVisual;
    public bool lockOnCamera;

    private void Start()
    {
        lockOnCamera = false;
        controls = new ControlsforPlayer();
        controls.Enable();
        controls.Actions.DragonLockOn.started += LockOnCamera;
        if (GameObject.FindGameObjectWithTag("Dragon"))
        {
            if (GameObject.FindGameObjectWithTag("Dragon").GetComponent<EnemyVision>())
                dragon = GameObject.FindGameObjectWithTag("Dragon").GetComponent<EnemyVision>();
            if (GameObject.FindGameObjectWithTag("Dragon").GetComponent<WaterDragonAi>())
                water = GameObject.FindGameObjectWithTag("Dragon").GetComponent<WaterDragonAi>();
        }
        else
        {
            enabled = false;
        }
    }
    private void LockOnCamera(InputAction.CallbackContext obj)
    {
        
        if (lockOnCamera == false)
        {
            lockOnCamera = true; //locks camera to the dragon
            LockOnVisual.SetActive(true);
        }
        else
        {
            lockOnCamera = false;
            LockOnVisual.SetActive(false);
        }
    }
    void Update()
    {
        if (water != null)
            CanLockOn = water.PlayerDetect();
        else if (dragon != null)
            CanLockOn = dragon.PlayerDetected;

    }
}
