using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public Reload reload;
    public int LastScene;
    public string lastLevel;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void Retry()
    {
        // ReloadNum.LastLevelNum;
        reload.retry = true;
        lastLevel = PlayerPrefs.GetString("SceneName", "Level 1");
        // if (lastLevel == 1)
        // {
        //     SceneManager.LoadScene("Level 1");
        // }
        // if (lastLevel == 2)
        // {
        //     SceneManager.LoadScene("Level 2");
        // }
        // if (lastLevel == 3)
        // {
            SceneManager.LoadScene(lastLevel);
        // }
        Debug.Log("In retry function");
    }
    public void Load()
    {
        // ReloadNum.LastLevelNum;
        reload.Load = true;
        lastLevel = PlayerPrefs.GetString("SceneName", "Level 1");
        // if (lastLevel == 1)
        // {
        //     SceneManager.LoadScene("Level 1");
        // }
        // if (lastLevel == 2)
        // {
        //     SceneManager.LoadScene("Level 2");
        // }
        // if (lastLevel == 3)
        // {
        //     SceneManager.LoadScene("Level 3");
        // }
        SceneManager.LoadScene(lastLevel);
        Debug.Log("In Loading reload function");
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

    public void SetString(string KeyName, string Text)
    {
        PlayerPrefs.SetString(KeyName, Text);
    }

    public string GetString(string KeyName)
    {
        return PlayerPrefs.GetString(KeyName);
    }
}
