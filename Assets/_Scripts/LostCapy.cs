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

    [SerializeField] private List<string> Dialogues;
    [SerializeField] private string Repeat;
    public int defId;

    //**    ---Functions---    **//
    private void Start() {
        IdleAnim();
    }

    public void Interact() {
        if (id == CapyID.DEFAULT) {
            if (!GameManager.Instance.hasCapyBeenFound(defId)) {
                WaveAnim();
                if (Dialogues != null && Dialogues.Count > 0) {
                    GameManager.Instance.LoadPrompts(Dialogues);  
                    GameManager.Instance.Prompt();
                }else
                    GameManager.Instance.Prompt("Me encontraste!");
                
                GameManager.Instance.CapyFound(defId);
            }
            else {
                GameManager.Instance.Prompt("Hola otra vez");
            }
        }else if (!GameManager.Instance.hasCapyBeenFound(id)) {
            WaveAnim();
            GameManager.Instance.LoadPrompts(Dialogues);  
            GameManager.Instance.Prompt();
            GameManager.Instance.CapyFound(id);
        }
        else {
            GameManager.Instance.Prompt(Repeat);
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
