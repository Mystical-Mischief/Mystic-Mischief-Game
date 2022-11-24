using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorScript : MonoBehaviour
{
    public float RotateXValue;
    public float RotateYValue;
    public float RotateZValue;
    void Update()
    {
        //rotates the ticket based off the axis over time. Simply just adjust the value to make it rotate
        transform.Rotate(new Vector3(Time.deltaTime * RotateXValue, Time.deltaTime * RotateYValue, Time.deltaTime * RotateZValue), Space.World);
    }
}
