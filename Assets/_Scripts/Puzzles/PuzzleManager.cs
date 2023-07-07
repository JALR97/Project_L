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
    private GameObject puzzleGameObject;
    
    //**    ---Functions---    **//
    private void Awake() {
        GameManager.OnGameStateChange += StateChange;
        gameObject.SetActive(false);
    }
    
    private void OnDestroy() {
        GameManager.OnGameStateChange -= StateChange;
    }

    private void StateChange(GameManager.GameState newState) {
        if (newState == GameManager.GameState.Puzzle) {
            Popup(_currentPuzzle);
        }
    }

    public void Popup(Puzzle puzzle) {
        gameObject.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(true);
        _currentPuzzle = puzzle;
        puzzleGameObject = Instantiate(FetchPuzzle(puzzle), puzzleArea);
        switch (puzzle) {
            case Puzzle.CABLES:
                puzzleGameObject.GetComponent<PuzzleCables>().setManager(gameObject);
                break;
        }
    }

    public void Exit() {
        Destroy(puzzleArea.GetChild(0).gameObject);
        gameObject.SetActive(false);
        GameManager.Instance.SwitchState(GameManager.GameState.Exploring);
    }

    public void Completed() {
        GameManager.Instance.Solved();
        Exit();
    }

    public void SetPuzzle(Puzzle puzzle) {
        _currentPuzzle = puzzle;
    }

    private GameObject FetchPuzzle(Puzzle puzzleId) {
        switch (puzzleId) {
            case Puzzle.CABLES:
                return puzzleCables;
        }
        
        return null;
    }
}
