using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletionTimerPoop : MonoBehaviour
{
    //general script for when you want something deleted after a certian amount of time after spawning in. 
    public float time;
    public ParticleSystem ps;
    public ParticleSystem ps2;
    void Start()
    {
        StartCoroutine(deletionTimer());
    }
    IEnumerator deletionTimer()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
        ps.Play(true);
        ps2.Play(true);
    }

    public void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject,0.2f);
        ps.Play(true);
        ps2.Play(true);
    }
}
