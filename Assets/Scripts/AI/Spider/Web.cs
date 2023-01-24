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
        transform.position = Vector3.MoveTowards(transform.position, target, speed);
        if (!hitPlayer)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            DestroyProjectile();
        }

        if (controls.Actions.Snatch.WasPerformedThisFrame() && isStun == true)
        {
            player.GetComponent<ThirdPersonController>().canMove = true;
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
    private void OnTriggerStay(Collider other)
    {

        if (!isStun && other.gameObject.tag != "Player")
        {
            DestroyProjectile();
        }
    }

    //stuns player
    IEnumerator StunTimer(GameObject player)
    {
        print("player stunned");
        player.GetComponent<ThirdPersonController>().canMove = false;
        yield return new WaitForSeconds(stun);
        player.GetComponent<ThirdPersonController>().canMove = true;
        DestroyProjectile();

    }
    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
