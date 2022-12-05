using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatSpawn : MonoBehaviour
{
    public GameObject Hat;
    public Inventory inv;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inv.PickedUpItems.Count >= 10)
        {
            Hat.SetActive(true);
        }
    }
}
