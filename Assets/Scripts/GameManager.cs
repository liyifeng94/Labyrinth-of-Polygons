using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector]
    public LevelManager CurrentLevelManager;

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

    public class PathFinding
    {
        private Cell[,] _grid;
        private Cell[,] _prev;
        private int[] dist;
        private List<Cell> _entry;
        private List<Cell> _exit;

        public PathFinding( Cell[,] grid, List<Cell> Entrance, List<Cell> Exits )
        {
            _grid = grid;
            _entry = Entrance;
            _exit = Exits;
            dist = new int[Width * Height] { -1 };
            _prev = new Cell[Width, Height] { null };
        }

        public Cell find()
        {

        }
    }
}
