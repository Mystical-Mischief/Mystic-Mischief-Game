using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public bool Paused;

    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private Button defaultBtn;

    private ControlsforPlayer playerControls;
    private Reload reload;
    private Scene scene;

    private void Awake()
    {
        scene = SceneManager.GetActiveScene();
        playerControls = new ControlsforPlayer();
        if (scene.name != "Wizard Hub")
        {
        reload = GameObject.Find("Saves").GetComponent<Reload>();
        }
    }

    void Update()
    {
        if(SkillTreeLeveling.OpenSkilltree == false)
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

            if (playerControls.Pause.QuitGame.triggered)
            {
                QuitGame();
            }
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
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Level 1")
        {
            SetFloat("LastLevel", 1);
        }
        if (scene.name == "Level 2")
        {
            SetFloat("LastLevel", 2);
        }
        if (scene.name == "Level 3")
        {
            SetFloat("LastLevel", 3);
        }
        SceneManager.LoadScene("Main Menu");
        if (FindObjectOfType<ActivateQuest>())
        {
            FindObjectOfType<ActivateQuest>().changeSceneName("Main Menu");
        }
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

    public void SetFloat(string KeyName, float Value)
    {
        PlayerPrefs.SetFloat(KeyName, Value);
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
