using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorerCircleLogic : MonoBehaviour
{
    [SerializeField]
    private ExplorersHat exploreHat;
    [SerializeField]
    private CowboyHat cowboyHat;

    private void Update()
    {
        //looks at camera
        transform.LookAt(Camera.main.transform);
        //if the explorer hat is active go to the closest explorer hat item
        if (exploreHat.gameObject.activeSelf)
        {
            transform.position = exploreHat.closestItem.transform.position;
        }
        //if the cowboy hat is active go to the closest cowboy hat item
        if (cowboyHat.gameObject.transform.parent.gameObject.activeSelf) 
        {
            transform.position = cowboyHat.closestItem.transform.position;
        }

    }
}
