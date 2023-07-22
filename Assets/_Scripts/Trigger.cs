using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trigger : MonoBehaviour, IInteractable
{
    [SerializeField] private enum Type {
        CABLES,
        CASA,
        Yggdrasil,
        DAD
    }
    //**    ---Components---    **//
    [SerializeField] private Type _type;
    
    //**    ---Variables---    **//
    //  [[ balance control ]] 
    
    //  [[ internal work ]] 
    private bool flag = false;
    
    //**    ---Properties---    **//
    
    
    //**    ---Functions---    **//
    public void Interact() {
        switch (_type) {
            case Type.CABLES:
                if (!GameManager.Instance.hasBeenSolved(PuzzleManager.Puzzle.CABLES)) {
                    GameManager.Instance.TriggeredPuzzle(PuzzleManager.Puzzle.CABLES);
                }
                break;
            case Type.CASA:
                GameManager.Instance.hint_UI.SetActive(false);
                GameManager.Instance.PrevScene = 1;
                GameManager.Instance.SceneChange("casa");
                break;
            case Type.Yggdrasil:
                GameManager.Instance.hint_UI.SetActive(false);
                GameManager.Instance.PrevScene = 2;
                GameManager.Instance.SceneChange("yggdrasil");
                break;
            case Type.DAD:
                if (flag) {
                    GameManager.Instance.GameOverUI();
                    flag = false;
                    break;    
                }else {
                    GameManager.Instance.Prompt("Quieres ya dejar de jugar y llamar a los demas?");
                    flag = true;
                    break;
                }                
        }
    }
}
