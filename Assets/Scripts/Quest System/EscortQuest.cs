using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscortQuest : MonoBehaviour
{
    public GameObject escortItem;
    public GameObject escortLocation;
    public float targetDistance;
    Quest questScript;
    public void startQuest(QuestInfo quest, Quest questScr)
    {
        escortItem = quest.objectiveItems[0];
        escortLocation = quest.objectiveItems[1];
        questScript = questScr;
    }
}
