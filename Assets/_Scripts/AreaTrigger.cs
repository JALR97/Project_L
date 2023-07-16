using System;
using UnityEngine;

public class AreaTrigger : MonoBehaviour {
    
    //-----------------//Data structures//-----------------//
    //enums
    [SerializeField] private enum Type {
        MostrarUI,
    }
    
    //-----------------//Components//-----------------//
    //Internal Components


    //Prefabs


    //External References
    [SerializeField] private GameObject UI;

    //-----------------//Variables//-----------------//
    //Process variables - private
    [SerializeField] private Type type;

    //Balance variables - serialized 


    //Public properties - private set "Name { get; private set; }"


    //-----------------//Functions//-----------------//
    //Built-in
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            switch (type) {
                case Type.MostrarUI:
                    UI.SetActive(true);
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            switch (type) {
                case Type.MostrarUI:
                    UI.SetActive(false);
                    break;
            }
        }
    }
    //Inner process - private


    //External interaction - public



}