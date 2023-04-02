using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private PlayerController playerController;
    
    [SerializeField] private Button defaultBtn;

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.visible = true;
        defaultBtn.Select();
        Time.timeScale = 0f; //stops the game

        Cursor.lockState = CursorLockMode.None;
    }

    private void OnEnable()
    {
        Cursor.visible = true;
        defaultBtn.Select();
        Time.timeScale = 0f; //stops the game

        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Retry()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f; //resumes game time
        
        Cursor.lockState = CursorLockMode.Locked;
        AudioListener.pause = false;
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
