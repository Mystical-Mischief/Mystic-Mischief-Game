using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunEnemies : MonoBehaviour
{
    BaseEnemyAI enemyAI;

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
