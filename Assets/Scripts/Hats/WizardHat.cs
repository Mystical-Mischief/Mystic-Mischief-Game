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
        //add this to the smoke bomb itself and collision logic to make a smoke field

        //thisBomb.GetComponent<Rigidbody>().AddForce(new Vector3(transform.forward.x, 1, transform.forward.z).normalized * 10, ForceMode.Impulse);
        base.HatAbility();
    }

}
