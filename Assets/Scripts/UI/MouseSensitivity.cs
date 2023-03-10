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
        sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity", 0.1f);
        // mouseSensitivity = sensitivitySlider.value;
        // if(!PlayerPrefs.HasKey("Sensitivity")){
        //     PlayerPrefs.SetFloat("Sensitivity", 0.3f);
        //     Load();
        // }

        // else{
        //     Load();
        // }
    }

    public Rect SliderLocation;
    // Update is called once per frame
    public void OnSliderChange()
    {
        mouseSensitivity = sensitivitySlider.value;
        if(cameraScript != null)
        {
        cameraScript.sensitivity = mouseSensitivity;
        }
        // Update();
        // Save();
        SetFloat("Sensitivity", sensitivitySlider.value);
    }

    public void SetFloat(string KeyName, float Value)
    {
        PlayerPrefs.SetFloat(KeyName, Value);
    }

    public float GetFloat(string KeyName)
    {
        return PlayerPrefs.GetFloat(KeyName);
        if(cameraScript != null)
        {
            cameraScript.sensitivity = sensitivitySlider.value;
        }
        Save();
    }

    private void Load(){
        sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity");
    }
    
    private void Save(){
        PlayerPrefs.SetFloat("Sensitivity", sensitivitySlider.value);
    }
}
