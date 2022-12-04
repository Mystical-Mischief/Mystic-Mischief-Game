using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CameraData {

    //public int level;
    public float[] position;
    public float[] rotation;
    
    public CameraData (CameraLogic camera)
    {

        position = new float[3];
        position[0] = camera.transform.position.x;
        position[1] = camera.transform.position.y;
        position[2] = camera.transform.position.z;
        
        rotation = new float[2];
        rotation[0] = camera.turn.x;
        rotation[1] = camera.turn.y;
        // rotation[2] = camera.transform.rotation.z;
    }
}
