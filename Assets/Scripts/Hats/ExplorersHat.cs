using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorersHat : BaseHatScript
{
    public List<GameObject> currentDestinationItems = new List<GameObject>();
    public GameObject cameraForward;
    [SerializeField]
    private GameObject circleTool;
    [SerializeField]
    private Vector3 offsetHeight;
    public GameObject closestItem;
    private GameObject nextClosestItem;
    private bool findCloseItem;
    public GameObject quest;
    private bool updateList = true;
    new void Start()
    {
        if (closestItem == null)
        {
            findCloseItem = true;
        }
        base.Start();
    }

    new void Update()
    {
        if (updateList)
        {
            currentDestinationItems.Clear();
            currentDestinationItems.AddRange(quest.GetComponent<Quest>().activeQuest.objectiveItems);
            if(quest.GetComponent<Quest>().activeQuest.questType != QuestType.Escort)
                closestItem = null;
            updateList = false;
        }
        //if the hat isnt active dont enable the circle guide
        if (!activateHat)
        {
            circleTool.SetActive(false);
            updateList = true;
            timeElapsed = 0;
        }
        if (activateHat)
        {
            circleTool.SetActive(true);
            updateList = true;
        }
        if (closestItem == null || !closestItem.activeInHierarchy)
        {
            closestItem = null;
            findCloseItem = true;
        }
        //finds the closest item out of the objectives so when you use the ability it will guide the player to the nearest object
        if (findCloseItem)
        {
            updateList = true;
            if (quest.GetComponent<Quest>().activeQuest.questType != QuestType.Escort)
            {
                foreach (GameObject gO in currentDestinationItems)
                {
                    if (!gO.activeInHierarchy)
                    {
                        continue;
                    }
                    if (closestItem == null)
                    {
                        closestItem = gO;
                        continue;
                    }
                    //sees if the object is closer to the current object. if it is make it the closest object
                    if (Vector3.Distance(gO.transform.position, transform.position) < Vector3.Distance(closestItem.transform.position, transform.position))
                    {
                        closestItem = gO;
                    }
                }
                findCloseItem = false;
            }
            else
            {
                closestItem = quest.GetComponent<Quest>().activeQuest.objectiveItems[1];
            }
        }
        else
        {
            if (!activateHat)
            {
                detectNextClosestItem();
            }
        }
        base.Update();
    }
    //detects the 2nd closes item so when the player gets to that item it will update and make it to where that is the closest item.
    //then it will rerun to see what the closest item is and what the next closest item is
    void detectNextClosestItem()
    {
        foreach (GameObject gO in currentDestinationItems)
        {
            if(nextClosestItem == closestItem)
            {
                nextClosestItem = null;
            }
            if(nextClosestItem == null)
            {
                nextClosestItem = gO;
                continue;
            }
            if (gO == closestItem || !gO.activeInHierarchy)
            {
                continue;
            }
            if(Vector3.Distance(gO.transform.position, transform.position) < Vector3.Distance(nextClosestItem.transform.position, transform.position))
            {
                nextClosestItem = gO;
            }
        }
        if(Vector3.Distance(nextClosestItem.transform.position, transform.position) < Vector3.Distance(closestItem.transform.position, transform.position))
        {
            findCloseItem = true;
        }
    }
    float timeElapsed;
    float lerpDuration = 0.5f;
    //moves the camera to face the nearest objective and turns the circle guide on so it can show it better. 
    public override void HatAbility()
    {
        if (timeElapsed < lerpDuration)
        {
            cameraForward.transform.forward = Vector3.Lerp(cameraForward.transform.position, closestItem.transform.position - (transform.position + offsetHeight), timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
        }
        cameraForward.GetComponent<CameraLogic>().turn.y = cameraForward.transform.localRotation.eulerAngles.x;
        cameraForward.GetComponent<CameraLogic>().turn.x = cameraForward.transform.localRotation.eulerAngles.y;


        base.HatAbility();
    }
}
