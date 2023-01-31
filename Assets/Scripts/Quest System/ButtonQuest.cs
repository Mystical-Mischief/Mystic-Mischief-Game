using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonQuest : MonoBehaviour
{
    public UnityEngine.InputSystem.InputAction correctInput;
    bool doInAir;
    Quest questScript;
    public void startQuest(QuestInfo quest, Quest questScr, UnityEngine.InputSystem.InputAction input)
    {
        correctInput = input;
        questScript = questScr;
        correctInput.Enable();
    }
    private void Update()
    {
        if (correctInput.WasPressedThisFrame())
        {
            finishQuest();
        }
    }
    void finishQuest()
    {
        correctInput = null;
        questScript.NextQuest();
        //this.enabled = false;
    }
}
