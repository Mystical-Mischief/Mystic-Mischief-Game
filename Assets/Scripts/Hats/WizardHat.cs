using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardHat : BaseHatScript
{
    public GameObject[] smokeBomb;
    public Transform[] potionThrowPos;
    static int SkillLevel = 1;
    public int currentLevel = SkillLevel;

    private GameObject closestEnemy;
    private GameObject nextClosestEnemy;

    private bool findCloseEnemy;

    public List<GameObject> Enemies = new List<GameObject>();
    new void Start()
    {
        base.Start();
    }
    private void Awake()
    {
        foreach(GameObject g0 in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Enemies.Add(g0);
        }
    }
    new void Update()
    {
        currentLevel = SkillLevel;
        base.Update();
    }
    new void OnEnable()
    {
        base.OnEnable();
    }
    public override void HatAbility()
    {
        if(SkillLevel<3)
        {
            smokeBomb[0].SetActive(true);
            smokeBomb[0].transform.forward = potionThrowPos[0].forward;
            smokeBomb[0].GetComponent<SphereCollider>().isTrigger = true;
            smokeBomb[0].GetComponent<Rigidbody>().AddForce(new Vector3(potionThrowPos[0].forward.x, 1, potionThrowPos[0].forward.z).normalized * 10, ForceMode.Impulse);
            smokeBomb[0].transform.parent = null;
            StartCoroutine(activateSmokeBomb(smokeBomb[0]));
            base.HatAbility();
        }
        if(SkillLevel==3)
        {
            for(int i = 0; i < smokeBomb.Length; i++)
            {
                smokeBomb[i].SetActive(true);
                smokeBomb[i].transform.forward = potionThrowPos[i].forward;
                smokeBomb[i].GetComponent<SphereCollider>().isTrigger = true;
                smokeBomb[i].GetComponent<Rigidbody>().AddForce(new Vector3(potionThrowPos[i].forward.x, 1, potionThrowPos[i].forward.z).normalized * 10, ForceMode.Impulse);
                smokeBomb[i].transform.parent = null;
                StartCoroutine(activateSmokeBomb(smokeBomb[i]));
                base.HatAbility();
            }
        }
        if(SkillLevel!=5)
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

    void detectNextClosestEnemy()
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
    }
}
