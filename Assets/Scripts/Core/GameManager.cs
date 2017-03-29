using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector]
    public LevelManager CurrentLevelManager { get; private set; }

#if UNITY_EDITOR
    private const string HighScoreBoardPath = "Assets/Resources/Local/GameJSONData/Runtime/HighScore.json";
#else
    private const string HighScoreBoardPath = "HighScore.json";
#endif

    private string _highScoreBoardPath;

    private PathFinding _pathingFinder;

    public string PlayerName = "player";

    public HighScoreBoard LocalHighScoreBoard { get; private set; }

    //Returns the last game state. If no games had finished, it returns null
    public LevelState LastLevelState { get; private set; }

    public uint Obstacles;
    public GameOptions DefaultGameOptions;

    [HideInInspector] public AudioSource SoundSource;
    [HideInInspector] public AudioSource StartSoundSource;
    [HideInInspector] public AudioSource BattleSoundSource;
    private AudioSource[] _sounds;

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
            LoadHighScoreBoard();
            DefaultGameOptions = null;
            LastLevelState = null;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        _sounds = GetComponents<AudioSource>();
        StartSoundSource = _sounds[0];
        BattleSoundSource = _sounds[1];
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
        return DefaultGameOptions;
    }

    public List<GridSystem.Cell> SearchPathFrom(int x, int y)
    {
        return _pathingFinder.SearchCache(x, y);
    }

    public List<GridSystem.Cell> SearchFlyingPath(int x, int y)
    {
        return _pathingFinder.SearchFlying(x, y);
    }

    public void CreatePathFinder(GridSystem gameGrid)
    {
        _pathingFinder = new PathFinding(gameGrid);
    }

    public void ResetPathCache()
    {
        _pathingFinder.ResetPathCache();
    }

    void LoadHighScoreBoard()
    {
        HighScoreBoard.HighScoreList scoreList = null;

        if (File.Exists(_highScoreBoardPath))
        {
            string rawJson = File.ReadAllText(_highScoreBoardPath);
            scoreList = JsonUtility.FromJson<HighScoreBoard.HighScoreList>(rawJson);
            LocalHighScoreBoard = new HighScoreBoard(scoreList);
        }
       

        if (scoreList == null)
        {
            LocalHighScoreBoard = new HighScoreBoard(null);
        }
    }

    public void SaveHighScoreBoard()
    {
        if (!File.Exists(_highScoreBoardPath))
        {
            File.Create(_highScoreBoardPath);
        }
        string jsonDataString = JsonUtility.ToJson(LocalHighScoreBoard.ScoreList);
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
        LastLevelState.CalculateFinalScore(levelState.LastGameOptions);
    }

    public void PlayStartSound()
    {
        BattleSoundSource.Stop();
        StartSoundSource.Play();
    }


    public void PlaybattleSound()
    {
        StartSoundSource.Stop();
        BattleSoundSource.Play();
    }
}
