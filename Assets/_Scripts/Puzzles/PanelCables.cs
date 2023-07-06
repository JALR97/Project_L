using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PanelCables : MonoBehaviour
{
    //**    ---Components---    **//
    //  [[ set in editor ]] 
    [SerializeField] private Color[] cableColors;
    [SerializeField] private Image[] plugs;
    [SerializeField] private PuzzleCables PuzzleRoot;

    //**    ---Variables---    **//
    private int connectedPlugs = 0;
    [SerializeField] private bool lastPanel;
    
    //**    ---Properties---    **//
    public List<int> Order { get; } = new List<int>();

    //**    ---Functions---    **//
    private void CorrectColors() {
        for (int i = 0; i < 4; i++) {
            plugs[i].color = cableColors[Order[i]];
            plugs[i].GetComponent<Plug>().id = Order[i];
        }
    }

    public void NewConnection() {
        connectedPlugs += 1;
        if (connectedPlugs == 4 && lastPanel) {
            PuzzleRoot.Completed();
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
