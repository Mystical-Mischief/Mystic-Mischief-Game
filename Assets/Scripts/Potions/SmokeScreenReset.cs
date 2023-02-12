using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeScreenReset : MonoBehaviour
{
    public GameObject potion;
    public float timer;

    private void OnEnable()
    {
        GetComponentInChildren<ParticleSystem>().Play();
        StartCoroutine(TimerCorotine());
    }
    IEnumerator TimerCorotine()
    {
        yield return new WaitForSeconds(timer);
        gameObject.transform.parent = potion.transform;
        transform.localPosition = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        gameObject.SetActive(false);
        GetComponentInChildren<ParticleSystem>().Stop();
    }
}
