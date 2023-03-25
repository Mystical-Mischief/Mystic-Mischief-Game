using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialQuest : MonoBehaviour
{
    private List<GameObject> allObjs;
    public string[] ObjDescriptions;
    private GameObject currObj;
    private int currObjNumber;
    private string currObjDescription;
    private bool currObjCompelete;
    private TextMeshProUGUI tutorialText;
    private Quest questScript;

    public void startQuest(QuestInfo quest, Quest questScr)
    {
        tutorialText = questScr.text;
        foreach (GameObject gO in quest.objectiveItems)
        {
            allObjs.Add(gO);
        }
        questScript = questScr;
        currObj = allObjs[0];
        currObjNumber = 0;
        currObjDescription = ObjDescriptions[0];
        tutorialText.text = currObjDescription;
        currObjCompelete = false;
    }
    void Update()
    {
        if (currObjCompelete)
        {
            currObjNumber++;
            if (currObjNumber <= allObjs.Count - 1)
            {
                nextObj();
            }
            if (currObjNumber > allObjs.Count - 1)
            {
                questFinished();
            }
        }
        
    }
    public void nextObj()
    {
        currObjCompelete = false;
        currObj = allObjs[currObjNumber];
        currObjDescription = ObjDescriptions[currObjNumber];
        tutorialText.text = currObjDescription;
    }

    public void questFinished()
    {
        questScript.NextQuest();
        allObjs.Clear();
        currObj = null;
        currObjNumber = 0;
        currObjDescription = null;
        tutorialText.text = null;
        currObjCompelete = false;
    }
}
