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
    [SerializeField] private LayerMask terrain;
    
    //**    ---Work Variables---    **//
    private States _currentState;
    [SerializeField] private float rcBoxMaxDistance;
    [SerializeField] private Vector3 rcBoxSize;

    //**    ---Script Functions---    **//
    private void Awake() {
        _currentState = States.IDLE;
        GameManager.OnGameStateChange += StateChange;
    }

    private void OnDestroy() {
        GameManager.OnGameStateChange -= StateChange;
    }

    private void StateChange(GameManager.GameState newState) {
        if (newState != GameManager.GameState.Exploring) {
            _currentState = States.DISABLED;
        }
        else {
            _currentState = States.IDLE;
        }
        
    }
    
    public bool IsActive() {
        return _currentState is States.IDLE or States.WALKING;
    }
    
    public bool CanJump() {
        bool grounded = Physics.BoxCast(transform.position, rcBoxSize, -transform.up, transform.rotation, rcBoxMaxDistance, terrain);
        if (_currentState != States.JUMPING && IsActive() && grounded) {
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
                //Cooldown for the jump.
                
                SwitchState(States.IDLE);
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

    private void Start() {
        _currentState = States.IDLE;
    }
    
    //Debug
    public void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(transform.position - transform.up * rcBoxMaxDistance, rcBoxSize*2);
    }
}
