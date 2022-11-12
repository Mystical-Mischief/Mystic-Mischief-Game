using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : MonoBehaviour
{
    public float speed;
    public float stun = 3f;

    private Transform player;
    private Vector3 target;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector3(player.position.x, player.position.y, player.position.z);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed);
    }


   
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            
            StartCoroutine(StunTimer(col.gameObject));
            
        }
    }
    IEnumerator StunTimer(GameObject player)
    {
        player.GetComponent<ThirdPersonController>().TakeDamage(1);
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
