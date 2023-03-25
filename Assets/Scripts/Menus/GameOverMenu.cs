using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public Reload reload;
    public int LastScene;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void Retry()
    {
        // ReloadNum.LastLevelNum;
        reload.retry = true;
        float lastLevel = PlayerPrefs.GetFloat("LastLevel", 1f);
        if (lastLevel == 1)
        {
            SceneManager.LoadScene("Level 1");
        }
        if (lastLevel == 2)
        {
            SceneManager.LoadScene("Level 2");
        }
        if (lastLevel == 3)
        {
            SceneManager.LoadScene("Level 3");
        }
        Debug.Log("In retry function");
    }

    void Update()
    {
        LastScene = ReloadNum.LastLevelNum;
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("In quit function");
    }

        public float GetFloat(string KeyName)
    {
        return PlayerPrefs.GetFloat(KeyName);
        // if(cameraScript != null)
        // {
        //     cameraScript.sensitivity = sensitivitySlider.value;
        // }
        // Save();
    }
}
