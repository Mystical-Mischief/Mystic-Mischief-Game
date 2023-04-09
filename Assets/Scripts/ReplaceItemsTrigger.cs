using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceItemsTrigger : MonoBehaviour
{
    public List<GameObject> itemsToReplace;
    public List<GameObject> newItems;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            SwapItems();
        }
    }
    void SwapItems()
    {
        foreach(GameObject gO in itemsToReplace)
        {
            gO.SetActive(false);
        }
        foreach (GameObject gO in newItems)
        {
            gO.SetActive(true);
        }
    }
}
