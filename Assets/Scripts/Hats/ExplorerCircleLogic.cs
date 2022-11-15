using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorerCircleLogic : MonoBehaviour
{
    [SerializeField]
    private ExplorersHat exploreHat;

    private void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.position = exploreHat.closestItem.transform.position;
    }
}
