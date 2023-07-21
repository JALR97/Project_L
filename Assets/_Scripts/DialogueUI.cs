using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour {
    
    private List<string> _messages = new List<string>();
    [SerializeField] private TMP_Text TMPtext;
    [SerializeField] private GameObject UI;

    public void LoadPrompts(List<string> messages) {
        _messages = new List<string>(messages);
    }

    public void Prompt(string message) {
        gameObject.SetActive(true);
        UI.SetActive(true);
        TMPtext.text = message;
    }

    public void Prompt() {
        gameObject.SetActive(true);
        string mess = _messages[0];
        _messages.RemoveAt(0);
        Prompt(mess);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (_messages.Count > 0) {
                Prompt();
            }
            else {
                UI.SetActive(false);
                TMPtext.text = "text";
                gameObject.SetActive(false);
            }
        }
    }
}
