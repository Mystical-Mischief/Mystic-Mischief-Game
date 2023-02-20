using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public variables
    public int maxHealth = 4;
    public bool canMove;

    //private but can see in editor
    [SerializeField]

    float moveForce;
    float jumpForce;
    float maxSpeed;
    Camera playerCam;
    float stamina;
    float powerValue;
    [SerializeField] private GameObject camGround;
    [SerializeField] private GameObject camFly;
    [SerializeField] private GameObject dragonCamGround;
    [SerializeField] private GameObject dragonCamFly;
    [SerializeField] private GameObject flyingEffets;
    PlayerAnimation playerAnimation;
    //Private variables
    ControlsforPlayer controls;
    Rigidbody rb;
    float diveTim;
    private float originalMoveForce;
    private float originalMaxSpeed;
    Vector3 forceDirection = Vector3.zero;
    bool isGrounded;
    bool inWater;
    bool jumpInAir;


    //Scripts to make to seperate this shit
    //1) Audio manager & Animations
    //2) Lock on function
    //3) 
}
