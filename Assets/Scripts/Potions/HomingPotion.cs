using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingPotion : MonoBehaviour
{
    public GameObject smokeScreen;
    public float stunTimer;
    public GameObject wizHat;

    private float speed = 10f;
    WizardHat wizardHat;
    int wizHatLevel;
    Transform potionIntialTransfrom;

    private void Awake()
    {
        wizHat = GameObject.FindGameObjectWithTag("WizardHat");
        wizardHat = wizHat.GetComponent<WizardHat>();
        wizHatLevel = wizardHat.getLevel();
        potionIntialTransfrom = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (wizardHat.closestEnemy != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, wizardHat.closestEnemy.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        smokeScreen.SetActive(true);
        smokeScreen.transform.parent = null;
        //gameObject.SetActive(false);
        //gameObject.transform.parent = wizHat.transform;
        //transform.localPosition = Vector3.zero;
        Destroy(gameObject);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            other.gameObject.GetComponent<BaseEnemyAI>().Stun(stunTimer);
            GetComponent<SphereCollider>().isTrigger = false;
        }
    }
}
