using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private PathFinding _pathingFinder;

    [HideInInspector]
    public LevelManager CurrentLevelManager { get; private set; }

    // Use this for initialization
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            CurrentLevelManager = null;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void UpdateLevelManager(LevelManager currentLevelManager)
    {
        CurrentLevelManager = currentLevelManager;
        GridSystem gameGrid = CurrentLevelManager.GameBoardSystem.GameGridSystem;
        _pathingFinder = new PathFinding(gameGrid);
    }

    public List<GridSystem.Cell> SearchPathFrom(uint x, uint y)
    {
        return _pathingFinder.Search(x, y);
    }
}
