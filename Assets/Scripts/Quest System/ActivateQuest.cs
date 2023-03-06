using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivateQuest : MonoBehaviour
{
    public string[] startQuests;
    public Quest questScript;
    static string sceneName;
    void Start()
    {
        questScript = FindObjectOfType<Quest>();
        if(sceneName != SceneManager.GetActiveScene().name)
        {
            print(sceneName);
            if (sceneName != "Lose Screen")
            {
                foreach (string currquest in startQuests)
                {
                    questScript.ActivateQuest(currquest);
                }
                sceneName = SceneManager.GetActiveScene().name;
            }
        }
    }
    public void activateQuest(List<QuestInfo> quests)
    {
        foreach(QuestInfo currquest in quests)
        {
            questScript.ActivateQuest(currquest.questName);
        }
    }
}
