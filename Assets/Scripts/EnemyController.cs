using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    public static EnemyController Instance;
    public List<GameObject> Enemies;
    private List<Enemy> _enemies;
    private GameBoard _gameBoard;
    private LevelManager _levelManager;
    private bool _spawn = false;
    private bool _build = false;
    private float _start,_end;
    private Enemy.Type type = Enemy.Type.Normal;

    public void SpawnEnemy()
    {
        List<GridSystem.Cell> entrances = GameManager.Instance.CurrentLevelManager.GameBoardSystem.GameGridSystem.MainGameGrid.Entrances;
        int entrance = Random.Range(0, entrances.Count);

        List<GridSystem.Cell> path = GameManager.Instance.SearchPathFrom(entrances[entrance].X, entrances[entrance].Y);
        List<GridSystem.Cell> flyingPath = GameManager.Instance.SearchPathFrom(entrances[entrance].X, entrances[entrance].Y);
        var tiles = new List<GameBoard.Tile>();
        foreach (GridSystem.Cell t in path)
        {
            tiles.Add(_gameBoard.BoardTiles[t.X,t.Y]);
        }

        GridSystem.Cell startCell = entrances[0];
        GameBoard.Tile startTile = tiles[0];

        

        Vector3 spawnPosition = startTile.Position;

        GameObject enemeyGameObject = Instantiate(Enemies[0], spawnPosition, Quaternion.identity) as GameObject;
        if (enemeyGameObject != null)
        {
            
            enemeyGameObject.transform.SetParent(this.transform);
            Enemies.Add(enemeyGameObject);
            Enemy enemy = enemeyGameObject.GetComponent<Enemy>();
            _enemies.Add(enemy);
            if ()
            enemy.SetupEnemy(startCell.X,startCell.Y,tiles,path,(Enemy.Type)type);
            type++;
            _gameBoard.AddEnemy(enemy);
        }
    }

    public void StartSpawning()
    {
        _spawn = true;
    }

    public void StopSpawning()
    {
        _spawn = false;
    }

    public void RemoveEnemy(Enemy ptr)
    {
        _gameBoard.RemoveEnemy(ptr);
        _enemies.Remove(ptr);
    }

    
    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
    void Start ()
    {
        _gameBoard = GameManager.Instance.CurrentLevelManager.GameBoardSystem;
        _levelManager = GameManager.Instance.CurrentLevelManager;
        _enemies = new List<Enemy>();
        _start = Time.time;
        //SpawnEnemy();
    }

    private int _num = 5;
	// Update is called once per frame
	void Update ()
	{
	    _end = Time.time;
	    float diff =  _end - _start;
	    if (_spawn && _num>0 && diff>1)
	    {
	        _start = Time.time;
	        _end = Time.time;
	        _num--;
	        SpawnEnemy();
	        if (_num == 0)
	        {
	            _spawn = false;
            }
            _build = true;
        }
	    if ((!_spawn) && _build && _enemies.Count == 0)
	    {
	        _levelManager.EnterBuildingPhase();

	        _num = 5;
	        _build = false;
	    }
    }
}
