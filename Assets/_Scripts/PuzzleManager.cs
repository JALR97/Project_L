using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    //**    ---Enums---    **//
    public enum Puzzle{
        CABLES
    }
    //**    ---Components---    **//
    //  [[ set in editor ]] 
    [SerializeField] private GameObject puzzleCables;
    [SerializeField] private Transform puzzleArea;

    //**    ---Variables---    **//
    private Puzzle _currentPuzzle;
    
    //**    ---Functions---    **//
    public void Popup(Puzzle puzzle) {
        gameObject.SetActive(true);
        _currentPuzzle = puzzle;
        Instantiate(FetchPuzzle(puzzle), puzzleArea);
    }

    public void Exit() {
        Destroy(puzzleArea.GetChild(0).gameObject);
        gameObject.SetActive(false);
    }

    public void Completed() {
        //Message game manager, puzzle was completed
        Exit();
    }

    private GameObject FetchPuzzle(Puzzle puzzleId) {
        switch (puzzleId) {
            case Puzzle.CABLES:
                return puzzleCables;
        }
        
        return null;
    }
}
