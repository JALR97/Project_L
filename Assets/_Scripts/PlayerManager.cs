using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    //**    ---Functions---    **//
    public bool CanWalk() {
        return _currentState is States.IDLE or States.WALKING;
    }
    
    public bool CanJump() {
        //codigo para determinar si esta en el suelo
        bool OntheFloor = true;
        if (CanWalk() && OntheFloor) {
            return true;
        }
        return false;
    }

    public void WalkAnim() {
        
    }
    
    public void JumpAnim() {
        
    }
}
