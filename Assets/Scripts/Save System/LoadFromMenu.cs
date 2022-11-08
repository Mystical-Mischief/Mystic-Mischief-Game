using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadFromMenu : MonoBehaviour
{
    public static bool LoadMenu;
    public bool Loadenabled;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (LoadMenu == true)
        {
            Loadenabled = true;
        }
    }
    public virtual void Loadmenu()
    {
        LoadMenu = true;
    }
}
