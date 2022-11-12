using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibilityHat : BaseHatScript
{
    public Material InvisMaterial;
    public Material NormalMaterial;
    public GameObject modelRender;
    private GameObject Player;
    private bool isInvisible;
    new void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }
    new void OnEnable()
    {
        base.OnEnable();
        Player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(HatCooldown(SwitchCooldownTime));
    }
    void OnDisable()
    {
        if(Player != null)
        {
            Player.GetComponent<MeshRenderer>().material = NormalMaterial;
            isInvisible = false;
        }  
    }
    public override void HatAbility()
    {
        print("invisible hat poof!");
        isInvisible = !isInvisible;
        if (isInvisible)
        {
            modelRender.GetComponent<SkinnedMeshRenderer>().materials[0] = InvisMaterial;
            Player.transform.tag = "Untagged";
        }
        else
        {
            modelRender.GetComponent<SkinnedMeshRenderer>().materials[0] = NormalMaterial;
            Player.transform.tag = "Player";
        }
        base.HatAbility();
    }
}
