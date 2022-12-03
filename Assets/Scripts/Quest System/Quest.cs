using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public enum QuestType
{
    Tutorial,
    TalkToNPC,
    Collection,
    Escort,
}
[Serializable]
public class QuestInfo
{
    public string questName;
    public string questDescription;
    public QuestType questType;
    public GameObject[] objectiveItems;
    public bool turnInQuest;
    [Header("Use only if turn in quest is true")]
    public GameObject NPC;
    [HideInInspector]
    public bool active;
    [HideInInspector]
    public bool completed;
    [HideInInspector]
    public bool submitQuest;
}
public class Quest : MonoBehaviour
{
    [HideInInspector]
    public Interactor interactor;
    //tutorial is 0, Talk to NPC is 1, Collection is 2, Escort is 3 
    public MonoBehaviour[] questScript;
    //public Image questItem;
    //public Color completedColor;
    //public Color currentColor;
    public TextMeshProUGUI text;
    public TextMeshProUGUI questText;

    public QuestInfo[] allQuests;
    public List<QuestInfo> currentQuests = new List<QuestInfo>();
    [HideInInspector]
    public QuestInfo activeQuest;

    private void Start()
    {
        interactor = GameObject.FindGameObjectWithTag("Player").GetComponent<Interactor>();
        //currentColor = questItem.color;
    }
    public void ActivateQuest(string nameOfQuest)
    {
        foreach (QuestInfo quest in allQuests)
        {
            if(quest.questName == nameOfQuest)
            {
                currentQuests.Add(quest);
                if(activeQuest.questName == string.Empty)
                {
                    activeQuest = quest;
                    processQuest(activeQuest);
                }
            }
        }
        
    }
    public void UpdateQuest()
    {
        if (activeQuest.questType == QuestType.TalkToNPC)
        {
            activeQuest.completed = true;
        }
        if (activeQuest.completed)
        {
            print("quest updated");
            activeQuest.submitQuest = true;
            questText.text = string.Empty;
            NextQuest();
        }
    }
    void processQuest(QuestInfo quest)
    {
        switch (quest.questType)
        {
            case QuestType.Tutorial:
                questScript[0].enabled = true;
                questScript[0].GetComponent<TutorialQuest>().startQuest(activeQuest, this);
                break;
            case QuestType.TalkToNPC:
                questScript[1].enabled = true;
                questScript[1].GetComponent<TalkToNPCQuest>().startQuest(activeQuest, this);
                break;
            case QuestType.Collection:
                questScript[2].enabled = true;
                questScript[2].GetComponent<CollectionQuest>().startQuest(activeQuest, this);
                break;
            case QuestType.Escort:
                questScript[3].enabled = true;
                questScript[3].GetComponent<EscortQuest>().startQuest(activeQuest, this);
                break;
            default:
                print("quest not found");
                break;
        }
        text.text = $"{activeQuest.questName}:\n{activeQuest.questDescription}";
    }
    public void NextQuest()
    {
        if (!activeQuest.completed)
        {
            activeQuest.completed = true;
        }
        if (!activeQuest.turnInQuest)
        {
            activeQuest.submitQuest = true;
        }
        if (activeQuest.turnInQuest)
        {
            text.text = $"Return to {activeQuest.NPC.name}";
        }
        if (activeQuest.submitQuest == true)
        {
            currentQuests.Remove(activeQuest);
            if (currentQuests.Count != 0)
            {
                activeQuest = currentQuests[0];
                activeQuest.active = true;
                processQuest(activeQuest);
            }
            else
            {
                activeQuest.active = false;
            }
            FinishQuest();
        }
    }
    public void FinishQuest()
    {
        if(activeQuest.active)
        {
            text.text = $"{activeQuest.questName}:\n{activeQuest.questDescription}";
        }
        else
        {
            text.text = "No Active Quests";
        }
    }

    //rework this
    public void onQuestClick()
    {
        /*
        foreach(QuestInfo quest in allQuests)
        {
            //quest.questItem.color = quest.currentColor;
        }
        questItem.color = activeColor;
        */
    }
}
