using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {

    //public int level;
    public int health;
    public float[] position;
    public float Stamina;
    public bool jumpInAir;
    public bool godMode;
    
    public PlayerData (ThirdPersonController player)
    {
        health = player.currentHealth;
        Stamina = player.Stamina;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
        jumpInAir = player.jumpInAir;
        godMode = player.godMode;
    }
}