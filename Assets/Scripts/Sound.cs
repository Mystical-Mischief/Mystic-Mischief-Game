using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Sound : MonoBehaviour
{

    [SerializeField] Slider volumeSlider;
    public AudioMixerGroup audioMixerGroup;
    // public AudioMixerGroup[] FindMatchingGroups(string BGM);

    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("musicVolume")){
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }

        else{
            Load();
        }
    }
     public AudioMixer mixer;
     public string parameterName = "SE";
 
     protected float Parameter
     {
         get
         {
             float parameter;
             mixer.GetFloat(parameterName, out parameter);
             return parameter;
         }
         set
         {
             mixer.SetFloat(parameterName, value);
         }
     }
    public void ChangeVolume(){
        Parameter = volumeSlider.value;
        // audioMixerGroup.SetFloat(volumeSlider.value);
        // audioMixerGroup.MyExposedParam = volumeSlider.value;
        Save();
    }

    private void Load(){
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }
    
    private void Save(){
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
