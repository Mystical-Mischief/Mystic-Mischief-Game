using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHatScript : MonoBehaviour
{
    //sets up properties of the hat, like if it has a cool down,
    //the cooldown time for the hat ability and the time you have to wait before you can activate the hat when you switch to it
    public bool HasCooldown;
    public float AbilityCooldownTime;
    public float SwitchCooldownTime;

    internal ControlsforPlayer controls;
    [HideInInspector]
    public bool activateHat;
    protected bool canUseHat = true;

    [SerializeField]
    internal int SkillLevel = 1;
    public void Start()
    {
        controls = new ControlsforPlayer();
        controls.Enable();
    }
    public void OnEnable()
    {
        //checks to see if the hat has a cooldown. if it does activate the cooldown so the player cant activate it instantly
        if (HasCooldown)
        {
            canUseHat = false;
            StartCoroutine(HatCooldown(SwitchCooldownTime));
        }
    }
    public void Update()
    {
        activateHat = controls.Actions.ActivateHat.IsPressed();
        //if you can use the hat use the hat and start the cooldown
        if (activateHat && canUseHat)
        {
            canUseHat = false;
            HatAbility();
        }
    }
    //hat ability is activated. runs a basic print and cooldown function
    public virtual void HatAbility()
    {
        if (HasCooldown)
        {
            StartCoroutine(HatCooldown(AbilityCooldownTime));
        }
        else
        {
            canUseHat = true;
        }
        
    }
    //waits a certian amount of seconds until the player can use the hat again
    public virtual IEnumerator HatCooldown(float cooldownTime)
    {
        yield return new WaitForSeconds(cooldownTime);
        canUseHat = true;
    }

    public virtual void LevelUp()
    {
        SkillLevel++;
    }
    public int getLevel()
    {
        return SkillLevel;
    }

}
