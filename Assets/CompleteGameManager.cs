using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteGameManager : MonoBehaviour
{
    public GameObject[] objectsToRemove;
    public GameObject[] objectsToAdd;
    void Start()
    {
        if (PlayerPrefs.GetInt("Completed") == 1) 
        {
            foreach(GameObject gO in objectsToRemove)
            {
                Destroy(gO);
            }
            foreach(GameObject gO in objectsToAdd)
            {
                gO.SetActive(true);
            }
        }

    }
}
