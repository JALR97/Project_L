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
    [SerializeField] private GameObject UIContainer;

    [SerializeField] private GameObject creditsUI;
    [SerializeField] private GameObject mainMenuUI;

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
        }
    }
    
    public void Endgame() {
        SceneChange("menu");
        Reset();
    }

    private void Reset() {
        foundCapys = new List<LostCapy.CapyID>();
        collected = new List<int>();
        PrevScene = 0;
    }

    public void CollectableTaken(int id) {
        collected.Add(id);
    }
    
    public void CapyFound(LostCapy.CapyID capy) {
        foundCapys.Add(capy);
    }
    public void CapyFound(int capy) {
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
            int collects = GameObject.FindGameObjectWithTag("Collectable").transform.childCount;
            string newStats = $"Capybaras encontrados: {foundCapys.Count + defaultCapys.Count} / {totalCapys}\n\nPuzzles resueltos: {completedPuzzles} / {totalPuzzles}\n\nMcGuffins Encontrados: {collected.Count} / {collects}";
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

    private IEnumerator LoadAsync(string scene) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        if (scene == "Yggdrasil") {
            if (PrevScene == 0) {
                mainMenuUI.SetActive(false);
                GameObject.FindGameObjectWithTag("Player").transform.position =
                    GameObject.FindGameObjectWithTag("STPoint").transform.position;
                
            }
            else {
                GameObject.FindGameObjectWithTag("Player").transform.position =
                    GameObject.FindGameObjectWithTag("HoPoint").transform.position;
            }
        }
    }
    
    public void SceneChange(string scene) {
        switch (scene) {
            case "yggdrasil":
                LoadAsync("Yggdrasil");
                break;
            case "casa":
                SceneManager.LoadScene(2);
                PrevScene = 1;
                break;
            case "menu":
                SceneManager.LoadScene(0);
                PrevScene = 1;
                ShowMainMenu();
                break;
        }
    }

    public void CloseGame() {
        Application.Quit();
    }
    public void ShowCredits() {
        PrevState = Instance.State;
        Instance.State = GameState.Credits;
        mainMenuUI.SetActive(false);
        creditsUI.SetActive(true);
    }
    
    public void ShowMainMenu() {
        PrevState = Instance.State;
        Instance.State = GameState.MainMenu;
        creditsUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }


}
