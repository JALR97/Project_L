using System.Collections;
using System.Collections.Generic;using Unity.VisualScripting;
using UnityEngine;

public class Interactor : MonoBehaviour {
    //**    ---Components---    **//
    [SerializeField] private LayerMask _interactableMask;

    //**    ---Variables---    **//
    private Collider[] _colliders = new Collider[5];
    [SerializeField] private float _interactionRadius;

    //**    ---Functions---    **//
    public void Interact() {
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, _interactionRadius, _colliders, _interactableMask);

        if (numColliders > 0) {
            Debug.Log("Interactuar con objeto");
        }
        else
            Debug.Log("Nothing to interact");
    }
    
    //**    ---Debug---    **//
    public void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _interactionRadius);
    }
}