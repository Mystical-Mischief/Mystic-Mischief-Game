using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private int speed = 5;
    public float jumpForce = 10f;
    Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, Vector3.up, speed * Time.deltaTime);
        rigidbody.velocity = new Vector3( 0f, jumpForce, 0f);
    }
}

