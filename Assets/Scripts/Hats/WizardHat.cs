using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardHat : BaseHatScript
{
    public GameObject[] smokeBomb;
    public GameObject[] potionThrow;
    public Transform[] potionThrowPos;
    public DrawPotionProjection[] TrajectoryLine;

    public GameObject HomingPotion;

    static int SkillLevel = 5;
    public int currentLevel = SkillLevel;
    [Range(10,50)]
    private int _shotForce = 20;

    public GameObject[] Enemies;
    public GameObject closestEnemy;

    float distanceFromEnemy;

    new void Start()
    {
        base.Start();
        Enemies = GameObject.FindGameObjectsWithTag("enemy");
        
    }

    new void Update()
    {
        currentLevel = SkillLevel;
        detectClosestEnemy();
        if (SkillLevel ==2)
        {
            TrajectoryLine[0].ShowTrajectoryLine(potionThrowPos[0].position, (potionThrowPos[0].forward).normalized * _shotForce);
        }
        else if(SkillLevel ==3)
        {
            for(int i = 0; i < TrajectoryLine.Length/2; i++)
                TrajectoryLine[i].ShowTrajectoryLine(potionThrowPos[i].position, (potionThrowPos[i].forward).normalized * _shotForce);
        }
        else if (SkillLevel == 4)
        {
            for (int i = 0; i < TrajectoryLine.Length; i++)
                TrajectoryLine[i].ShowTrajectoryLine(potionThrowPos[i].position, (potionThrowPos[i].forward).normalized * _shotForce);
        }

        base.Update();
    }
    new void OnEnable()
    {
        base.OnEnable();
        if(SkillLevel !=5)
        {
            foreach (GameObject gO in potionThrow)
            {
                gO.SetActive(true);
            }
        }
    }

    void OnDisable()
    {
        foreach (GameObject gO in potionThrow)
        {
            gO.SetActive(false);
        }

    }
    public override void HatAbility()
    {
        if(SkillLevel<3)
        {
            smokeBomb[0].SetActive(true);
            smokeBomb[0].transform.forward = potionThrowPos[0].forward;
            smokeBomb[0].GetComponent<SphereCollider>().isTrigger = true;
            smokeBomb[0].GetComponent<Rigidbody>().AddForce((potionThrowPos[0].forward).normalized * _shotForce, ForceMode.Impulse);
            smokeBomb[0].transform.parent = null;
            StartCoroutine(activateSmokeBomb(smokeBomb[0]));
            base.HatAbility();
        }
        if(SkillLevel==3)
        {
            for(int i = 0; i < smokeBomb.Length/2; i++)
            {
                smokeBomb[i].SetActive(true);
                smokeBomb[i].transform.forward = potionThrowPos[i].forward;
                smokeBomb[i].GetComponent<SphereCollider>().isTrigger = true;
                smokeBomb[i].GetComponent<Rigidbody>().AddForce((potionThrowPos[i].forward).normalized * _shotForce, ForceMode.Impulse);
                smokeBomb[i].transform.parent = null;
                StartCoroutine(activateSmokeBomb(smokeBomb[i]));
                base.HatAbility();
            }
        }
        if (SkillLevel == 4)
        {
            for (int i = 0; i < smokeBomb.Length; i++)
            {
                smokeBomb[i].SetActive(true);
                smokeBomb[i].transform.forward = potionThrowPos[i].forward;
                smokeBomb[i].GetComponent<SphereCollider>().isTrigger = true;
                smokeBomb[i].GetComponent<Rigidbody>().AddForce((potionThrowPos[i].forward).normalized * _shotForce, ForceMode.Impulse);
                smokeBomb[i].transform.parent = null;
                StartCoroutine(activateSmokeBomb(smokeBomb[i]));
                base.HatAbility();
            }
        }
        if (SkillLevel==5)
        {
            //HomingPotion.SetActive(true);
            GameObject homing = Instantiate(HomingPotion, potionThrowPos[0].position, Quaternion.identity);
            homing.SetActive(true);
            base.HatAbility();

        }
    }
    IEnumerator activateSmokeBomb(GameObject bomb)
    {
        yield return new WaitForSeconds(0.5f);
        bomb.GetComponent<SphereCollider>().isTrigger = false;
    }
    public virtual void LevelUp()
    {
        SkillLevel++;
        if(SkillLevel==5)
        {
            foreach (GameObject gO in potionThrow)
            {
                gO.SetActive(false);
            }
        }
    }
    public int getLevel()
    {
        return SkillLevel;
    }

    void detectClosestEnemy()
    {
       for(int i = 0; i< Enemies.Length; i++)
        {
            distanceFromEnemy = Vector3.Distance(transform.position, Enemies[i].transform.position);
            if(distanceFromEnemy <= 10000)
            {
                closestEnemy = Enemies[i];
            }
        }
    }
}
