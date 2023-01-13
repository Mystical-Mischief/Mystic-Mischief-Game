using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void Retry()
    {
        SceneManager.LoadScene(2);
        Debug.Log("In retry function");
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("In quit function");
    }
}
