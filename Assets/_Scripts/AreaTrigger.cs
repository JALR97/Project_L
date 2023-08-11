using System;
using UnityEngine;

public class AreaTrigger : MonoBehaviour {
    
    //-----------------//Data structures//-----------------//
    //enums
    [SerializeField] private enum Type {
        EntrarCasa,
        JumpUI,
        DadUI,
        StartingUI
    }
    
    //-----------------//Components//-----------------//
    //Internal Components


    //Prefabs


    //External References
    private GameObject UI;

    //-----------------//Variables//-----------------//
    //Process variables - private
    [SerializeField] private Type type;

    //Balance variables - serialized 


    //Public properties - private set "Name { get; private set; }"


    //-----------------//Functions//-----------------//
    //Built-in
    private void Start() {
        switch (type) {
            case Type.EntrarCasa:
                UI = GameManager.Instance.hint_UI;
                break;
            case Type.JumpUI:
                UI = GameManager.Instance.jumphint_UI;
                break;
            case Type.DadUI:
                UI = GameManager.Instance.DadHint_UI;
                break;
            case Type.StartingUI:
                UI = GameManager.Instance.Walkhint_UI;
                break;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (UI == null) {
            return;
        }
        if (other.CompareTag("Player")) {
            switch (type) {
                case Type.EntrarCasa:
                    UI.SetActive(true);
                    break;
                case Type.JumpUI:
                    UI.SetActive(true);
                    break;
                case Type.DadUI:
                    UI.SetActive(true);
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (UI == null) {
            return;
        }
        if (other.CompareTag("Player")) {
            switch (type) {
                case Type.EntrarCasa:
                    UI.SetActive(false);
                    break;
                case Type.JumpUI:
                    UI.SetActive(false);
                    Destroy(gameObject);
                    break;
                case Type.DadUI:
                    UI.SetActive(false);
                    Destroy(gameObject);
                    break;
                case Type.StartingUI:
                    UI.SetActive(false);
                    Destroy(gameObject);
                    break;
            }
        }
    }

    //Inner process - private


    //External interaction - public



}