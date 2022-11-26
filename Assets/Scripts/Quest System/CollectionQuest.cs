using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectionQuest : MonoBehaviour
{
    public int totalNumberOfItems;
    public int currentNumberOfItems = 0;
    public List<GameObject> allItems;
    public bool allItemsCollected;
    Quest questScript;
    Inventory inv;

    public void startQuest(QuestInfo quest, Quest questScr)
    {
        Inventory inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        questScript = questScr;
        totalNumberOfItems = quest.objectiveItems.Length;
        foreach(GameObject gO in quest.objectiveItems)
        {
            allItems.Add(gO);
        }
    }
    private void Update()
    {

        if (allItemsCollected)
        {
            questComplete();
        }
    }
    public void questComplete()
    {
        questScript.NextQuest();
        totalNumberOfItems = 0;
        currentNumberOfItems = 0;
        allItemsCollected = false;
        allItems.Clear();
    }
}
