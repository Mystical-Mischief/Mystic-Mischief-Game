using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;
    [SerializeField] private InteractionpromptUI _interactionpromptUI;

   private readonly Collider[] _colliders = new Collider[3];
   [SerializeField] private int _numFound;

   private IInteractable _interactable;
    ControlsforPlayer controls;
    private void Start()
    {
        controls = new ControlsforPlayer();
        controls.Enable();
    }
    private void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, 
        _interactableMask);

       if (_numFound > 0)
       {
            _interactable = _colliders[0].GetComponent<IInteractable>();

            if(_interactable != null)
            {
                if (!_interactionpromptUI.IsDisplayed) _interactionpromptUI.Setup(_interactable.InteractionPrompt);

                if(controls.Actions.Interact.WasPerformedThisFrame()) _interactable.Interact(this);
            }
       }
       else
       {
        if (_interactable != null) _interactable = null;
        if(_interactionpromptUI.IsDisplayed) _interactionpromptUI.Close();
       }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }


}
