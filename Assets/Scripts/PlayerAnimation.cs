using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Rigidbody rb;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (rb.velocity.magnitude >= 8)
        // {
        //     animator.SetFloat("RunSpeed", 2f);
        // // animator.SetTrigger("Launch");
        // }
        //         if (rb.velocity.magnitude >= 6 && rb.velocity.magnitude < 8)
        // {
        //     animator.SetFloat("RunSpeed", 1f);
        // // animator.SetTrigger("Launch");
        // }
        // if (rb.velocity.magnitude < 6)
        // {
        //     animator.SetFloat("RunSpeed", 0f);
        // }
    }
}
