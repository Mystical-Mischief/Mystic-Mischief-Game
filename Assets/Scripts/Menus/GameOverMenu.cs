using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public Reload reload;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void Retry()
    {
        reload.retry = true;
        SceneManager.LoadScene(2);
        Debug.Log("In retry function");
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("In quit function");
    }
}
