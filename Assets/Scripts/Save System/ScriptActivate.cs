using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptActivate : MonoBehaviour
{
    public static bool Active;
    public bool isActivated;
    // Start is called before the first frame update
    void Start()
    {
        isActivated = Active;
    }

    // Update is called once per frame
    void Update()
    {
        isActivated = Active;
    }
}
