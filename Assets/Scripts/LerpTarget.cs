using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTarget : MonoBehaviour
{
    public Transform Target;
    public float CamSpeed;
    private float time;
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, Target.position, Time.deltaTime * CamSpeed);
    }
}
