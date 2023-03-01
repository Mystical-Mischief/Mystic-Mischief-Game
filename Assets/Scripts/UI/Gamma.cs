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

    // void Start()
    // {
    //     myLight = GetComponent<Light>();
    // }
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
    }
   
    void OnGUI () {
       
        GammaCorrection = GUI.HorizontalSlider(SliderLocation, GammaCorrection, 0, 1.0f);
       
    }
}
