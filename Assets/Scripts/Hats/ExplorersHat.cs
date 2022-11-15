using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorersHat : BaseHatScript
{
    public GameObject[] currentDestinationItems;
    private GameObject closestDestination;
    private bool findCloseItem;
    new void Start()
    {
        if (closestDestination == null)
        {
            findCloseItem = true;
        }
        base.Start();
    }

    new void Update()
    {
        if (findCloseItem)
        {
            foreach(GameObject gO in currentDestinationItems)
            {
                if(Vector3.Distance(gO.transform.position, transform.position) < Vector3.Distance(closestDestination.transform.position, transform.position))
                {
                    closestDestination = gO;
                }
                findCloseItem = false;
            }
        }
        base.Update();
    }
    public override void HatAbility()
    {

        base.HatAbility();
    }

}
