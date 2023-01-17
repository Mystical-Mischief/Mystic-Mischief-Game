using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
{
    public static bool loadFromCheckPoint;
    public bool retry;
    public bool retrying;
    public bool reloaded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (retry == true)
        {
            loadFromCheckPoint = true;
        }
        if (loadFromCheckPoint == true)
        {
            retrying = true;
        }
        if (reloaded == true)
        {
            retry = false;
            loadFromCheckPoint = false;
            retrying = false;
        }
    }
}
