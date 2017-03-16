using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GameManager : MonoBehaviour
{
    private PathFinding _pathingFinder;

    public static GameManager Instance;

    [HideInInspector]
    public LevelManager CurrentLevelManager { get; private set; }

#if UNITY_EDITOR
    private const string HighScoreBoardPath = "Assets/Resources/Local/GameJSONData/Runtime/HighScore.json";
#else
    private const string HighScoreBoardPath = "HighScore.json";
#endif

    private string _highScoreBoardPath;

    public HighScoreBoard LocalHighScoreBoard { get; private set; }

    //Returns the last game state. If no games had finished, it returns null
    public LevelState LastLevelState { get; private set; }

    public GameOptions CurrentGameOptions;

    // Use this for initialization
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            CurrentLevelManager = null;
#if UNITY_EDITOR
            _highScoreBoardPath = HighScoreBoardPath;
#else
            _highScoreBoardPath = Application.persistentDataPath + "/" + HighScoreBoardPath;
#endif
            Debug.LogWarning("HighScoreBoardPath: "  + _highScoreBoardPath);
            LoadHighScoreBoard();
            CurrentGameOptions = null;
            LastLevelState = null;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); 
#endif
        }
    }

    public GameOptions StartGameLevel(LevelManager currentLevelManager)
    {
        CurrentLevelManager = currentLevelManager;
        return CurrentGameOptions;
    }

    public List<GridSystem.Cell> SearchPathFrom(uint x, uint y)
    {
        return _pathingFinder.Search(x, y);
    }

    public List<GridSystem.Cell> SearchFlyingPath(uint x, uint y)
    {
        return _pathingFinder.SearchFlying(x, y);
    }

    public void CreatePathFinder(GridSystem gameGrid)
    {
        _pathingFinder = new PathFinding(gameGrid);
    }

    void LoadHighScoreBoard()
    {
        LocalHighScoreBoard = null;
        
        if (File.Exists(_highScoreBoardPath))
        {
            string rawJson = File.ReadAllText(_highScoreBoardPath);
            LocalHighScoreBoard = JsonUtility.FromJson<HighScoreBoard>(rawJson);
        }
       

        if (LocalHighScoreBoard == null)
        {
            LocalHighScoreBoard = new HighScoreBoard();
        }
    }

    void SaveHighScoreBoard()
    {
        string jsonDataString = JsonUtility.ToJson(LocalHighScoreBoard);
        File.WriteAllText(_highScoreBoardPath, jsonDataString);
    }

    void OnApplicationQuit()
    {
        // Save high score on quit
        SaveHighScoreBoard();
    }

    public void SaveGameState(LevelState levelState)
    {
        LastLevelState = levelState;
        LastLevelState.CalculateFinalScore(CurrentGameOptions);
    }

    public void AddScoreEntry(LevelState levelState, string playerName)
    {
        LocalHighScoreBoard.AddEntry(levelState.FinalScore,playerName);
    }
}
