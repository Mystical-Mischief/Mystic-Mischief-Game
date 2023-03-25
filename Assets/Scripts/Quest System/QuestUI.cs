using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    public GameObject OpenScroll;
    public GameObject ClosedScroll;
    private bool questOpen;
    private ControlsforPlayer controls;
    void Start()
    {
        controls = new ControlsforPlayer();
        controls.Enable();
    }

    //if the player presses the quest button. open if its closed and close it if its open
    void Update()
    {
        if (controls.Pause.QuestAction.WasPerformedThisFrame())
        {
            questOpen = !questOpen;
            if (questOpen)
            {
                OpenScroll.SetActive(true);
                ClosedScroll.SetActive(false);
            }
            else
            {
                OpenScroll.SetActive(false);
                ClosedScroll.SetActive(true);
            }
        }   
    }
}
