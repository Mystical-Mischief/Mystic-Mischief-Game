using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletionScript : MonoBehaviour
{
    public float time;
    void Start()
    {
        StartCoroutine(Deletion());
    }
    IEnumerator Deletion()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

}
