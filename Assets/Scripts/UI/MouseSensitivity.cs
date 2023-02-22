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
        if (FindObjectOfType<CameraLogic>())
        {
            cameraScript = FindObjectOfType<CameraLogic>();
        }
        if(!PlayerPrefs.HasKey("Sensitivity")){
            PlayerPrefs.SetFloat("Sensitivity", 1);
            Load();
        }

        else{
            Load();
        }
    }

    public Rect SliderLocation;
    // Update is called once per frame
    public void OnSliderChange()
    {
        mouseSensitivity = sensitivitySlider.value;
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
