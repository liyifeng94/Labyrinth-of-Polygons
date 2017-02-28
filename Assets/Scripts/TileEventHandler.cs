﻿using UnityEngine;
using System.Collections;

public class TileEventHandler : MonoBehaviour
{
    [HideInInspector]
    public uint GridX;
    [HideInInspector]
    public uint GridY;

    private bool _towerExist;
    private bool _ongui;
    private TowerController _towerController;
    private Texture2D _image_1;
    private Texture2D _image_2;
    private Texture2D _image_3;
    private Texture2D _image_4;
    private LevelManager _levelManager;
    private GameBoard _gameBoard;
    private Tower _towerPtr;
    private GameObject towerGameObject;

    // Use this for initialization
    void Start ()
    {
        _towerExist = false;
        _ongui = false;
        _gameBoard = null;
        _towerController = TowerController.Instance;
        _image_1 = (Texture2D)Resources.Load("TowerImage_1");
        _image_2 = (Texture2D)Resources.Load("SellImage");
        _image_3 = (Texture2D)Resources.Load("Repair");
        _image_4 = (Texture2D)Resources.Load("Upgrade");
    }
	
	// Update is called once per frame
	void Update () {

    }

    void OnMouseDown()
    {
        //Debug.Log("TEH: Pressed click at " + GridX + "," + GridY + "," + _towerExist + " " + _ongui);
        _ongui = true;
        if(!_towerExist && _levelManager.CurrentGamePhase() == GameBoard.GamePhase.BuildingPhase)
        {
            _gameBoard.HighlightTileAt(GridX, GridY);
        }
    }

    void OnGUI()
    {
        if (_gameBoard == null)
        {
            _levelManager = GameManager.Instance.CurrentLevelManager;
            _gameBoard = _levelManager.GameBoardSystem;
        }
        //Vector3 gridPosition = _gameBoard.BoardTiles[GridX, GridY].TileObject.transform.position;
        //float x = gridPosition.x;
        //float y = gridPosition.y;
#if UNITY_EDITOR
        float x = 4;
        float y = 0;
        int size = 30;
#else
        float x = 40;
        float y = 500;
        int size = 90;

#endif


        //uint x = GridX * 20 + 38;
        //uint y = 460 - GridY * 20 + (3-(GridY / 5))*10;
        //if (GridX >= 7) x -= 49;
        //if (GridY <= 1) y -= 60;
        if (_towerExist)
        {
            // remove case
            if (_ongui && GUI.Button(new Rect(x + 4, y, size, size), _image_2))
            {
                _ongui = false;
                _towerExist = false;
                Debug.Log("TEH: Trying to sell tower");
                RemoveTower();
                _gameBoard.ClearHighlightTiles();
            }

            // repair case
            if (_ongui && GUI.Button(new Rect(x + 4, y + 2 * size, size, size), _image_3))
            {
                _ongui = false;
                _towerExist = true;
                Debug.Log("TEH: Trying to repair tower");
                RepairTower();
                _gameBoard.ClearHighlightTiles();
            }

            // upgrade case
            if (_ongui && GUI.Button(new Rect(x + 4, y + 4 * size, size, size), _image_4))
            {
                _ongui = false;
                _towerExist = true;
                Debug.Log("TEH: Trying to upgrade tower");
                UpgradeTower();
                _gameBoard.ClearHighlightTiles();
            }
        }
        else
        {
            if (_levelManager.CurrentGamePhase() != GameBoard.GamePhase.BuildingPhase)
            {
                return;
            }
            // build tower 1 case
            if (_ongui && GUI.Button(new Rect(x + 4, y, size, size), _image_1))
            {
                _ongui = false;
                Debug.Log("TEH: Trying to build a tower build at " + GridX + "," + GridY + "," + _towerExist + " " + _ongui);
                // ask tower controller to build(check avaliable gold)
                towerGameObject = _towerController.BuildTower(this, GridX, GridY, 0);
                if (null == towerGameObject)
                {
                    Debug.Log("TEH: towerGameObject is null");
                }
                else
                {
                    _towerExist = true;
                    _towerPtr = towerGameObject.GetComponent<Tower>(); // get scripts
                    _towerPtr.Setup(this);
                    // check if it blocks the last path
                    if (!_gameBoard.BuildTower(_towerPtr))
                    {
                        RemoveTower();
                        _towerExist = false;
                    }
                    else
                    {
                        _levelManager.UseGold(_towerPtr.buildCost);
                    }
                }
                _gameBoard.ClearHighlightTiles();
            }
        }
    }

    public void RepairTower()
    {
        // todo update the gold
        _towerPtr.Repair();
    }


    public void RemoveTower()
    {
        Destroy(towerGameObject);
        _towerPtr.Remove();
        // todo update the gold
        Debug.Log("TC: Tower object removed");
    }

    public void UpgradeTower()
    {
        _levelManager.UseGold(_towerPtr.upgradeCost[_towerPtr.getLevel()]); // move it into Tower.cs
        _towerPtr.Upgrade();
    }

    public void SetTowerExist(bool towerExist)
    {
        _towerExist = towerExist;
    }
}
