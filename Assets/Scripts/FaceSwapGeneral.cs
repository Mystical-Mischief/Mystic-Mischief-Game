using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[Serializable]
public class AllMaterials
{
    public Material[] Materialss;
}

public class FaceSwapGeneral : MonoBehaviour
{
    public int arraySize;
    public AllMaterials[] AllMaterials;
    // public Material[Material[]];

    // public float change1;
    public Material safeMaterial;
    public GameObject[] models;
    private GameObject Model;
    private bool ChangeFace;
    public int FaceMaterialNum;
    public int EyeMaterialNum;
    void Start()
    {
        //finds player
        // Model = GameObject.FindGameObjectWithTag("Player");
    }
    void OnEnable()
    {
        //finds player and runs switch cooldown so player cant use hat ability right away
        // base.OnEnable();
        // Player = GameObject.FindGameObjectWithTag("Player");
        // StartCoroutine(HatCooldown(SwitchCooldownTime));
    }
    void OnDisable()
    {
        //if hat is changed turn player back to being visible
        if(Model != null)
        {
            becomeVisible();
        }  
    }
    void Update()
    {
        HatAbility();
    }
    public void HatAbility()
    {
        print("invisible hat poof!");
        //flips like a toggle. if player is invisible become visible. if player is visible become invisible
        ChangeFace = !ChangeFace;
        if (ChangeFace)
        {
            becomeInvisible();
        }
        else
        {
            becomeVisible();
        }
        // base.HatAbility();
    }
    //changes materials to look invisible and changes tag so enemies cant detect the player since they look for the "player" tag
    void becomeInvisible()
    {
        Material[] mats = new Material[] {safeMaterial, AllMaterials[1].Materialss[EyeMaterialNum] };
        models[0].GetComponent<SkinnedMeshRenderer>().materials = mats;
        // models[1].GetComponent<MeshRenderer>().material = FaceMaterialsB[1];
        // Model.transform.tag = "Untagged";
        // Model.layer = 0;
    }
    //changes materials to look visible and changes tag so enemies can see player again
    void becomeVisible()
    {
        // Material[] mats = new Material[] { FaceMaterialsA[0], faceMaterial };
        // models[0].GetComponent<SkinnedMeshRenderer>().materials = AllMaterials[0];
        // models[1].GetComponent<MeshRenderer>().material = FaceMaterialsA[1];
        // Model.transform.tag = "Player";
        // Model.layer = 8;
    }
}
