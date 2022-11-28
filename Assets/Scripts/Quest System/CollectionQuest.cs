using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectionQuest : MonoBehaviour
{
    public int totalNumberOfItems;
    public int currentNumberOfItems = 0;
    public List<GameObject> allItems;
    public bool updateList;
    Quest questScript;
    Inventory inv;

    public void startQuest(QuestInfo quest, Quest questScr)
    {
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        questScript = questScr;
        totalNumberOfItems = quest.objectiveItems.Length;
        foreach(GameObject gO in quest.objectiveItems)
        {
            allItems.Add(gO);
        }
    }
    private void Update()
    {
        //if(inv.)
        if (updateList)
        {
            currentNumberOfItems = 0;
            foreach(GameObject gO in allItems)
            {
                if (inv.PickedUpItems.Contains(gO))
                {
                    currentNumberOfItems++;
                }
            }
            updateList = false;
        }
        if (currentNumberOfItems >= totalNumberOfItems)
        {
            questComplete();
        }
    }
    public void questComplete()
    {
        questScript.NextQuest();
        totalNumberOfItems = 0;
        currentNumberOfItems = 0;
        allItems.Clear();
    }
}
