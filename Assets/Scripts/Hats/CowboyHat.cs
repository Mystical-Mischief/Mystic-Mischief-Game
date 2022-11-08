using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowboyHat : BaseHatScript
{
    private GameObject player;
    private GameObject obj;

    LineRenderer lineRenderer;

    bool isWhipping;
    bool isObjAttached;

    float whipDis;

    float whipSpeed = 1000f;
    float maxWhipDis = 15f;
    float x_Offset = -0.7701938f;
    float y_Offset = 0.62326f;

    float z_Offset = 0.03f;

    float objectDis = 2f;

    Vector3 originalPos;

    Rigidbody rb;

    new void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
        lineRenderer = GetComponent<LineRenderer>();
        isWhipping = false;
        isObjAttached = false;
        whipDis = 0f;

        originalPos = new Vector3(player.transform.position.x + x_Offset, player.transform.position.y + y_Offset, player.transform.position.z + z_Offset);

        rb = GetComponent<Rigidbody>();
    }

    new void Update()
    {
        originalPos = new Vector3(player.transform.position.x + x_Offset, player.transform.position.y + y_Offset, player.transform.position.z + z_Offset);
        lineRenderer.SetPosition(0, originalPos);
        lineRenderer.SetPosition(1,transform.position);
        base.Update();
        ReturnWhip();
        BringObject();
    }
    public override void HatAbility()
    {
        
        if(!isWhipping && !isObjAttached)
        {
            StartWhip();
        }
        
        base.HatAbility();
    }

    private void StartWhip()
    {
        isWhipping = true;
        GetComponent<Rigidbody>().isKinematic = false;
        rb.AddForce(transform.forward * whipSpeed);
    }

    private void ReturnWhip()
    {
        if(isWhipping)
        {
            whipDis = Vector3.Distance(transform.position,originalPos);
            if(whipDis > maxWhipDis || isObjAttached)
            {
                rb.isKinematic = true;
                transform.position = originalPos;
                isWhipping = false;
            }
        }
    }

    void OnTriggerEnter (Collider collider)
    {
        if(collider.gameObject.tag.Equals("Object"))
        {
            isObjAttached = true;
            obj = collider.gameObject;
        }
    }

    private void BringObject()
    {
        if(isObjAttached)
        {
            Vector3 finalPos = new Vector3(originalPos.x,obj.transform.position.y,originalPos.z + z_Offset - objectDis); 
            obj.transform.position = Vector3.MoveTowards(obj.transform.position,finalPos, maxWhipDis);
            isObjAttached = false;
        }
    }
}
