using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    static bool GameIsPaused = false;
    public bool Paused;

    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private Button defaultBtn;

    private ControlsforPlayer playerControls;

    private void Awake()
    {
        playerControls = new ControlsforPlayer();
    }

    void Update()
    {
        if (GameIsPaused == true)
        {
            Paused = true;
        }
        if (GameIsPaused == false)
        {
            Paused = false;
        }
        
        if (playerControls.Pause.PauseGame.triggered)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if(playerControls.Pause.QuitGame.triggered)
        {
            QuitGame();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; //resumes game time
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        AudioListener.pause = false;
    }

    void Pause()
    {
        defaultBtn.Select();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; //stops the game
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        AudioListener.pause = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Loading menu...");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    public void Focus() 
    {
        defaultBtn.Select();
    }

    private void OnApplicationFocus(bool focus)
    {
        Focus();
    }


    public void ChangeDefaultBtn(Button button)
    {
        defaultBtn = button;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }
}
