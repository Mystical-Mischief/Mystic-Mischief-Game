using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TalkToNPCQuest : MonoBehaviour
{
    public GameObject NPC;
    public Quest questScript;
    public void startQuest(QuestInfo quest, Quest questScr)
    {
        questScript = questScr;
        NPC = quest.objectiveItems[0];
    }
    void Update()
    {
        if (NPC.GetComponent<Dialogue>().enabled)
        {
            questFinished();
        }
    }
    void questFinished()
    {
        NPC = null;
        questScript.NextQuest();
    }
}
