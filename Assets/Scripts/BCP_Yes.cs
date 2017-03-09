﻿using UnityEngine;
using System.Collections;

public class BCP_Yes : MonoBehaviour
{

    public static BCP_Yes Instance;
    private TowerBuildPanel _towerBuildPanel;
    private TileEventHandler _tileEventHandler;
    private BuildCheckPanel _buildCheckPanel;
    private TowerInfoPanel _towerInfoPanel;
    private LevelManager _levelManager;
    private GameBoard _gameBoard;

    void Awake()
    {
        Instance = this;
    }

    void Start () {

    }

	void Update () {
	
	}

    public void BuildConfirmed()
    {
        _towerBuildPanel.DisAppear();
        _buildCheckPanel.DisAppear();
        _towerInfoPanel.DisAppear();
        _tileEventHandler.SetYes();
        _gameBoard.ClearHighlightTiles();
    }

    public void setTileEventHandler(TileEventHandler teh)
    {
        if (null == _gameBoard)
        {
            _towerBuildPanel = TowerBuildPanel.Instance;
            _buildCheckPanel = BuildCheckPanel.Instance;
            _towerInfoPanel = TowerInfoPanel.Instance;
            _levelManager = GameManager.Instance.CurrentLevelManager;
            _gameBoard = _levelManager.GameBoardSystem;
        }
        _tileEventHandler = teh;
    }
}
