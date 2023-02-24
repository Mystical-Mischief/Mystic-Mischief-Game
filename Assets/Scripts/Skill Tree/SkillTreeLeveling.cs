using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeLeveling : MonoBehaviour
{
    public CowboyHat CowboyHat;
    public InvisibilityHat InvisibilityHat;
    public WizardHat WizardHat;

    public GameObject SkillTree;

    private void Start()
    {
        SkillTree.SetActive(false);
    }
    private void OnTriggerEnter(Collider other) // The skill tree ui pops up
    {
        if(other.gameObject.tag=="LevelUp")
        {
            SkillTree.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }
    private void OnTriggerExit(Collider other) // The skill tree ui disappears
    {
        if (other.gameObject.tag == "LevelUp")
        {
            SkillTree.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void WizardLv2()
    {
        int wizardLv = WizardHat.getLevel();
        if(wizardLv ==1) //if wizard hat level is 1 then level up hat, this happenes when you click wizard hat 2 button
        {
            WizardHat.LevelUp();
        }
    }

    public void WizardLv3()
    {
        int wizardLv = WizardHat.getLevel();
        if (wizardLv == 2) //if wizard hat level is 2 then level up hat, this happenes when you click wizard hat 3 button
        {
            WizardHat.LevelUp();
        }
    }

    public void WizardLv4A()
    {
        int wizardLv = WizardHat.getLevel();
        if (wizardLv == 3) //if wizard hat level is 3 then level up hat, this happenes when you click wizard hat 4A button
        {
            WizardHat.LevelUp();
        }
    }

    public void WizardLv4B()
    {
        int wizardLv = WizardHat.getLevel();
        if (wizardLv == 3) //if wizard hat level is 3 then level up hat, this happenes when you click wizard hat 4B button
        {
            WizardHat.LevelUp(); //the integer in script will be 5 but this is to easily manage levels
            WizardHat.LevelUp();
        }
    }
    //All CowboyLv and InvisibilityLv functions work the same as the WizardLv functions but applies to its respective hat
    public void CowboyLv2()
    {
        int cowboyLv = CowboyHat.getLevel();
        if(cowboyLv == 1)
        {
            CowboyHat.LevelUp();
        }
    }
    public void CowboyLv3()
    {
        int cowboyLv = CowboyHat.getLevel();
        if (cowboyLv == 2)
        {
            CowboyHat.LevelUp();
        }
    }

    public void CowboyLv4A()
    {
        int cowboyLv = CowboyHat.getLevel();
        if (cowboyLv == 3)
        {
            CowboyHat.LevelUp();
            CowboyHat.gunSlinger = true;
        }
    }

    public void CowboyLv4B()
    {
        int cowboyLv = CowboyHat.getLevel();
        if (cowboyLv == 3)
        {
            CowboyHat.LevelUp();
            CowboyHat.LevelUp();
            CowboyHat.pullEnemy = true;
        }
    }

    public void InvisibilityLv2()
    {
        int invisibilityLv = InvisibilityHat.getLevel();
        if(invisibilityLv == 1)
        {
            InvisibilityHat.LevelUp();
        }    
    }

    public void InvisibilityLv3()
    {
        int invisibilityLv = InvisibilityHat.getLevel();
        if (invisibilityLv == 2)
        {
            InvisibilityHat.LevelUp();
        }
    }

    public void InvisibilityLv4A()
    {
        int invisibilityLv = InvisibilityHat.getLevel();
        if (invisibilityLv == 3)
        {
            InvisibilityHat.LevelUp();
        }
    }

    public void InvisibilityLv4B()
    {
        int invisibilityLv = InvisibilityHat.getLevel();
        if (invisibilityLv == 3)
        {
            InvisibilityHat.LevelUp();
            InvisibilityHat.LevelUp();
        }
    }

}
