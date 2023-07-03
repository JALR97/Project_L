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
    [SerializeField] private GameObject player;
    
    //**    ---Work Variables---    **//
    private States _currentState;
    [SerializeField] private float rcBoxMaxDistance;
    [SerializeField] private Vector3 rcBoxSize;

    //**    ---Script Functions---    **//
    public bool CanWalk() {
        return _currentState is States.IDLE or States.WALKING;
    }
    
    public bool CanJump() {
        bool grounded = Physics.BoxCast(transform.position, rcBoxSize, -transform.up, transform.rotation, rcBoxMaxDistance, LayerMask.GetMask("Terrain"));
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

    //Debug
    public void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(transform.position - transform.up * rcBoxMaxDistance, rcBoxSize*2);
    }

    private void Start() {
        _currentState = States.IDLE;
    }
}
