using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardHat : BaseHatScript
{
    public GameObject smokeBomb;
    public GameObject potionThrow;
    Transform potionThrowPos;
    DrawPotionProjection TrajectoryLine;

    private static float abilityCooldown;

    static int SkillLevel = 1;
    public int currentLevel = SkillLevel;
    [Range(10,50)]
    private int _shotForce = 20;

    new void Start()
    {
        base.Start();
        TrajectoryLine = potionThrow.GetComponent<DrawPotionProjection>();
        potionThrowPos = potionThrow.transform;
        abilityCooldown = AbilityCooldownTime;
        
    }

    new void Update()
    {
        TrajectoryLine.ShowTrajectoryLine(potionThrowPos.position, (potionThrowPos.forward).normalized * _shotForce);

        base.Update();
    }
    new void OnEnable()
    {
        base.OnEnable();
        potionThrow.SetActive(true);
    }

    void OnDisable()
    {
        potionThrow.SetActive(false);

    }
    public override void HatAbility()
    {
        smokeBomb.SetActive(true);
        smokeBomb.transform.forward = potionThrowPos.forward;
        smokeBomb.GetComponent<SphereCollider>().isTrigger = true;
        smokeBomb.GetComponent<Rigidbody>().AddForce((potionThrowPos.forward).normalized * _shotForce, ForceMode.Impulse);
        smokeBomb.transform.parent = null;
        StartCoroutine(activateSmokeBomb(smokeBomb));
        base.HatAbility();

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

    public static float getAbilityCooldown()
    {
        return abilityCooldown;
    }
}
