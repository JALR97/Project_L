using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLiquidSort : MonoBehaviour
{
    //**    ---Components---    **//
    public int completado;
    [SerializeField] private PuzzleManager pzm;
    //**    ---Variables---    **//


    //**    ---Properties---    **//


    //**    ---Functions---    **//
    private void Start()
    {
        completado = 0;
    }
    public void setManager(GameObject manager)
    {
        pzm = manager.GetComponent<PuzzleManager>();
    }

    public void Completed()
    {
        pzm.Completed();
    }
}