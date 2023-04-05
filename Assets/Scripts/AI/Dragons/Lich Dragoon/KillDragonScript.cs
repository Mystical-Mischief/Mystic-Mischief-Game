using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillDragonScript : MonoBehaviour
{
    public BasicLichDragon Dragon;
    public bool KillDragon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnEnable()
    {
        Dragon.Die = true;
    }
}
