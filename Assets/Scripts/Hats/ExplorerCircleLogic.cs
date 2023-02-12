using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorerCircleLogic : MonoBehaviour
{
    [SerializeField]
    private ExplorersHat exploreHat;
    [SerializeField]
    private CowboyHat cowboyHat;
    [SerializeField]
    private RopeStretcher rope;
    private Transform startOfRopeTransform;
    private Vector3 cowboyAutoLockPos;
    private Transform closestItem;

    private void Update()
    {
        //looks at camera
        transform.LookAt(Camera.main.transform);
        //if the explorer hat is active go to the closest explorer hat item
        if (exploreHat.gameObject.activeSelf)
        {
            transform.position = exploreHat.closestItem.transform.position;
        }
        //if the cowboy hat is active 
        if (cowboyHat.gameObject.transform.parent.gameObject.activeSelf && cowboyHat.closestItem != null) 
        {
            startOfRopeTransform = rope.transform;
            closestItem = cowboyHat.closestItem.transform;
            //finds the distance between the rope and closest item
            float ropeAndItemDis = Vector3.Distance(startOfRopeTransform.position, closestItem.transform.position);
            //if item is within reach for the cowboy hat
            if(ropeAndItemDis < cowboyHat.MaxWhipDis())
            {
                transform.position = closestItem.transform.position;
            }
            //if item is not within reach then change the position of the auto lock circle to where the player wouldn't see
            else
            {
                transform.position = cowboyAutoLockPos;
            }
        }

    }
}
