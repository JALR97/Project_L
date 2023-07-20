using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostCapy : MonoBehaviour, IInteractable
{
    public enum CapyID {
        PUNK,
        ARTIST,
        NERD
    }
    //**    ---Components---    **//

    
    
    //**    ---Variables---    **//
    [SerializeField] private CapyID id;


    //**    ---Functions---    **//

    public void Interact() {
        if (!GameManager.Instance.isFound(id)) {
            //Show message that capy was found
            //Debug.Log($"capy found: {id}");
            GameManager.Instance.Found(id);
        }
        else {
            //Debug.Log($"already found: {id}");
            //Show message when capy is found already. Trigger puzzle if there's one
        }
    }
}
