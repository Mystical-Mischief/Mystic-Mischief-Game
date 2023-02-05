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
        dragon = GameObject.FindGameObjectWithTag("Dragon").GetComponent<EnemyVision>();
        water = GameObject.FindGameObjectWithTag("Dragon").GetComponent <WaterDragonAi>();
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
