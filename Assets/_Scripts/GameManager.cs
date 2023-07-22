using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    public int PrevScene = 0;
    public GameState PrevState;
    public GameState State;

    public static event Action<GameState> OnGameStateChange; 
    public enum GameState {
        MainMenu,
        Credits,
        Controles,
        Objetivos,
        Exploring,
        Puzzle,
        Paused,
        GameOver
    }
    //**    ---Components---    **//
    //  [[ set in editor ]] 
    [SerializeField] private PuzzleManager _puzzleManager;
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject gameoverUI;
    [SerializeField] private GameObject UIContainer;
    [SerializeField] private DialogueUI DialogueBox;

    [SerializeField] private GameObject creditsUI;
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject ControlesUI;
    [SerializeField] private GameObject ObjetivosUI;

    public GameObject hint_UI;
    private List<int> defaultCapys = new List<int>();

    //  [[ set in Start() ]] 


    //**    ---Variables---    **//
    //  [[ balance control ]] 

    //  [[ internal work ]] 
    private List<LostCapy.CapyID> foundCapys = new List<LostCapy.CapyID>();
    private List<int> collected = new List<int>();
    private List<PuzzleManager.Puzzle> completedPuzzles = new List<PuzzleManager.Puzzle>();
    private int totalPuzzles = 1;
    private int totalCapys = 15;

    //**    ---Properties---    **//
    public int PlayerSpawn = 0;

    //**    ---Functions---    **//
    public void LoadCollectables() {
        Transform container = GameObject.FindGameObjectWithTag("Collectable").transform;
        for (int i = 0; i < container.childCount; i++) {
            container.GetChild(i).GetComponent<Collectable>().id = i;
        }

        foreach (int id in collected) {
            container.GetChild(id).gameObject.SetActive(false);
        }
    }
    
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
            Destroy(UIContainer);
            Destroy(_soundManager);
        }
    }
    
    public void Endgame() {
        gameoverUI.SetActive(false);
        Time.timeScale = 1;
        SceneChange("menu");
        SwitchState(GameState.MainMenu);
        Reset();
    }

    private void Reset() {
        foundCapys = new List<LostCapy.CapyID>();
        collected = new List<int>();
        PrevScene = 0;
        PlayerSpawn = 0;
    }

    public void CollectableTaken(int id) {
        _soundManager.PlaySimple(SoundManager.clipID.COLLECTABLE);
        collected.Add(id);
    }
    
    public void CapyFound(LostCapy.CapyID capy) {
        _soundManager.PlaySimple(SoundManager.clipID.CAPYFOUND);
        foundCapys.Add(capy);
    }
    public void CapyFound(int capy) {
        _soundManager.PlaySimple(SoundManager.clipID.CAPYFOUND);
        defaultCapys.Add(capy);
    }
    
    public bool hasCapyBeenFound(LostCapy.CapyID capy) {
        return foundCapys.Contains(capy);
    }
    public bool hasCapyBeenFound(int capy) {
        return defaultCapys.Contains(capy);
    }
    
    public bool hasBeenSolved(PuzzleManager.Puzzle puzzle) {
        return completedPuzzles.Contains(puzzle);
    }
    
    public void Solved(PuzzleManager.Puzzle puzzle) {
        completedPuzzles.Add(puzzle);
    }

    public void TriggeredPuzzle(PuzzleManager.Puzzle puzzle)
    {
        _puzzleManager.SetPuzzle(puzzle);
        SwitchState(GameState.Puzzle);
    }

    public void PlayOne(SoundManager.clipID id) {
        _soundManager.PlaySimple(id);
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
            case GameState.Controles:
                break;
            case GameState.Objetivos:
                break;
        }
        OnGameStateChange?.Invoke(newState);
    }

    public void GameOverUI() {
        if (State != GameState.GameOver) {
            int collects = GameObject.FindGameObjectWithTag("Collectable").transform.childCount;
            string newStats = $"Capybaras encontrados: {foundCapys.Count + defaultCapys.Count} / {totalCapys}\n\nPuzzles resueltos: {completedPuzzles.Count} / {totalPuzzles}\n\nMcGuffins Encontrados: {collected.Count} / {collects}";
            gameoverUI.transform.GetChild(0).GetComponent<TMP_Text>().text = newStats;
            gameoverUI.SetActive(true);
            SwitchState(GameState.GameOver);
            Time.timeScale = 0;
        }
        else {
            gameoverUI.SetActive(false);
            SwitchState(GameState.Exploring);
            Time.timeScale = 1;
        }
    }

    // private IEnumerator LoadAsync(string scene) {
    //     AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
    //
    //     // Wait until the asynchronous scene fully loads
    //     while (!asyncLoad.isDone)
    //     {
    //         yield return null;
    //     }
    //
    //     if (scene == "Yggdrasil") {
    //         if (PrevScene == 0) {
    //             mainMenuUI.SetActive(false);
    //             GameObject.FindGameObjectWithTag("Player").transform.position =
    //                 
    //         }
    //         else {
    //             GameObject.FindGameObjectWithTag("Player").transform.position =
    //                 GameObject.FindGameObjectWithTag("HoPoint").transform.position;
    //         }
    //     }
    // }
    
    public void SceneChange(string scene) {
        switch (scene) {
            case "yggdrasil":
                SceneManager.LoadScene(1);
                if (PrevScene == 0) {
                    PlayerSpawn = 0;
                    SwitchState(GameState.Exploring);
                }
                else {
                    PlayerSpawn = 1;
                    GameManager.Instance.PlayOne(SoundManager.clipID.DOOR);
                }
                break;
            case "casa":
                SceneManager.LoadScene(2);
                PlayerSpawn = -1;
                PrevScene = 1;
                GameManager.Instance.PlayOne(SoundManager.clipID.DOOR);
                break;
            case "menu":
                SceneManager.LoadScene(0);
                PrevScene = 0;
                ShowMainMenu();
                break;
        }
    }

    public void CloseGame() {
        Application.Quit();
    }

    public void HideUI() {
        mainMenuUI.SetActive(false);
        creditsUI.SetActive(false);
    }
    
    public void ShowCredits() {
        PrevState = Instance.State;
        Instance.State = GameState.Credits;
        mainMenuUI.SetActive(false);
        creditsUI.SetActive(true);
    }

    public void ShowControlesFromMenu()
    {
        PrevState = Instance.State;
        Instance.State = GameState.Controles;
        ControlesUI.SetActive(true);
        mainMenuUI.SetActive(false);
    }
    public void ShowObjetivos()
    {
        PrevState = Instance.State;
        Instance.State = GameState.Objetivos;
        ObjetivosUI.SetActive(true);
        ControlesUI.SetActive(false);
    }
    public void ShowMainMenuFromObjetivos()
    {
        PrevState = Instance.State;
        Instance.State = GameState.MainMenu;
        ObjetivosUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    public void ShowCreditosFromObjetivos()
    {
        PrevState = Instance.State;
        Instance.State = GameState.MainMenu;
        ObjetivosUI.SetActive(false);
        creditsUI.SetActive(true);
    }
    public void ShowMainMenuFromControles()
    {
        PrevState = Instance.State;
        Instance.State = GameState.MainMenu;
        ControlesUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    public void ShowMainMenu() {
        PrevState = Instance.State;
        Instance.State = GameState.MainMenu;
        creditsUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }
    
    /////////////////Dialogue
    public void LoadPrompts(List<string> messages) { 
        DialogueBox.LoadPrompts(messages);
    }

    public void Prompt(string message) {
        DialogueBox.Prompt(message);
    }

    public void Prompt() {
        DialogueBox.Prompt();
    }

}
