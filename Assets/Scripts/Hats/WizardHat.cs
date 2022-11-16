using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardHat : BaseHatScript
{
    public GameObject smokeBomb;
    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
        base.Update();
    }
    new void OnEnable()
    {
        base.OnEnable();
    }
    public override void HatAbility()
    {
        print("wizard hat ability wooosh");
        GameObject thisBomb = Instantiate(smokeBomb, transform.position, Quaternion.identity);
        thisBomb.transform.forward = transform.forward;
        thisBomb.GetComponent<SphereCollider>().isTrigger = true;
        StartCoroutine(activateSmokeBomb(thisBomb));
        base.HatAbility();
    }
    IEnumerator activateSmokeBomb(GameObject bomb)
    {
        yield return new WaitForSeconds(0.5f);
        bomb.GetComponent<SphereCollider>().isTrigger = false;
    }
}
