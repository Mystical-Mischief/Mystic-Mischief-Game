using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : MonoBehaviour
{
    public float speed;

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

        if (transform.position.x == target.x && transform.position.y == target.y && transform.position.z == target.z)
        {
            StunPlayer();
            DestroyProjectile();
        }
    }

    void StunPlayer()
    {

    }

    void OnTriggerEnter3D(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
