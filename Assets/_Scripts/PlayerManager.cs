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
        if (newState == GameManager.GameState.Exploring) 
            _currentState = States.IDLE;
        else 
            _currentState = States.DISABLED;

    }
    
    public bool IsActive() {
        return _currentState != States.DISABLED;
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
        if (_currentState == States.JUMPING) {
            return;
        }
        anim.CrossFade("walk", 0.2f, 0);
    }
    
    public void JumpAnim() {
        anim.CrossFade("jump", 0, 0);
    }
    
    public void IdleAnim() {
        if (_currentState == States.JUMPING) {
            return;
        }
        anim.CrossFade("Idle", 0.2f, 0);
    }

    private void Start() {
        _currentState = States.IDLE;
        if (GameManager.Instance.PlayerSpawn == -1) {
            //Nothing
        }else if (GameManager.Instance.PlayerSpawn == 0) {
            transform.position = GameObject.FindGameObjectWithTag("STPoint").transform.position; 
            GameManager.Instance.Prompt("Encuentra a los capybaras de la familia! Recolecta los champiñones");
        }
        else {
            transform.position = GameObject.FindGameObjectWithTag("HoPoint").transform.position;
        }
        GameManager.Instance.HideUI();
    }
    
    private IEnumerator JumpCooldown() {
        float timerS = Time.time;
        while (Time.time - timerS <= 0.5f) {
            yield return null;
        }
        while (!IsGrounded()) {
            yield return null;
        }
        
        SwitchState(States.IDLE);
        IdleAnim();
    }
    
    //Debug
    public void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(transform.position - transform.up * rcBoxMaxDistance, rcBoxSize*2);
    }
}
