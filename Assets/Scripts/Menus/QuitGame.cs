using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    ControlsforPlayer controls;
    public GameObject Saves;
    private void Start()
    {
        controls = new ControlsforPlayer();
        controls.Enable();
    }
    private void Update()
    {
        if (controls.MenuActions.Quit.WasPerformedThisFrame())
        {
            Saves.GetComponent<SaveGeneral>().SaveEnemy();
            print("quitting game");
            Application.Quit();
        }
    }
}
