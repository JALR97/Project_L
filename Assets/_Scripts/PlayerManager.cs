using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //**    ---Enums---    **//
    public enum States {
        DISABLED,
        IDLE,
        WALKING,
        JUMPING
    }
    
    //**    ---Components---    **//
    // [SerializeField] private PlayerController _playerController;
    [SerializeField] private Interactor _interactor;
    [SerializeField] private GameObject player;
    [SerializeField] private LayerMask terrain;
    
    //**    ---Work Variables---    **//
    private States _currentState;
    [SerializeField] private float rcBoxMaxDistance;
    [SerializeField] private Vector3 rcBoxSize;

    //**    ---Script Functions---    **//
    public bool CanWalk() {
        return _currentState is States.IDLE or States.WALKING;
    }
    
    public bool CanJump() {
        bool grounded = Physics.BoxCast(transform.position, rcBoxSize, -transform.up, transform.rotation, rcBoxMaxDistance, terrain);
        if (CanWalk() && grounded) {
            return true;
        }
        return false;
    }

    public void SwitchState(States newState) {
        if (_currentState == newState) {
            return;
        }

        switch (newState) {
            case States.DISABLED:
            case States.IDLE:
                IdleAnim();
                break;
            case States.WALKING:
                WalkAnim();
                break;
            case States.JUMPING:
                JumpAnim();
                break;
        }
        _currentState = newState;
    }
    
    public void WalkAnim() {
        
    }
    
    public void JumpAnim() {
        
    }
    
    public void IdleAnim() {
        
    }
    
    private void Update() {
        //PRUEBA -- Deberia moverse a PlayerController
        if (Input.GetKeyDown(KeyCode.E)) {
            _interactor.Interact();
        }
    }

    private void Start() {
        _currentState = States.IDLE;
    }
    
    //Debug
    public void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(transform.position - transform.up * rcBoxMaxDistance, rcBoxSize*2);
    }
}
