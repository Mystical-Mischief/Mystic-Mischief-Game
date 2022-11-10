using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WDProjectrileScript : MonoBehaviour
{
    public GameObject particles;
    public int TimeToDestroy = 2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExecuteAfterTime(5));

    }

        IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(particles, transform.position, transform.rotation);
        Destroy(gameObject, TimeToDestroy);
    }

        private void OnCollisionEnter(Collision other)
    {
            if (other.gameObject.tag == "Player" && gameObject.GetComponentInParent<WaterDragonAi>().rangedAttacked == true)
        {  
            other.gameObject.GetComponent<ThirdPersonController>().TakeDamage(1);
        }
    }
}
