using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpQuest : MonoBehaviour
{
    Inventory inv;
    Quest questScript;
    GameObject itemToHold;
    private void Start()
    {
        inv = FindObjectOfType<Inventory>();
    }
    public void startQuest(QuestInfo quest, Quest questScr)
    {
        questScript = questScr;
        itemToHold = quest.objectiveItems[0];
    }

    void Update()
    {
        if(inv.GetComponent<Inventory>().currentHeldItem == itemToHold)
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
