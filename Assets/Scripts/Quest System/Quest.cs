using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quest : MonoBehaviour
{
    public Image questItem;
    public Color completedColor;
    public Color activeColor;
    public Color currentColor;
    public GameObject text;

    public static int tickets;

    public Quest[] allQuests;

    private void Start()
    {
        allQuests = FindObjectsOfType<Quest>();
        currentColor = questItem.color;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player")
        {
            tickets += 1;
            Destroy(gameObject);
            text.GetComponent<Text>().text = tickets + "/3";
        }

        if (tickets <= 3)
        {
            FinishQuest();
        }
    }
    void FinishQuest()
    {
        questItem.GetComponent<Button>().interactable = false;
        currentColor = completedColor;
        questItem.color = completedColor;
    }

    public void onQuestClick()
    {
        foreach(Quest quest in allQuests)
        {
            quest.questItem.color = quest.currentColor;
        }
        questItem.color = activeColor;
    }
}
