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
        if (ReloadNum.LastLevelNum == 1)
        {
            SceneManager.LoadScene("Level 1");
        }
        if (ReloadNum.LastLevelNum == 2)
        {
            SceneManager.LoadScene("Level 2");
        }
        if (ReloadNum.LastLevelNum == 3)
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
}
