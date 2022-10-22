using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolderAI : BaseEnemyAI
{
    public bool HoldingItem;
    public GameObject CurrentHeldItem;

    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }
    
}
