using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PuzzleCables : MonoBehaviour
{
    //**    ---Components---    **//
    public Cable[] cables;
    [SerializeField] private PanelCables _panel1;
    [SerializeField] private PuzzleManager pzm;
    //**    ---Variables---    **//

    
    //**    ---Properties---    **//
    
    
    //**    ---Functions---    **//
    public void setManager(GameObject manager) {
        pzm = manager.GetComponent<PuzzleManager>();
    }
    
    public void Completed() {
        pzm.Completed();
    }
    
    private void Start() {
        for (int i = 0; i < 4; i++) {
            cables[i].id = _panel1.Order[i];
        }
    }
}
