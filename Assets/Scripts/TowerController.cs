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

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start ()
    {
        _gameBoard = GameManager.Instance.CurrentLevelManager.GameBoardSystem;
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public GameObject BuildTower(TileEventHandler teh,uint x, uint y, int index)
    {

        Vector3 GamePosition;
        GamePosition = _gameBoard.BoardTiles[x, y].TileObject.transform.position;
        towerGameObject = Instantiate(Towers[index], GamePosition, Quaternion.identity) as GameObject;
        _towerPtr = towerGameObject.GetComponent<Tower>(); // get scripts
        //_towerPtr.buildCost
        Debug.Log("TC: Tower object created");
        return towerGameObject;
    }
}
