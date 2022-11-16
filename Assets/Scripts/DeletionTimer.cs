using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletionTimer : MonoBehaviour
{
    public float time;
    void Start()
    {
        StartCoroutine(deletionTimer());
    }
    IEnumerator deletionTimer()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
