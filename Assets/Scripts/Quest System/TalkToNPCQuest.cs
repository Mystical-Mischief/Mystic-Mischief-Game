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
        print(quest.NPC);
        NPC = quest.NPC;
    }
    void questFinished()
    {
        NPC = null;
        questScript.NextQuest();
    }
}
