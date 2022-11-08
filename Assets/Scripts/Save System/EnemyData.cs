using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData {

    //public int level;
    public int patrolNum;
    public float[] position;
    public string EnemyType;
    public bool spottedPlayer;
    
    public EnemyData (BaseEnemyAI enemy)
    {
        EnemyType = enemy.EnemyType;
        spottedPlayer = enemy.spottedPlayer;
        patrolNum = enemy.patrolNum;       

        position = new float[3];
        position[0] = enemy.transform.position.x;
        position[1] = enemy.transform.position.y;
        position[2] = enemy.transform.position.z;
    }
}
