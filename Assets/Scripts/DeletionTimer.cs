using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletionTimer : MonoBehaviour
{
    //general script for when you want something deleted after a certian amount of time after spawning in. 
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
