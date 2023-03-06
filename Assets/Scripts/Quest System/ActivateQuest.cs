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
            foreach (string quest in startQuests)
            {
                questScript.ActivateQuest(quest);
            }
            loadStartQuests = false;
        }
    }
    public void activateQuest(List<QuestInfo> quests)
    {
        foreach(QuestInfo quest in quests)
        {
            questScript.ActivateQuest(quest.questName);
        }
    }
}
