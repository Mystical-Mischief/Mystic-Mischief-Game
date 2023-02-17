using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibilityHat : BaseHatScript
{
    [Header("0 is for mammon and 1 is for the cloak")]
    [SerializeField]
    private Material[] InvisMaterials;
    [SerializeField]
    private Material[] NormalMaterials;
    public Material faceMaterial;
    public GameObject[] models;
    private GameObject Player;
    private bool isInvisible;
    private bool _snatching;
    [SerializeField]
    private float _timer;
    

    new void Start()
    {
        //finds player
        Player = GameObject.FindGameObjectWithTag("Player");
        base.Start();
    }
    new void OnEnable()
    {
        //finds player and runs switch cooldown so player cant use hat ability right away
        base.OnEnable();
        Player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(HatCooldown(SwitchCooldownTime));
    }
    void OnDisable()
    {
        //if hat is changed turn player back to being visible
        if(Player != null)
        {
            becomeVisible();
        }  
    }
    public override void HatAbility()
    {
        //flips like a toggle. if player is invisible become visible. if player is visible become invisible
        isInvisible = !isInvisible;
        if (isInvisible)
        {
            becomeInvisible();
        }
        else
        {
            becomeVisible();
        }
        base.HatAbility();


    }
    //changes materials to look invisible and changes tag so enemies cant detect the player since they look for the "player" tag
    void becomeInvisible()
    {
        Material[] mats = new Material[] { InvisMaterials[0], faceMaterial };
        models[0].GetComponent<SkinnedMeshRenderer>().materials = mats;
        models[1].GetComponent<MeshRenderer>().material = InvisMaterials[1];
        Player.transform.tag = "Untagged";
        Player.layer = 0;
        if(SkillLevel > 1)
        {
            ThirdPersonController playerController = Player.GetComponent<ThirdPersonController>();
            playerController.IncreaseSpeed(2);
        }
    }
    //changes materials to look visible and changes tag so enemies can see player again
    void becomeVisible()
    {
        Material[] mats = new Material[] { NormalMaterials[0], faceMaterial };
        models[0].GetComponent<SkinnedMeshRenderer>().materials = mats;
        models[1].GetComponent<MeshRenderer>().material = NormalMaterials[1];
        Player.transform.tag = "Player";
        Player.layer = 8;
        if (SkillLevel > 1)
        {
            ThirdPersonController playerController = Player.GetComponent<ThirdPersonController>();
            playerController.SetSpeedToNormal();
        }
    }

    private new void Update()
    {
        base.Update();
        _snatching = controls.Actions.Snatch.IsPressed();
        if(isInvisible)
        {
            if(_snatching)
            {
                becomeVisible();
                isInvisible = false;
                return;
            }
            //StartCoroutine(TimeInvisible(_timer));
            //isInvisible = false;
        }
        
    }
    IEnumerator TimeInvisible(float time)
    {
        yield return new WaitForSeconds(time);
        becomeVisible();
    }
}
