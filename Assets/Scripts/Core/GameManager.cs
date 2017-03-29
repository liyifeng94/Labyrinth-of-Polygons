using System;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

    public int GetDifficultyMultiplier(uint obs)
    {

        var difswitch = new Dictionary<Func<uint, bool>, int>
        {
            {x => x >= 115, 20 },
            {x => 115 > x && x >= 105, 18 },
            {x => 105 > x && x >= 95, 17 },
            {x => 95 > x && x >= 85, 16 },
            {x => 85 > x && x >= 75, 12 },
            {x => 75 > x && x >= 65, 11 },
            {x => 65 > x && x >= 45, 10 },
            {x => 45 > x && x >= 35, 11 },
            {x => 35 > x && x >= 25, 13 },
            {x => 25 > x && x >= 15, 15 },
            {x => 15 > x && x >= 5, 17 },
            {x => x < 5, 19 },
        };
        return difswitch.First(sw => sw.Key(obs)).Value;

        /*
        if (obs >= 115) return 20;
        else if (obs >= 105) return 18;
        else if (obs >= 95) return 17;
        else if (obs >= 85) return 16;
        else if (obs >= 75) return 12;
        else if (obs >= 65) return 11;
        else if (obs >= 55) return 10;
        else if (obs >= 45) return 9;
        else if (obs >= 35) return 11;
        else if (obs >= 25) return 13;
        else if (obs >= 15) return 15;
        else if (obs >= 5) return 17;
        else return 19;
        */
    }
}
