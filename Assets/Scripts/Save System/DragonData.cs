using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DragonData {

    //public int level;
    public int patrolNum;
    public float[] position;
    public int RandomNumber;
    public string EnemyType;
    public bool spottedPlayer;
    public int AttackTimes;
    
    public DragonData (WaterDragonAi dragon)
    {
        EnemyType = dragon.EnemyType;
        spottedPlayer = dragon.spottedPlayer;
        patrolNum = dragon.patrolNum;  
        RandomNumber =  dragon.randomNumber;
        AttackTimes = dragon.attackTimes;

        position = new float[3];
        position[0] = dragon.transform.position.x;
        position[1] = dragon.transform.position.y;
        position[2] = dragon.transform.position.z;
    }
}
