
using UnityEngine;
using UnityEngine.InputSystem;

public class PoopReticle : MonoBehaviour
{
    public GameObject reticle;
    private bool groundCheck;
    private RaycastHit hit;
    private Vector3 reticlePoint;
    [SerializeField]
    private PlayerController playerController;

    ControlsforPlayer controls;
    private void Start()
    {
        controls = new ControlsforPlayer();
        controls.Enable();
        controls.Actions.Poop.started += Reticle;
        controls.Actions.Poop.canceled += stop;
    }
    void Reticle(InputAction.CallbackContext obj)
    {
        if(!playerController.onGround)
        {
            groundCheck = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 50f);
            reticlePoint = hit.point;
            reticlePoint.y += 0.2f;
            reticle.transform.position = reticlePoint;
            reticle.SetActive(true);
            
        }

    }
    void stop(InputAction.CallbackContext obj)
    {
        reticle.SetActive(false);
    }
}
