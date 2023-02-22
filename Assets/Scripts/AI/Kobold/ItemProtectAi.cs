using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemProtectAi : MonoBehaviour
{
    public KoboldProtectAi kobold;
    private static InvisibilityHat _invisibilityHat;

    public bool Invisible;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag =="Player" && !kobold.holdingItem && !Invisible)
        {
            kobold.Protect = true;
        }
    }
    public static void FindInvisibilityHat(InvisibilityHat hat)
    {
        _invisibilityHat = hat;
    }

    private void Update()
    {
        if (_invisibilityHat != null && _invisibilityHat.IsInvisible())
        {
            Invisible = true;
        }
        else
        {
            Invisible = false;
        }
    }
}
