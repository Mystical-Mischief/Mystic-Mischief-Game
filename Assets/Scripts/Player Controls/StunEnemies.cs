using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StunEnemies : MonoBehaviour
{
    BaseEnemyAI enemyAI;
    //ControlsforPlayer controls;
    // Start is called before the first frame update
    void Start()
    {
        //controls.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "enemy")
        {
            enemyAI = other.GetComponent<BaseEnemyAI>();
            if(enemyAI.stunned == false)
            {
                enemyAI.stunned = true;
            }
        }
    }
}
