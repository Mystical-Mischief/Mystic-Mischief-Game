using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTarget : MonoBehaviour
{
    public Transform Target;
    public float CamSpeed;
    private float time;
    private GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        bool isFlying = !player.GetComponent<ThirdPersonController>().isGrounded;
        if (!isFlying)
        {
            transform.position = Vector3.Lerp(transform.position, Target.position, Time.deltaTime * CamSpeed);
        }
        else
        {
            Vector3 tempTrans = new Vector3(Target.transform.position.x, Target.transform.position.y + 1, Target.transform.position.z);
            transform.position = Vector3.Lerp(transform.position, tempTrans, Time.deltaTime * CamSpeed);
        }
    }
}
