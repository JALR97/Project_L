using System;
using System.Collections;
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
    [SerializeField] private Animator anim;
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

    public bool IsGrounded() {
        return Physics.BoxCast(transform.position, rcBoxSize, -transform.up, transform.rotation, rcBoxMaxDistance, terrain);
    }
    
    public bool CanJump() {
        if (_currentState != States.JUMPING && IsActive() && IsGrounded()) {
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
                StartCoroutine(JumpCooldown());
                break;
        }
        _currentState = newState;
    }
    
    public void WalkAnim() {
        Debug.Log("walk anim");
        anim.CrossFade("walk", 0.5f, 0);
    }
    
    public void JumpAnim() {
        Debug.Log("jump anim");
        anim.CrossFade("jump", 0, 0);
    }
    
    public void IdleAnim() {
        Debug.Log("Idle anim");
        anim.CrossFade("Idle", 0.5f, 0);
    }

    private void Start() {
        _currentState = States.IDLE;
    }

    private IEnumerator JumpCooldown() {
        float timerS = Time.time;
        while (Time.time - timerS <= 0.5f) {
            yield return null;
        }
        Debug.Log("Wait for ground");
        while (!IsGrounded()) {
            yield return null;
        }
        Debug.Log("Back on ground");
        SwitchState(States.IDLE);
    }
    
    //Debug
    public void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(transform.position - transform.up * rcBoxMaxDistance, rcBoxSize*2);
    }
}
