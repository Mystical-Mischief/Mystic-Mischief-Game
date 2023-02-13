using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    private static readonly string BackgroundPref = "BackgroundPref";
    private static readonly string SoundEffectsPref = "SoundEffectsPref";

    private float _backgroundFloat, _soundEffectsFloat;

    public AudioSource BackgroundAudio;
    public AudioSource[] SoundEffectsAudio;


    void Awake()
    {
        ContinueSettings();
    }

    // Update is called once per frame
    private void ContinueSettings()
    {
        _backgroundFloat = PlayerPrefs.GetFloat(BackgroundPref);
        _soundEffectsFloat = PlayerPrefs.GetFloat(SoundEffectsPref);

        if(BackgroundAudio != null)
        {
            BackgroundAudio.volume = _backgroundFloat;
        }
        if(SoundEffectsAudio != null)
        {
            for (int i = 0; i < SoundEffectsAudio.Length; i++)
            {
                SoundEffectsAudio[i].volume = _soundEffectsFloat;
            }
        }
    }
}
