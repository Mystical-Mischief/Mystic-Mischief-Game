using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TalkToNPCQuest : MonoBehaviour
{
    private GameObject NPC;
    private Quest questScript;
    public void startQuest(QuestInfo quest, Quest questScr)
    {
        questScript = questScr;
    }
    void questFinished()
    {
        questScript.NextQuest();
    }
}
