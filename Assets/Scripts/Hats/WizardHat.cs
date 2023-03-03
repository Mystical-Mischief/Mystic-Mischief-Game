using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardHat : BaseHatScript
{
    public GameObject[] smokeBomb;
    public GameObject[] potionThrow;
    public Transform[] potionThrowPos;
    public DrawPotionProjection[] TrajectoryLine; 

    static int SkillLevel = 1;
    public int currentLevel = SkillLevel;
    [Range(10,50)]
    private int _shotForce = 20;

    public GameObject closestEnemy;
    private GameObject nextClosestEnemy;

    private bool findCloseEnemy;

    public List<GameObject> Enemies = new List<GameObject>();
    new void Start()
    {
        base.Start();
        
    }

    new void Update()
    {
        currentLevel = SkillLevel;

        if(SkillLevel ==2)
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
        foreach(GameObject gO in potionThrow)
        {
            gO.SetActive(true);
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
            smokeBomb[0].transform.position = Vector3.MoveTowards(smokeBomb[0].transform.position, closestEnemy.transform.position, 2 * Time.deltaTime);
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
    }
    public int getLevel()
    {
        return SkillLevel;
    }

    /**void detectNextClosestEnemy()
    {
        foreach (GameObject gO in Enemies)
        {
            if (!gO.activeInHierarchy)
            {
                continue;
            }
            if (nextClosestEnemy == null)
            {
                nextClosestEnemy = gO;
                continue;
            }
            if (gO == closestEnemy)
            {
                continue;
            }
            if (Vector3.Distance(gO.transform.position, transform.position) < Vector3.Distance(nextClosestEnemy.transform.position, transform.position))
            {
                nextClosestEnemy = gO;
            }
        }
        if (Vector3.Distance(nextClosestEnemy.transform.position, transform.position) < Vector3.Distance(closestEnemy.transform.position, transform.position))
        {
            findCloseEnemy = true;
        }
    }**/
}
