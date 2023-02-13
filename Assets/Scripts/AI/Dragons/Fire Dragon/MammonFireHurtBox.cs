using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MammonFireHurtBox : MonoBehaviour
{
    public GameObject Player;
    //This code is because Mammon needs a seperate object for the dragons fire breath to collide with him. I don't know why the particles dont collide with anything in Player(new).

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position;
    }
}
