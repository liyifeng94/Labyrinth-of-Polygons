﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerController : MonoBehaviour
{
    public static TowerController Instance;
    public List<GameObject> Towers;
    private Tower _towerPtr;
    private GameObject towerGameObject;
    private GameBoard _gameBoard;
    private LevelManager _levelManager;
    private HashSet<Enemy> _enemies;
    // todo: flag to make sure only one ongui esixt

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start ()
    {
        _levelManager = GameManager.Instance.CurrentLevelManager;
        _gameBoard = _levelManager.GameBoardSystem;
        _enemies = new HashSet<Enemy>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public GameObject BuildTower(TileEventHandler teh,uint x, uint y, int index)
    {

        Vector3 gamePosition = _gameBoard.BoardTiles[x, y].TileObject.transform.position;
        towerGameObject = Instantiate(Towers[index], gamePosition, Quaternion.identity) as GameObject;
        _towerPtr = towerGameObject.GetComponent<Tower>(); // get scripts
        if (_towerPtr.buildCost < _levelManager.GetGold())
        {
            // gold will not be used here, need to check if it blocks the last path later
            Debug.Log("TC: Enough gold to build");
        }
        else
        {
            Debug.Log("TC: Not enough gold to build");
            // TODO: display the message
            Destroy(towerGameObject);
            return null;
        }
        Debug.Log("TC: Tower object created");
        return towerGameObject;
    }

    public int CheckTowerInfo(int index, int[] info) // return the tower range, store other info array
    {
        towerGameObject = Instantiate(Towers[index], new Vector3(), Quaternion.identity) as GameObject;
        _towerPtr = towerGameObject.GetComponent<Tower>();
        _towerPtr.GetTowerInfo(info);
        _towerPtr.Remove();
        Destroy(towerGameObject);
        return info[0];
    }

    public void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
    }

    public void ClearEnemis()
    {
        _enemies.Clear();
    }

    public bool CheckIfEnemyAlive(Enemy enemy)
    {
        return _enemies.Contains(enemy);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);
    }
}
