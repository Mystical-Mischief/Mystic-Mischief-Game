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
    Input,
    PickUp,
    Destroy
}
[Serializable]
public class QuestInfo
{
    public string questName;
    public string questDescription;
    public Sprite questImage;
    public QuestType questType;
    public GameObject[] objectiveItems;
    public bool mainQuest;

    [Header("Use only if turn in quest is true")]
    public GameObject NPC;
    [Header("Use only if input quest")]
    public UnityEngine.InputSystem.InputAction input;
    public GameObject[] rewards;
    public MonoBehaviour[] scriptsToActivate;
    [SerializeField]
    public GameObject[] ObjToDeactivate;
    public bool priorityQuest;

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
    public TextMeshProUGUI text;
    public Image questImage;
    public TextMeshProUGUI sideQuestText;
    public Sprite defaultImage;

    public QuestInfo[] allQuests;
    public List<QuestInfo> currentQuests = new List<QuestInfo>();
    public QuestInfo activeQuest;
    public List<GameObject> sideQuests;

    private static GameObject currQuestObj;
    private void Start()
    {
        interactor = GameObject.FindGameObjectWithTag("Player").GetComponent<Interactor>();
        if(allQuests.Length == 0)
            UpdateText();
        //currentColor = questItem.color;
    }
    //activates the quest based off the name
    public void ActivateQuest(string nameOfQuest)
    {
        if(nameOfQuest != null)
            nameOfQuest = nameOfQuest.ToUpper();
        //checks all the quests in the quest list to find it
        foreach (QuestInfo currquest in allQuests)
        {
            //checks to see if the names match
            if(currquest.questName.ToUpper() == nameOfQuest)
            {
                //if so activate the quest and add it to the list of current quests
                currquest.active = true;
                currentQuests.Add(currquest);
                //if there is no active quest make it the active quest
                if (activeQuest.questName == string.Empty || currquest.priorityQuest)
                {
                    if(activeQuest.questName != null)
                    {
                        activeQuest.active = false;
                    }
                    activeQuest = currquest;
                    if (activeQuest.NPC != null && !activeQuest.NPC.activeInHierarchy)
                    {
                        activeQuest.NPC.SetActive(true);
                    }
                    print(activeQuest.questName);
                    processQuest(activeQuest);
                }
            }
        }
        UpdateText();
    }
    //Updates the quest and sees if its ready to be complete or not
    public void UpdateQuest()
    {
        //if its a npc quest finish the quest
        if (activeQuest.questType == QuestType.TalkToNPC)
        {
            activeQuest.completed = true;
            NextQuest();
        }
        //updates the quest
        if (activeQuest.completed)
        {
            print("quest updated");
            activeQuest.submitQuest = true;
            NextQuest();
        }
    }
    void processQuest(QuestInfo currquest)
    {
        //checks to see which quest to activate out of all the quests
        switch (currquest.questType)
        {
            case QuestType.Tutorial:
                questScript[0].enabled = true;
                questScript[0].GetComponent<TutorialQuest>().startQuest(currquest, this);
                break;
            case QuestType.TalkToNPC:
                questScript[1].enabled = true;
                questScript[1].GetComponent<TalkToNPCQuest>().startQuest(currquest, this);
                break;
            case QuestType.Collection:
                questScript[2].enabled = true;
                questScript[2].GetComponent<CollectionQuest>().startQuest(currquest, this);
                break;
            case QuestType.Escort:
                questScript[3].enabled = true;
                questScript[3].GetComponent<EscortQuest>().startQuest(currquest, this);
                break;
            case QuestType.Input:
                questScript[4].enabled = true;
                questScript[4].GetComponent<ButtonQuest>().startQuest(currquest, this, activeQuest.input);
                break;
            case QuestType.PickUp:
                questScript[5].enabled = true;
                questScript[5].GetComponent<PickUpQuest>().startQuest(currquest, this);
                break;
            case QuestType.Destroy:
                questScript[6].enabled = true;
                questScript[6].GetComponent<DestroyedQuest>().startQuest(currquest, this);
                break;
            default:
                print("quest not found");
                break;
        }
        UpdateText();
    }
    //moves onto the next quest and sees if its ready to turn in or not
    public void NextQuest()
    {

        //if there are rewards activate them
        if(activeQuest.rewards.Length != 0)
        {
            foreach(GameObject gO in activeQuest.rewards)
            {
                if(gO != null)
                {
                    gO.SetActive(true);
                }
            }
        }
        if (activeQuest.scriptsToActivate.Length != 0)
        {
            foreach (MonoBehaviour mB in activeQuest.scriptsToActivate)
            {
                if (mB != null)
                {
                    mB.enabled = true;
                }
            }
        }
        if (activeQuest.ObjToDeactivate.Length != 0)
        {
            foreach(GameObject gO in activeQuest.ObjToDeactivate)
            {
                if (gO != null)
                {
                    gO.SetActive(false);
                }
            }
        }
        GetComponent<AudioSource>().Play();
        //remove the quest from the list
        currentQuests.Remove(activeQuest);
        //if there is another quest activate the next quest on the list
        if (currentQuests.Count != 0)
        {
            activeQuest = currentQuests[0];
            activeQuest.active = true;
            processQuest(activeQuest);
            print(activeQuest.questName);
        }
        //otherwise turn on all the side quests
        else
        {
            foreach(GameObject gO in sideQuests)
            {
                gO.SetActive(true);
            }
        }
        UpdateText();
    }
    //Update the text of both the active quests and the other quests
    public void UpdateText()
    {
        if(activeQuest.active)
        {
            text.text = $"{activeQuest.questName}:\n{activeQuest.questDescription}";
            questImage.sprite = activeQuest.questImage;
        }
        if(currentQuests.Count == 0)
        {
            text.text = "No Active Quests.\nMaybe take some time to explore or go through the portal!";
            questImage.sprite = defaultImage;
        }
    }
}
