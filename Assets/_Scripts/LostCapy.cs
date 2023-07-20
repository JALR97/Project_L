using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostCapy : MonoBehaviour, IInteractable
{
    public enum CapyID {
        PUNK,
        ARTIST,
        DRESS,
        DEFAULT
    }
    //**    ---Components---    **//
    [SerializeField] private Animator anim;
    
    
    //**    ---Variables---    **//
    [SerializeField] private CapyID id;


    //**    ---Functions---    **//
    private void Start() {
        IdleAnim();
    }

    public void Interact() {
        if (!GameManager.Instance.isFound(id)) {
            WaveAnim();
            //Show message that capy was found
            //Debug.Log($"capy found: {id}");
            GameManager.Instance.Found(id);
        }
        else {
            //Debug.Log($"already found: {id}");
            //Show message when capy is found already. Trigger puzzle if there's one
        }
    }
    
    public void WalkAnim() {
        anim.CrossFade("walk", 0.2f, 0);
    }
    
    public void JumpAnim() {
        anim.CrossFade("jump", 0, 0);
    }
    
    public void IdleAnim() {
        anim.CrossFade("Idle", 0.2f, 0);
    }
    
    public void WaveAnim() {
        anim.CrossFade("found", 0.2f, 0);
    }
}
