using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHatScript : MonoBehaviour
{

    public bool HasCooldown;
    public float AbilityCooldownTime;
    public float SwitchCooldownTime;

    ControlsforPlayer controls;
    [HideInInspector]
    public bool activateHat;
    protected bool canUseHat = true;
    public void Start()
    {
        controls = new ControlsforPlayer();
        controls.Enable();
    }
    public void OnEnable()
    {
        if (HasCooldown)
        {
            canUseHat = false;
            StartCoroutine(HatCooldown(SwitchCooldownTime));
        }
    }
    public void Update()
    {
        activateHat = controls.Actions.ActivateHat.IsPressed();
        if (activateHat && canUseHat)
        {
            canUseHat = false;
            HatAbility();
        }
    }
    public virtual void HatAbility()
    {
        print("hat activate");
        if (HasCooldown)
        {
            StartCoroutine(HatCooldown(AbilityCooldownTime));
        }
        else
        {
            canUseHat = true;
        }
        
    }
    public virtual IEnumerator HatCooldown(float cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime);
        print("cooldown done!");
        canUseHat = true;
    }

}
