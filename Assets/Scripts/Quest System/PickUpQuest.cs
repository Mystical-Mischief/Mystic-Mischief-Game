using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpQuest : MonoBehaviour
{
    GameObject player;
    Quest questScript;
    GameObject itemToHold;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void startQuest(QuestInfo quest, Quest questScr)
    {
        questScript = questScr;
        itemToHold = quest.objectiveItems[0];
    }

    void Update()
    {
        if(player.GetComponent<Inventory>().currentHeldItem == itemToHold)
        {
            questFinished();
        }
    }
    void questFinished()
    {
        enabled = false;
        questScript.NextQuest();
        itemToHold = null;

    }
}
