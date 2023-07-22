using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PuzzleManager : MonoBehaviour
{
    //**    ---Enums---    **//
    public enum Puzzle{
        CABLES,
        LIQUIDSORT
    }
    //**    ---Components---    **//
    //  [[ set in editor ]] 
    [SerializeField] private GameObject puzzle;
    [SerializeField] private Transform puzzleArea;
    [SerializeField] private VolumeProfile _normalProfile;

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
        ///
        //puzzleGameObject.GetComponent<PuzzleCables>().setManager(gameObject);
        switch (puzzle) {
            case Puzzle.CABLES:
                puzzleGameObject.GetComponent<PuzzleCables>().setManager(gameObject);
                break;
            case Puzzle.LIQUIDSORT:
                puzzleGameObject.GetComponent<PuzzleLiquidSort>().setManager(gameObject);
                break;
        }
    }

    public void Exit() {
        Destroy(puzzleArea.GetChild(0).gameObject);
        gameObject.SetActive(false);
        GameManager.Instance.SwitchState(GameManager.GameState.Exploring);
    }

    public void Completed() {
        GameManager.Instance.Solved(_currentPuzzle);
        if (_currentPuzzle == Puzzle.CABLES) {
            GameManager.Instance.PlayOne(SoundManager.clipID.CABLES);
            GameObject.FindGameObjectWithTag("Capy").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("Volume").GetComponent<Volume>().profile = _normalProfile;
        }
        Exit();
    }

    public void SetPuzzle(Puzzle puzzle) {
        _currentPuzzle = puzzle;
    }

    private GameObject FetchPuzzle(Puzzle puzzleId) {
        switch (puzzleId) {
            case Puzzle.CABLES:
                return puzzle;
            case Puzzle.LIQUIDSORT:
                return puzzle;
        }
        
        return null;
    }
}
