using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonQuest : MonoBehaviour
{
    UnityEngine.InputSystem.InputAction correctInput;
    bool doInAir;
    Quest questScript;
    public void startQuest(QuestInfo quest, Quest questScr, UnityEngine.InputSystem.InputAction input)
    {
        correctInput = input;
        questScript = questScr;
    }
    private void Update()
    {
        if (correctInput.WasPerformedThisFrame())
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
