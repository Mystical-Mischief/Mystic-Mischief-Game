using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


 public class ReloadNum {
     public static int LastLevelNum;
 }
public class Reload : MonoBehaviour
{
    public static bool loadFromCheckPoint;
    public bool retry;
    public bool retrying;
    public bool reloaded;

    // public static int LastLevelNum;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        if (retry == true)
        {
            loadFromCheckPoint = true;
        }
        if (loadFromCheckPoint == true)
        {
            retrying = true;
        }
        if (reloaded == true)
        {
            retry = false;
            loadFromCheckPoint = false;
            retrying = false;
        }
    }

     private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // SetString("_last_scene_", scene.name);
    }
     public static void LoadLastScene()
    {
        // SceneManager.LoadScene(GetString("_last_scene_"));
    }

    //     public void SetString(string KeyName, string name)
    // {
    //     PlayerPrefs.SetFloat(KeyName, Value);
    // }

    // public float GetString(string KeyName)
    // {
    //     // return PlayerPrefs.GetString(KeyName);
    //     // if(cameraScript != null)
    //     // {
    //     //     cameraScript.sensitivity = sensitivitySlider.value;
    //     // }
    //     // Save();
    // }

}
