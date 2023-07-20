using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// ReSharper disable InconsistentNaming

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    public GameState PrevState;
    public GameState State;

    public static event Action<GameState> OnGameStateChange; 
    public enum GameState {
        MainMenu,
        Credits,
        Exploring,
        Puzzle,
        Paused,
        GameOver
    }
    //**    ---Components---    **//
    //  [[ set in editor ]] 
    [SerializeField] private PuzzleManager _puzzleManager;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject gameoverUI;

    [SerializeField] private GameObject creditsUI;
    [SerializeField] private GameObject mainMenuUI;

    public GameObject hint_UI;


    //  [[ set in Start() ]] 


    //**    ---Variables---    **//
    //  [[ balance control ]] 

    //  [[ internal work ]] 
    private List<LostCapy.CapyID> foundCapys = new List<LostCapy.CapyID>();
    private int completedPuzzles = 0;
    private int totalPuzzles = 1;
    //private int foundCapys = 0;
    private int totalCapys = 0;

    //**    ---Properties---    **//


    //**    ---Functions---    **//

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (Instance.State is GameState.Exploring or GameState.Puzzle) {

                SwitchState(GameState.Paused);
                Time.timeScale = 0;
                pauseUI.SetActive(true);
            }
            else if (Instance.State is GameState.Paused)
            {
                Time.timeScale = 1;
                SwitchState(PrevState);
                pauseUI.SetActive(false);
            }
        }
    }


    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(transform.parent);
        }
        else {

            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SwitchState(GameState.Exploring);
    }


    public void Endgame() {
        SceneChange("menu");
    }
    
    public void Found(LostCapy.CapyID capy) {
        foundCapys.Add(capy);
    }
    public bool isFound(LostCapy.CapyID capy) {
        return foundCapys.Contains(capy);
    }
    
    public void Solved() {

        completedPuzzles += 1;
    }

    public void TriggeredPuzzle(PuzzleManager.Puzzle puzzle)
    {
        _puzzleManager.SetPuzzle(puzzle);
        SwitchState(GameState.Puzzle);
    }

    public void SwitchState(GameState newState) {
        PrevState = State;
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
            case GameState.Credits:
                break;
        }
        OnGameStateChange?.Invoke(newState);
    }


    public void GameOver() {
        if (State != GameState.GameOver) {
            string newStats = $"Capybaras encontrados: {foundCapys.Count} / {totalCapys}\n\nPuzzles resueltos: {completedPuzzles} / {totalPuzzles}";
            gameoverUI.transform.GetChild(0).GetComponent<TMP_Text>().text = newStats;
            gameoverUI.SetActive(true);
            SwitchState(GameState.GameOver);
            Time.timeScale = 0;
        }

        else {

            gameoverUI.SetActive(false);
            SwitchState(PrevState);
            Time.timeScale = 1;
        }
    }

    public void SceneChange(string scene) {
        switch (scene) {
            case "yggdrasil":
                SceneManager.LoadScene(0);
                break;
            case "casa":
                SceneManager.LoadScene(1);
                break;
            case "menu":
                SceneManager.LoadScene(2);
                break;
        }
    }

    public void CloseGame()
    {
        Application.Quit();
    }
    public void ShowCredits()
    {
        PrevState = Instance.State;
        Instance.State = GameState.Credits;
        mainMenuUI.SetActive(false);
        creditsUI.SetActive(true);
    }
    public void ShowMainMenu()
    {
        PrevState = Instance.State;
        Instance.State = GameState.MainMenu;
        creditsUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }


}
