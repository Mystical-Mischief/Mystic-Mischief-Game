using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletionTimer : MonoBehaviour
{
    public float time;
    void Start()
    {
        StartCoroutine(Delete());
    }
    IEnumerator Delete()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
