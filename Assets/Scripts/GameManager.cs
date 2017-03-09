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
#elif UNITY_STANDALONE
    private const string HighScoreBoardPath = "GameData/Resources/GameJSONData/HighScore.json";
#else
    private const string HighScoreBoardPath = "HighScore.json";
#endif

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
        Directory.CreateDirectory(Path.GetDirectoryName(HighScoreBoardPath));
        using (FileStream fs = new FileStream(HighScoreBoardPath, FileMode.OpenOrCreate))
        {
            using (StreamReader reader = new StreamReader(fs))
            {
                string jsonDataString = reader.ReadLine();
                LocalHighScoreBoard = JsonUtility.FromJson<HighScoreBoard>(jsonDataString);
            }
        }

        if (LocalHighScoreBoard == null)
        {
            LocalHighScoreBoard = new HighScoreBoard();
        }
    }

    void SaveHighScoreBoard()
    {
        string jsonDataString = JsonUtility.ToJson(LocalHighScoreBoard);
        Directory.CreateDirectory(Path.GetDirectoryName(HighScoreBoardPath));
        using (FileStream fs = new FileStream(HighScoreBoardPath, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.Write(jsonDataString);
            }
        }
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
