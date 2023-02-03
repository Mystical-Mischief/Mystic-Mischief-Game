using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDragonPerch : MonoBehaviour
{
    public Transform perch;
    public bool playerInRoom;
    public FireDragonScript Dragon;
    public GameObject dragonFace;
    public GameObject Player;
    public bool perchInAir;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(perch.transform.position, Dragon.transform.position);
        if (playerInRoom == true)
        {
            if(perchInAir == true)
            {
                Dragon.groundToAir = true;
            }
            Dragon.target = perch.transform;
            Dragon.UpdateDestination(perch.position);
            dragonFace.transform.LookAt(Player.transform);
            if (dist <= 1f)
            {
                Debug.Log("AtDestination");
                Dragon.canMove = false;
                Dragon.ps.Play(true);
            }
        }
        if (playerInRoom == false)
        {
            Dragon.canMove = true;
            Dragon.ps.Stop(true);
            // Dragon.NewRandomNumber();
        }
    }
    void OnCollisionStay(Collision other)
    {
        if(other.gameObject.CompareTag("Hat"))
        {
            playerInRoom = true;
        }
    }
        void OnCollisionExit(Collision other)
    {
        if(other.gameObject.CompareTag("Hat"))
        {
            playerInRoom = false;
        }
    }
}
