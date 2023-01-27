using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBombLogic : MonoBehaviour
{
    public GameObject smokeScreen;
    public float stunTimer;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.AddForce(new Vector3(transform.forward.x, 1, transform.forward.z).normalized * 10, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        print("poof! smoke bomb here");
        Instantiate(smokeScreen, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "enemy")
        {
            other.gameObject.GetComponent<BaseEnemyAI>().Stun(stunTimer);
        }
    }
}
