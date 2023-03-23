using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering;

public class Gamma : MonoBehaviour
{
    public PostProcessVolume cameraBrightness;
    // ColorGradingModel cgm;
    public float GammaCorrection;
    public Light myLight;
    [SerializeField] Slider lightSlider;
    public float brightnessValue;
    public ColorGrading colorgrading;

    void Start()
    {
        ColorGrading tmp;
        cameraBrightness.profile.TryGetSettings(out colorgrading);
    }
    // public void SetBrightness(float brightness)
    // {
    //     ColorGradingModel.Settings settings = cgm.settings;
    //     settings.basic.postExposure = brightness;
    //     cgm.settings = settings;
    //     Debug.Log("Brightness is: " + brightness);  //For testing purposes
    // }
    public Rect SliderLocation;
    public void Update() {
        colorgrading.brightness.value = lightSlider.value;
    //    cameraBrightness.ColorGrading.brightness = lightSlider.value;
        // myLight.intensity = lightSlider.value + 0.5f;
        // Screen.brightness = 0;
       
    }

        public void OnSliderChange()
    {
        colorgrading.brightness.value = lightSlider.value;
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
