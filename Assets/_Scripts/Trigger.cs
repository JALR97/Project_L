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
        Yggdrasil
    }
    //**    ---Components---    **//
    [SerializeField] private Type _type;
    
    //**    ---Variables---    **//
    //  [[ balance control ]] 
    
    //  [[ internal work ]] 
    
    
    //**    ---Properties---    **//
    
    
    //**    ---Functions---    **//
    public void Interact() {
        switch (_type) {
            case Type.CABLES:
                GameManager.Instance.TriggeredPuzzle(PuzzleManager.Puzzle.CABLES);
                break;
        }
        switch (_type) {
            case Type.CASA:
                SceneManager.LoadScene(3);
                break;
        }
        switch (_type)
        {
            case Type.Yggdrasil:
                SceneManager.LoadScene(0);
                break;
        }
    }
}
