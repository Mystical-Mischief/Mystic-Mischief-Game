using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBHatLevel1 : CowboyHat
{
    public bool isGrounded;

    // Start is called before the first frame update
    new void Start()
    {
        // base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        // base.Update();
    }

    public override void HatAbility()
    {
        if (isGrounded == true)
        {
        GetComponent<SphereCollider>().enabled = true;
        // originalWorldPosition = transform.position;
        // rb.isKinematic = false;
        // rb.AddForce(transform.forward * whipStrength, ForceMode.Impulse);
        base.HatAbility();
        }
    }

    public virtual void IsGrounded()
    {
        float bufferDistance = 0.1f;
        float groundCheckDistance = ((GetComponent<CapsuleCollider>().height/2)+bufferDistance) * 1.5f;
        Debug.DrawRay(transform.position, -Vector3.up * (groundCheckDistance), Color.green);
        RaycastHit hit;

        if(Physics.Raycast(transform.position, -Vector3.up, out hit, groundCheckDistance))
        {
            if (hit.collider)
            {
                isGrounded=true;
            }
        }
        else
        {
            isGrounded = false;
        }
    }
}
