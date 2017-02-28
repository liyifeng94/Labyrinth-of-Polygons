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

    public void UpdateLevelManager(LevelManager currentLevelManager)
    {
        CurrentLevelManager = currentLevelManager;
    }

    public List<GridSystem.Cell> SearchPathFrom(uint x, uint y)
    {
        return _pathingFinder.Search(x, y);
    }

    public void CreatePathFinder(GridSystem gameGrid)
    {
        _pathingFinder = new PathFinding(gameGrid);
    }


}
