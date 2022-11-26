using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivity : MonoBehaviour
{
    public float mouseSensitivity;
    public CameraLogic cameraScript;
    [SerializeField] Slider sensitivitySlider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Rect SliderLocation;
    // Update is called once per frame
    void Update()
    {
        mouseSensitivity = sensitivitySlider.value;
        cameraScript.sensitivity = mouseSensitivity;
    }
    public void OnSliderChange()
    {
        mouseSensitivity = sensitivitySlider.value;
        Update();
    }
}
