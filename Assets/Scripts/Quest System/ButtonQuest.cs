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
        //reads inputs and script
        correctInput = input;
        questScript = questScr;
        correctInput.Enable();
    }
    private void Update()
    {
        //if you press the button given, finish the quest
        if (correctInput != null && correctInput.WasPressedThisFrame())
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
