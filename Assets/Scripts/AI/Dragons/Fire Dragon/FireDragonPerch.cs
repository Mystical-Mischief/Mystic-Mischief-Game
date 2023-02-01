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

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRoom == true)
        {
            Dragon.target = perch.transform;
            Dragon.UpdateDestination(perch.position);
            dragonFace.transform.LookAt(Player.transform);
            if (Dragon.ai.enabled && Dragon.ai.remainingDistance < 0.5f && Dragon.atDestination == false)
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
