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
    public int EnemyNum = 5;

    public void SpawnEnemy(int wave)
    {
        List<GridSystem.Cell> entrances = GameManager.Instance.CurrentLevelManager.GameBoardSystem.GameGridSystem.MainGameGrid.Entrances;
        int entrance = Random.Range(0, entrances.Count);

        int temp = Random.Range(0, 4);

        if (wave == 1) temp = 0;
        if (wave == 2) temp = 1;
        if (wave == 3) temp = 2;
        if (wave == 4) temp = 3;
        if (wave % 5 == 0) temp = Random.Range(4, 7);

        List<GridSystem.Cell> path;
        if ((Enemy.Type)temp != Enemy.Type.Flying) path =  GameManager.Instance.SearchPathFrom(entrances[entrance].X, entrances[entrance].Y);
        else path = GameManager.Instance.SearchFlyingPath(entrances[entrance].X, entrances[entrance].Y);
        var tiles = new List<GameBoard.Tile>();
        foreach (GridSystem.Cell t in path)
        {
            tiles.Add(_gameBoard.BoardTiles[t.X,t.Y]);
        }

        GridSystem.Cell startCell = entrances[0];
        GameBoard.Tile startTile = tiles[0];


        Vector3 spawnPosition = startTile.Position;

        GameObject enemeyGameObject = Instantiate(Enemies[temp], spawnPosition, Quaternion.identity) as GameObject;
        if (enemeyGameObject != null)
        {
            
            enemeyGameObject.transform.SetParent(this.transform);
            //Enemies.Add(enemeyGameObject);
            Enemy enemy = enemeyGameObject.GetComponent<Enemy>();
            _enemies.Add(enemy);
            enemy.SetupEnemy(startCell.X,startCell.Y,tiles, path, (Enemy.Type) temp);
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
    }
    
	void Update ()
	{
	    _end = Time.time;
	    float diff =  _end - _start;
	    if (_spawn && EnemyNum>0 && diff>1)
	    {
	        _start = Time.time;
	        _end = Time.time;
            EnemyNum--;
	        SpawnEnemy(_levelManager.GetCurrentLevel());
	        if (EnemyNum == 0)
	        {
	            _spawn = false;
            }
            _build = true;
        }
	    if ((!_spawn) && _build && _enemies.Count == 0)
	    {
	        _levelManager.EnterBuildingPhase();

	        int level = _levelManager.GetCurrentLevel();
            EnemyNum = GetEnemyNum(level);
	        _build = false;
	    }
    }

    private int GetEnemyNum(int level)
    {
        int num;
        if (level < 4) num = 5;
        else if ((level + 1) % 5 == 0) num = 1 + level / 5;
        else num = 5 + (level - 5) * 3;
        return num;
    }

    public int GetTowerPriority(Tower.TowerType type)
    {
        switch (type)
        {
            case Tower.TowerType.Tank:
                return 1;
            case Tower.TowerType.Heal:
                return 2;
            case Tower.TowerType.Gold:
                return 0;
            case Tower.TowerType.Range:
                return 3;
            case Tower.TowerType.Slow:
                return 4;
            default:
                return -1;
        }
    }

    public int GetHpGrowth(Enemy.Type type)
    {
        int level = _levelManager.GetCurrentLevel();
        if (level <= 5) return 0;
        level -= 6;
        switch (type)
        {
            case Enemy.Type.Normal:
                return 5 * level;
            case Enemy.Type.Flying:
                return 5 * level;
            case Enemy.Type.Fast:
                return 3 * level;
            case Enemy.Type.Attacking:
                return 5 * level;
            case Enemy.Type.BossAttack:
                return 7 * level;
            case Enemy.Type.BossFly:
                return 5 * level;
            case Enemy.Type.BossTank:
                return 10 * level;
            default:
                return 0;
        }

    }

    public float GetGoldGrowth(Enemy.Type type)
    {
        int level = _levelManager.GetCurrentLevel();
        if (level <= 5) return 1;
        level -= 6;
        return (float) Math.Pow(1.4, level);
    }

    public float GetAttackGrowth(Enemy.Type type)
    {
        int level = _levelManager.GetCurrentLevel();
        if (level <= 5) return 1;
        level -= 6;
        switch (type)
        {
            case Enemy.Type.Attacking:
                return (float)Math.Pow(1.2, level);
            case Enemy.Type.BossFly:
                return (float)Math.Pow(1.1, level);
            case Enemy.Type.BossAttack:
                return (float)Math.Pow(1.3, level);
            case Enemy.Type.BossTank:
                return (float)Math.Pow(1.1, level);
        }
        return 0;
    }

}
