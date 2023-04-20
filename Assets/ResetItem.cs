using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetItem : MonoBehaviour
{
    public Vector3 resetPosition;
    void Start()
    {
        resetPosition = transform.position;
    }
    public void ResetPosition()
    {
        transform.position = resetPosition;
    }
}
