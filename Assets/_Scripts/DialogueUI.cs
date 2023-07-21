using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour {
    
    private List<string> _messages;
    [SerializeField] private TMP_Text TMPtext;
    [SerializeField] private GameObject UI;

    public void LoadPrompts(List<string> messages) {
        _messages = messages;
    }

    public void Prompt(string message) {
        gameObject.SetActive(true);
        UI.SetActive(true);
        TMPtext.text = message;
    }

    public void Prompt() {
        
    }

}
