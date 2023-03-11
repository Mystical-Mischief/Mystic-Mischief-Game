using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public float loot;
    bool addToTreasure;
    // Start is called before the first frame update
    void Start()
    {
        addToTreasure = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(addToTreasure)
        {
            Treasure.SetValue(loot);
            addToTreasure=false;
        }
    }
    public void AddToLoot(float Value)
    {
        loot+=Value;
        addToTreasure=true;
    }
}
