using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WDProjectrileScript : MonoBehaviour
{
    public GameObject particles;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExecuteAfterTime(1));

    }

        IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(particles, transform.position, transform.rotation);
        Destroy(gameObject, 0);
    }
}
