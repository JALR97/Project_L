using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PostProHouse : MonoBehaviour
{
    [SerializeField] private VolumeProfile _normalProfile;
    [SerializeField] private GameObject capy; 
    
    private void Start() {
        if (GameManager.Instance.hasBeenSolved(PuzzleManager.Puzzle.CABLES)) {
            GetComponent<Volume>().profile = _normalProfile;
            capy.SetActive(true);
        }
    }
}
