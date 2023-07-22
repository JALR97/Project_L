using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLiquidSort : MonoBehaviour
{
    //**    ---Components---    **//
    public int botellasCompletadas;
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

}
