using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour
{
    public static EnemyController Instance;
    public List<GameObject> Enemies;
    private List<Enemy> _enemies;
    private GameBoard _gameBoard;
    private LevelManager _levelManager;
    private bool _spawn = false;
    private bool _build = false;

    public void SpawnEnemy()
    {
        List<GridSystem.Cell> entrances = GameManager.Instance.CurrentLevelManager.GameBoardSystem.GameGridSystem.MainGameGrid.Entrances;
        List<GridSystem.Cell> path = GameManager.Instance.SearchPathFrom(entrances[0].X, entrances[0].Y);
        var tiles = new List<GameBoard.Tile>();
        for (int i = 0; i < path.Count; i++)
        {
            tiles.Add(_gameBoard.BoardTiles[path[i].X,path[i].Y]);
        }
        //randomly chose a entrance
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
            enemy.SetupEnemy(startCell.X,startCell.Y,tiles,path);
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
        //SpawnEnemy();
    }

    private int _num = 1;
	// Update is called once per frame
	void Update () {
	    if (_spawn && _num>0)
	    {
	        _num--;
	        SpawnEnemy();
	        _spawn = false;
	        _build = true;
	    }
	    if (_build && _enemies.Count == 0)
	    {
	        _levelManager.EnterBuildingPhase();
	    }
    }
}
