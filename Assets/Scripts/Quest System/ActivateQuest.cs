using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateQuest : MonoBehaviour
{
    public string[] startQuests;
    public Quest questScript;
    static bool loadStartQuests = true;
    void Start()
    {
        questScript = FindObjectOfType<Quest>();
        if (loadStartQuests)
        {
            foreach (string currquest in startQuests)
            {
                questScript.ActivateQuest(currquest);
            }
            loadStartQuests = false;
        }
    }
    public void activateQuest(List<QuestInfo> quests)
    {
        foreach(QuestInfo currquest in quests)
        {
            questScript.ActivateQuest(currquest.questName);
        }
    }
}
