using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscortQuest : MonoBehaviour
{
    private GameObject escortItem;
    private GameObject escortLocation;
    public float targetDistance;
    Quest questScript;
    public void startQuest(QuestInfo quest, Quest questScr)
    {
        escortItem = quest.objectiveItems[0];
        escortLocation = quest.objectiveItems[1];
        questScript = questScr;
    }
    private void Update()
    {
        if(escortItem != null && Vector3.Distance(escortItem.transform.position, escortLocation.transform.position) < targetDistance)
        {
            questFinished();
        }
    }
    void questFinished()
    {
        escortItem = null;
        escortLocation = null;
        questScript.NextQuest();
        //this.enabled = false;
    }
}

