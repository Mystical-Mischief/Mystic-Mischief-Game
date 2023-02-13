using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string BackgroundPref = "BackgroundPref";
    private static readonly string SoundEffectsPref = "SoundEffectsPref";

    private int _firstPlayInt;
    public Slider BackgroundSlider, SoundEffectsSlider;
    private float _backgroundFloat, _soundEffectsFloat;

    public AudioSource BackgroundAudio;
    public AudioSource[] SoundEffectsAudio;

    void Start()
    {
        _firstPlayInt = PlayerPrefs.GetInt(FirstPlay);
        if(_firstPlayInt == 0)
        {
            _backgroundFloat = 1f;
            _soundEffectsFloat = .5f;
            BackgroundSlider.value =_backgroundFloat;
            SoundEffectsSlider.value = _soundEffectsFloat;
            PlayerPrefs.SetFloat(BackgroundPref,_backgroundFloat);
            PlayerPrefs.SetFloat(SoundEffectsPref,_soundEffectsFloat);
            PlayerPrefs.SetInt(FirstPlay,-1);
        }
        else
        {
            _backgroundFloat = PlayerPrefs.GetFloat(BackgroundPref);
            BackgroundSlider.value = _backgroundFloat;
            _soundEffectsFloat = PlayerPrefs.GetFloat(SoundEffectsPref);
            SoundEffectsSlider.value = _soundEffectsFloat;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(BackgroundPref, BackgroundSlider.value);
        PlayerPrefs.SetFloat(SoundEffectsPref, SoundEffectsSlider.value);
    }

    private void OnApplicationFocus(bool focus)
    {
        if(!focus)
        {
            SaveSoundSettings();
        }
    }

    public void UpdateSound()
    {
        BackgroundAudio.volume = BackgroundSlider.value;
        for (int i = 0; i < SoundEffectsAudio.Length; i++)
        {
            SoundEffectsAudio[i].volume = SoundEffectsSlider.value;
        }
    }

}
