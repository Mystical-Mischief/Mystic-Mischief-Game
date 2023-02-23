using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Teleport_Back_To_Hub : MonoBehaviour
{
    public GameObject Player;
    public GameObject TeleportTo;
    public GameObject StartTeleporter;
    
 
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Teleporter"))
        {
            Player.transform.position = TeleportTo.transform.position;
        }
 
        if (collision.gameObject.CompareTag("SecondTeleporter"))
        {
            Player.transform.position = StartTeleporter.transform.position;
        }
    }
}
 
