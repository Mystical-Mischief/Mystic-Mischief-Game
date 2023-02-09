using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class DragonLockOn : MonoBehaviour
{
    private EnemyVision dragon;
    private WaterDragonAi water;
    public bool CanLockOn; //{ get; set; }

    private void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        if (water != null)
            CanLockOn = water.PlayerDetect();
        else if (dragon != null)
            CanLockOn = dragon.PlayerDetected;

    }
}
