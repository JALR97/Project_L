using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    //**    ---Components---    **//
    //  [[ set in editor ]] 
    
    //  [[ set in Start() ]] 
    
    
    //**    ---Variables---    **//
    //  [[ balance control ]] 
    [SerializeField] private float Amplitude;
    [SerializeField] private float Speed;
    //  [[ internal work ]] 
    private Vector3 initialPosition;
    
    //**    ---Properties---    **//
    public int id;
    
    //**    ---Functions---    **//
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            GameManager.Instance.CollectableTaken(id);
            //Play a sound or show some UI of taking the collectable here
            gameObject.SetActive(false);
        }
    }

    private void Start() {
        initialPosition = transform.position;
    }

    private void Update() {
        float newY = initialPosition.y + Mathf.Sin(Time.time * Speed) * Amplitude;

        transform.position = new Vector3(initialPosition.x, newY, initialPosition.z);
    }
}
