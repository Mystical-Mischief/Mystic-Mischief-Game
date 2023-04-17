using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PaperScraps : MonoBehaviour
{
    public GameObject PaperUI;
    public GameObject Text;
    public Button button;

    private ControlsforPlayer playerControls;

    bool canOpenPaper;
    public static bool OpenPaper;
    // Start is called before the first frame update
    void Start()
    {
        playerControls = new ControlsforPlayer();
        playerControls.Enable();
        OpenPaper = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canOpenPaper && playerControls.Actions.Interact.IsPressed())
        {
            OpenPaperUI();
        }
    }

    //When the player approaches the appropriately tagged object
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Inside collider");
        if (other.gameObject.tag == "Player")
        {
            canOpenPaper = true;
        }
    }

    private void OpenPaperUI()
    {
        if (PauseMenu.GameIsPaused == false)
        {
            button.Select();
            OpenPaper = true;
            Text.SetActive(true);
            PaperUI.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void BackButton()
    {
        PaperUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        OpenPaper = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canOpenPaper = false;
        }
    }
}
