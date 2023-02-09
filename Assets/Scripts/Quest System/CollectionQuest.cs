using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectionQuest : MonoBehaviour
{
    private int totalNumberOfItems;
    private int currentNumberOfItems = 0;
    private List<GameObject> allItems = new List<GameObject>();
    private bool updateList;
    Quest questScript;
    Inventory inv;
    ControlsforPlayer controls;
    TextMeshProUGUI questText;
    GameObject NPC;
    QuestInfo Quest;
    private void Start()
    {
        controls = new ControlsforPlayer();
        controls.Enable();
    }
    public void startQuest(QuestInfo quest, Quest questScr)
    {
        questText = questScr.text;
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        questScript = questScr;
        totalNumberOfItems = quest.objectiveItems.Length;
        NPC = quest.NPC;
        Quest = quest;
    }
    private void Update()
    {
        if (inv.controls.Actions.Interact.WasReleasedThisFrame() || Quest.objectiveItems.Length == 1)
        {
            if(inv.currentHeldItem == Quest.objectiveItems[0] && Quest.objectiveItems.Length == 1)
            {
                questComplete();
            }
            updateList = true;
        }
        if (updateList)
        {
            currentNumberOfItems = 0;
            foreach(GameObject gO in allItems)
            {
                if(gO != null)
                {
                    currentNumberOfItems++;
                }
            }
            if(currentNumberOfItems == totalNumberOfItems)
            {
                questComplete();
            }
            updateList = false;
        }
        if (NPC.GetComponent<BirdInteraction>().ticketCount >= NPC.GetComponent<BirdInteraction>().TicketAmount)
        {
            questComplete();
            Destroy(NPC.GetComponent<BirdInteraction>());
            Destroy(NPC.GetComponent<Chest>());
        }
    }
    public void CollectedItem(GameObject item) 
    {
        allItems.Add(item);
        item.SetActive(false);
    }
    public void questComplete()
    {
        questScript.NextQuest();
        totalNumberOfItems = 0;
        currentNumberOfItems = 0;
        allItems.Clear();
        this.enabled = false;
    }
}
