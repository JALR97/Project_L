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

    //**    ---Properties---    **//
    public List<int> Order { get; } = new List<int>();

    //**    ---Functions---    **//
    private void CorrectColors() {
        for (int i = 0; i < 4; i++) {
            plugs[i].color = cableColors[Order[i]];
            plugs[i].GetComponent<Plug>().id = Order[i];
        }
    }
    
    private void Awake() {
        Order.Add(0);
        Order.Add(1);
        Order.Add(2);
        Order.Add(3);
        for (int i = 0; i < Order.Count; i++)
        {
            int temp = Order[i];
            int randomIndex = Random.Range(i, Order.Count);
            Order[i] = Order[randomIndex];
            Order[randomIndex] = temp;
        }

        CorrectColors();
    }
}
