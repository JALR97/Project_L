using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PanelCables : MonoBehaviour
{
    //**    ---Components---    **//
    //  [[ set in editor ]] 
    [SerializeField] private Color[] cableColors;
    [SerializeField] private Image[] plugs;
    //  [[ set in Start() ]] 
    
    
    //**    ---Variables---    **//
    //  [[ balance control ]] 
    
    
    //  [[ internal work ]] 
    private int numColors = 4;
    private List<int> order = new List<int>();
    
    //**    ---Properties---    **//


    //**    ---Functions---    **//
    private void CorrectColors() {
        for (int i = 0; i < 4; i++) {
            plugs[i].color = cableColors[order[i]];
        }
    }
    
    private void Awake() {
        order.Add(0);
        order.Add(1);
        order.Add(2);
        order.Add(3);
        for (int i = 0; i < order.Count; i++)
        {
            int temp = order[i];
            int randomIndex = Random.Range(i, order.Count);
            order[i] = order[randomIndex];
            order[randomIndex] = temp;
        }

        CorrectColors();
    }
}
