using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivateQuest : MonoBehaviour
{
    public string[] startQuests;
    public Quest questScript;
    static bool activatedQuests;

    void Start()
    {
        questScript = FindObjectOfType<Quest>();
        if(!activatedQuests && PlayerPrefs.GetInt("Completed") != 1)
        {
            foreach (string currquest in startQuests)
            {
                questScript.ActivateQuest(currquest);
            }
            activatedQuests = true;
        }
        if(PlayerPrefs.GetInt("Completed") == 1)
        {
            questScript.UpdateText();
        }
    }
    public void activateQuest(List<QuestInfo> quests)
    {
        foreach(QuestInfo currquest in quests)
        {
            questScript.ActivateQuest(currquest.questName);
        }
    }
    public void ResetActivateQuests()
    {
        activatedQuests = false;
    }
}
