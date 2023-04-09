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
    public Quest quest;
    private bool updateList = true;
    bool cameraUpdated = true;
    new void Start()
    {
        quest = FindObjectOfType<Quest>();
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
            currentDestinationItems.AddRange(quest.activeQuest.objectiveItems);
            if(quest.activeQuest.questType != QuestType.Escort)
                closestItem = null;
            updateList = false;
        }
        //if the hat isnt active dont enable the circle guide
        if (!activateHat)
        {
            circleTool.SetActive(false);
            if (!cameraUpdated)
            {
                cameraForward.GetComponent<CameraLogic>().enabled = true;
                cameraUpdated = true;
            }
            
            updateList = true;
            timeElapsed = 0;
            
        }
        if (activateHat)
        {
            circleTool.SetActive(true);
            updateList = true;
            cameraUpdated = false;
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
            if (quest.activeQuest.questType != QuestType.Escort)
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
                closestItem = quest.activeQuest.objectiveItems[1];
            }
        }
        //if you are NOT activating the hat. find the next closest item.
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
        //checks all items the hat can detect
        foreach (GameObject gO in currentDestinationItems)
        {
            //nullifiers for weird bugs
            if(nextClosestItem == closestItem)
            {
                nextClosestItem = null;
            }
            if(nextClosestItem == null)
            {
                nextClosestItem = gO;
                continue;
            }
            //if its not active in the hierarchy skip it
            if (gO == closestItem || !gO.activeInHierarchy)
            {
                continue;
            }
            //if the item is closer then the next closest item. it is the 2nd closest item
            if(Vector3.Distance(gO.transform.position, transform.position) < Vector3.Distance(nextClosestItem.transform.position, transform.position))
            {
                nextClosestItem = gO;
            }
        }
        //if the 2nd closest item is the closest item. recalculate which item is the closest
        if(Vector3.Distance(nextClosestItem.transform.position, transform.position) < Vector3.Distance(closestItem.transform.position, transform.position))
        {
            findCloseItem = true;
        }
    }
    float timeElapsed;
    public float lerpDuration;
    //moves the camera to face the nearest objective and turns the circle guide on so it can show it better. 
    public override void HatAbility()
    {
        cameraForward.GetComponent<CameraLogic>().enabled = false;
        //timer for the lerp. bigger the lerp duration is the slower it moves
        if (timeElapsed < lerpDuration)
        {
            print(timeElapsed / lerpDuration);
            cameraForward.transform.forward = Vector3.Lerp(cameraForward.transform.forward, closestItem.transform.position - (transform.position + offsetHeight), (timeElapsed / lerpDuration));
            timeElapsed += Time.deltaTime;
        }
        else
        {
            cameraForward.transform.forward = closestItem.transform.position - (transform.position + offsetHeight);
        }
        //changes the position so the camera doesnt jolt back to its old position
        cameraForward.GetComponent<CameraLogic>().turn.y = cameraForward.transform.localRotation.eulerAngles.x;
        cameraForward.GetComponent<CameraLogic>().turn.x = cameraForward.transform.localRotation.eulerAngles.y;


        base.HatAbility();
    }
}
