using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[Serializable]
public class AllMaterialsCleverBird
{
    public Material[] Materialss;
}

public class CleverBirdFaceChange : MonoBehaviour
{
    public int arraySize;
    public AllMaterialsCleverBird[] AllMaterials;
    // public Material[Material[]];

    // public float change1;
    public Material safeMaterial;
    public GameObject[] models;
    private GameObject Model;
    private bool ChangeFace;
    public int FaceMaterialNum;
    public int EyeMaterialNum1;
    public int EyeMaterialNum2;
    private float nextActionTime = 2f;
    public float period = 2f;
    private float transitionTime = 5f;
    public bool talking;
    private float randomNumber;
    float lastNumber;
    public bool face2;
    public bool face3;
    public bool face4;
    
    void Start()
    {

    }

    void OnEnable()
    {

    }
    void OnDisable()
    {
        //if hat is changed turn player back to being visible
        if(Model != null)
        {
            ChangeEyes2();
        }  
    }
    void Update()
    {
        if (Time.time > nextActionTime ) {
        nextActionTime += period;
         // execute block of code here
         HatAbility();
        }
        if (talking == true)
        {
            period = randomNumber;
        }
        else {period = 2f;}
        if (face2 == true && face3 == false && face4 == false)
        {
            EyeMaterialNum1 = 2;
        }
        if (face2 == false && face3 == true && face4 == false)
        {
            EyeMaterialNum1 = 3;
        }
                if (face2 == false && face3 == false && face4 == true)
        {
            EyeMaterialNum1 = 4;
        }
                if (face2 == false && face3 == false && face4 == false)
        {
            EyeMaterialNum1 = 1;
        }
    }
    public void HatAbility()
    {
        //flips like a toggle. if player is invisible become visible. if player is visible become invisible
        ChangeFace = !ChangeFace;
        if (ChangeFace)
        {
            ChangeEyes1();
        }
        else
        {
            ChangeEyes2();
        }
    }
    //changes materials to look invisible and changes tag so enemies cant detect the player since they look for the "player" tag
    void ChangeEyes1()
    {
        Material[] mats = new Material[] {safeMaterial, AllMaterials[0].Materialss[EyeMaterialNum1] };
        models[0].GetComponent<SkinnedMeshRenderer>().materials = mats;
        // models[1].GetComponent<MeshRenderer>().material = FaceMaterialsB[1];
    }
    //changes materials to look visible and changes tag so enemies can see player again
    void ChangeEyes2()
    {
        Material[] mats = new Material[] {safeMaterial, AllMaterials[0].Materialss[EyeMaterialNum2]};
        models[0].GetComponent<SkinnedMeshRenderer>().materials = mats;
        // models[1].GetComponent<MeshRenderer>().material = FaceMaterialsA[1];
        if (talking == true)
        {
            NewRandomNumber();
        }
    }
        public virtual void NewRandomNumber()
    {
        randomNumber = UnityEngine.Random.Range(0.3f, 1f);
        if (randomNumber == lastNumber)
        {
            randomNumber = UnityEngine.Random.Range(0.3f, 1f);
        }
        lastNumber = randomNumber;
    }
}
