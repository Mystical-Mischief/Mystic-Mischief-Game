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
        smokeBomb.SetActive(true);
        smokeBomb.transform.forward = transform.forward;
        smokeBomb.GetComponent<SphereCollider>().isTrigger = true;
        smokeBomb.GetComponent<Rigidbody>().AddForce(new Vector3(transform.forward.x, 1, transform.forward.z).normalized * 10, ForceMode.Impulse);
        smokeBomb.transform.parent = null;
        StartCoroutine(activateSmokeBomb(smokeBomb));
        base.HatAbility();
    }
    IEnumerator activateSmokeBomb(GameObject bomb)
    {
        yield return new WaitForSeconds(0.5f);
        bomb.GetComponent<SphereCollider>().isTrigger = false;
    }
}
