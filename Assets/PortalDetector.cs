using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalDetector : MonoBehaviour
{
    static string nextLevel;
    private void Start()
    {
        if(nextLevel == string.Empty)
        {
            nextLevel = GetComponent<PortalLogic>().SceneName;
        }
    }
    public void UpdatePortal()
    {
        if(nextLevel == "Level 1")
        {
            nextLevel = "Level 2";
        }
        if (nextLevel == "Level 2")
        {
            nextLevel = "Level 3";
        }
        GetComponent<PortalLogic>().SceneName = nextLevel;
    }
}
