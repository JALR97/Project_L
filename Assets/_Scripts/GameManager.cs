using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
// ReSharper disable InconsistentNaming

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    public GameState State;

    public static event Action<GameState> OnGameStateChange; 
    public enum GameState {
        MainMenu,
        Exploring,
        Puzzle,
        Paused,
        GameOver
    }
    //**    ---Components---    **//
    //  [[ set in editor ]] 
    [SerializeField] private PuzzleManager _puzzleManager;
    [SerializeField] private GameObject pauseUI;
    
    //  [[ set in Start() ]] 


    //**    ---Variables---    **//
    //  [[ balance control ]] 

    //  [[ internal work ]] 
    private int completedPuzzles = 0;
    private int totalPuzzles = 1;
    private int foundCapys = 0;
    private int totalCapys = 0;

    //**    ---Properties---    **//


    //**    ---Functions---    **//
    private void Awake() {
        Instance = this;
    }

    public void SwitchState(GameState newState) {
        State = newState;

        switch (newState) {
            case GameState.MainMenu:
                break;
            case GameState.Exploring:
                break;
            case GameState.Puzzle:
                break;
            case GameState.Paused:
                break;
            case GameState.GameOver:
                break;
        }

        OnGameStateChange(newState);
    }
}
