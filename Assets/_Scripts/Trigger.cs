using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour, IInteractable
{
    [SerializeField] private enum Type {
        CABLES,
        CASA
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
                //Cambiar la escena a la casa.
                break;
        }
    }
}
