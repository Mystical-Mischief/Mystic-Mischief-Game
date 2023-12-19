using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedQuest : MonoBehaviour
{
    private GameObject destroyedItem;
    Quest questScript;
    public void startQuest(QuestInfo quest, Quest questScr)
    {
        destroyedItem = quest.objectiveItems[0];
        questScript = questScr;
    }
    void Update()
    {
        if(destroyedItem == null)
        {
            questFinished();
        }
    }
    void questFinished()
    {
        destroyedItem = null;
        questScript.NextQuest();
        this.enabled = false;
    }
}
