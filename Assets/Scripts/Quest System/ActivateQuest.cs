using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateQuest : MonoBehaviour
{
    public string[] startQuests;
    public string[] questsToActivate;
    public Quest questScript;
    void Start()
    {
        questScript = GameObject.FindObjectOfType<Quest>();
        foreach(string quest in startQuests)
        {
            questScript.ActivateQuest(quest);
        }
    }
    public void activateQuest()
    {
        foreach(string quest in questsToActivate)
        {
            questScript.ActivateQuest(quest);
        }
    }
}
