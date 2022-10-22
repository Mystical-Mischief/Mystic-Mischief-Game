using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    ControlsforPlayer controls;
    private void Start()
    {
        controls = new ControlsforPlayer();
        controls.Enable();
    }
    private void Update()
    {
        if (controls.MenuActions.Quit.WasPerformedThisFrame())
        {
            print("quitting game");
            Application.Quit();
        }
    }
}
