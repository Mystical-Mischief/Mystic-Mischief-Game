using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public float mouseSensitivity = 300f;
    private float xRotation = 0f;
      public Transform playerBody;
  public Transform cameras;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp (xRotation, -90, 90);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);


    }
}
