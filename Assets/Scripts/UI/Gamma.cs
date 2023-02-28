using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamma : MonoBehaviour
{
    public float GammaCorrection;
    public Light myLight;
    [SerializeField] Slider lightSlider;
    public float brightnessValue;

    void Start()
    {
        lightSlider.value = PlayerPrefs.GetFloat("Gamma", 1f);
    }
    public Rect SliderLocation;
    public void Update() {
       brightnessValue = lightSlider.value;
        myLight.intensity = lightSlider.value + 0.5f;
        Screen.brightness = 0;
       
    }

        public void OnSliderChange()
    {
        brightnessValue = lightSlider.value;
        Update();
        SetFloat("Gamma", lightSlider.value);
    }
   
    void OnGUI () {
       
        GammaCorrection = GUI.HorizontalSlider(SliderLocation, GammaCorrection, 0, 1.0f);
       
    }

        public void SetFloat(string KeyName, float Value)
    {
        PlayerPrefs.SetFloat(KeyName, Value);
    }

    public float GetFloat(string KeyName)
    {
        return PlayerPrefs.GetFloat(KeyName);
        if(myLight != null)
        {
            brightnessValue = lightSlider.value;
        }
        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("Gamma", lightSlider.value);
    }
}
