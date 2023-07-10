using System;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
// ReSharper disable InconsistentNaming

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    public GameState PrevState;
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
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (Instance.State is GameState.Exploring or GameState.Puzzle) {
                PrevState = Instance.State;
                Instance.State = GameState.Paused;
                Time.timeScale = 0;
                pauseUI.SetActive(true);
            }
            else if (Instance.State is GameState.Paused) {
                Time.timeScale = 1;
                Instance.State = PrevState;
                pauseUI.SetActive(false);
            }
        }
    }

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        SwitchState(GameState.Exploring);
    }

    public void Found() {
        foundCapys += 1;
    }
    public void Solved() {
        completedPuzzles += 1;
    }

    public void TriggeredPuzzle(PuzzleManager.Puzzle puzzle) {
        _puzzleManager.SetPuzzle(puzzle);
        SwitchState(GameState.Puzzle);
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
        OnGameStateChange?.Invoke(newState);
    }
}
