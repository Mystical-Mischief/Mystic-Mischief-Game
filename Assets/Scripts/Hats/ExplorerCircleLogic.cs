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
        transform.LookAt(Camera.main.transform);
        if (exploreHat.gameObject.activeSelf)
        {
            transform.position = exploreHat.closestItem.transform.position;
        }
        if (cowboyHat.gameObject.transform.parent.gameObject.activeSelf) 
        {
            transform.position = cowboyHat.closestItem.transform.position;
        }

    }
}
