using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorersHat : BaseHatScript
{
    public GameObject[] currentDestinationItems;
    public GameObject cameraForward;
    [SerializeField]
    private GameObject circleTool;
    [SerializeField]
    private Vector3 offsetHeight;
    [HideInInspector]
    public GameObject closestItem;
    private GameObject nextClosestItem;
    private bool findCloseItem;
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
        if (!activateHat)
        {
            circleTool.SetActive(false);
        }
        if (findCloseItem)
        {
            foreach(GameObject gO in currentDestinationItems)
            {
                if(closestItem == null)
                {
                    closestItem = gO;
                    continue;
                }
                if(Vector3.Distance(gO.transform.position, transform.position) < Vector3.Distance(closestItem.transform.position, transform.position))
                {
                    closestItem = gO;
                }
            }
            findCloseItem = false;
        }
        else
        {
            detectNextClosestItem();
        }
        base.Update();
    }
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
            if (gO == closestItem)
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
    public override void HatAbility()
    {
        cameraForward.transform.forward = Vector3.Lerp(cameraForward.transform.position, closestItem.transform.position - (transform.position + offsetHeight), 1);
        cameraForward.GetComponent<CameraLogic>().turn.x = (cameraForward.transform.rotation.eulerAngles.y);
        cameraForward.GetComponent<CameraLogic>().turn.y = (cameraForward.transform.rotation.eulerAngles.x);
        circleTool.SetActive(true);
        base.HatAbility();

    }

}
