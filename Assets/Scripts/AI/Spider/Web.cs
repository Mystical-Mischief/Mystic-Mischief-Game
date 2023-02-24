using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : MonoBehaviour
{
    public float speed;
    public float stun = 3f;

    private Transform player;
    private Vector3 target;
    public bool hitPlayer { get; private set; }
    public bool isStun;
    float timer = 3f;

    int stunCounter = 0;

    ControlsforPlayer controls;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        controls = new ControlsforPlayer();
        controls.Enable();

        //grabs the location of the target
        target = new Vector3(player.position.x, player.position.y, player.position.z);
        hitPlayer = false;
    }


    private void FixedUpdate()
    {
        //function deals with how the web shoots and how long it exists before it is deleted.
        transform.position = Vector3.MoveTowards(transform.position, target, speed);
        if (!hitPlayer)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 2)
        {

            DestroyProjectile();
        }


        /* Code commented out becuase the counter to have 2 button presses does
         * not work for some reason.
         * 
        if (controls.Actions.Snatch.WasPressedThisFrame() && isStun == true)
        {
            if (stunCounter < 1)
            {
                stunCounter += 1;
            }
            else
            {
                player.GetComponent<ThirdPersonController>().canMove = true;
                isStun = false;
                stunCounter = 0;
                DestroyProjectile();
            }
        }*/

        //allows the player to break out of a stun with a single snatch button press
        if (controls.Actions.Snatch.IsPressed() && isStun == true)
        {
                player.GetComponent<PlayerController>().canMove = true;
                isStun = false;
                DestroyProjectile();
        }
    }



    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            hitPlayer = true;
            StartCoroutine(StunTimer(col.gameObject));
            isStun = true;
        }

    }

    //stuns player
    IEnumerator StunTimer(GameObject player)
    {
        print("player stunned");
        player.GetComponent<PlayerController>().canMove = false;
        yield return new WaitForSeconds(stun);
        player.GetComponent<PlayerController>().canMove = true;
        DestroyProjectile();
        stunCounter = 0;

    }
    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
