using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowboyHat : BaseHatScript
{
    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
        base.Update();
    }
    public override void HatAbility()
    {
        print("yehaw *whip noise*");
        //figure out what the cowboy hat actually does LOL
        base.HatAbility();
    }
}
