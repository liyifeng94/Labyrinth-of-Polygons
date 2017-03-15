﻿using UnityEngine;
using System.Collections;

public class TileEventHandler : MonoBehaviour
{
    [HideInInspector]
    public uint GridX;
    [HideInInspector]
    public uint GridY;

    private bool _towerExist;
    private bool _confirmed;
    private int _towerIndex;
    private bool _clearBeforeBattle;

    private LevelManager _levelManager;
    private GameBoard _gameBoard;
    private TowerController _towerController;
    private TankTower _tankTowerPtr;
    private RangeTower _rangeTowerPtr;
    private SlowTower _slowTowerPtr;
    // TODO~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    private GameObject _towerGameObject;
    private int _currentTowerType;

    private TowerBuildPanel _towerBuildPanel;
    private TowerOperationPanel _towerOperationPanel;
    private TowerInfoPanel _towerInfoPanel;
    private BuildCheckPanel _buildCheckPanel;
    private NotificationPanel _notificationPanel;

    private TankTowerButton _tankTowerButton;
    private RangeTowerButton _rangeTowerButton;
    private SlowTowerButton _slowTowerButton;
    // TODO~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    private UpgradeButton _upgradeButton;
    private RepairButton _repairButton;
    private SellButton _sellButton;
    private BCP_Yes _yesButton;

    public enum Operation { Nop, TankTower, RangeTower, SlowTower, HealTower, MoneyTower, Upgrade, Repair, Sell}
    public Operation TowerOperation;


    void Start ()
    {
        _towerExist = false;
        _confirmed = false;
        _towerIndex = -1;
        _clearBeforeBattle = false;
        _currentTowerType = -1;

        _levelManager = GameManager.Instance.CurrentLevelManager;
        _gameBoard = _levelManager.GameBoardSystem;
        _towerController = TowerController.Instance;

        _towerBuildPanel = TowerBuildPanel.Instance;
        _towerOperationPanel = TowerOperationPanel.Instance;
        _towerInfoPanel = TowerInfoPanel.Instance;
        _buildCheckPanel = BuildCheckPanel.Instance;
        _notificationPanel = NotificationPanel.Instance;

        _tankTowerButton = TankTowerButton.Instance;
        _rangeTowerButton = RangeTowerButton.Instance;
        _slowTowerButton = SlowTowerButton.Instance;
        // TODO~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        _upgradeButton = UpgradeButton.Instance;
        _repairButton = RepairButton.Instance;
        _sellButton = SellButton.Instance;
        _yesButton = BCP_Yes.Instance;

        TowerOperation = Operation.Nop;
    }
	

	void Update () {

        // disappear all panels when fousing to enter a batte during building OR operation tower phase
	    if (!_clearBeforeBattle && _levelManager.CurrentGamePhase() == GameBoard.GamePhase.BattlePhase)
	    {
            _towerBuildPanel.DisAppear();
            _towerOperationPanel.DisAppear();
            _towerInfoPanel.DisAppear();
            _buildCheckPanel.DisAppear();
            _notificationPanel.DisAppear();
            _gameBoard.ClearHighlightTiles();
            _clearBeforeBattle = true;
	        return;
	    }
        // make the above code work for the entire game
        if (_levelManager.CurrentGamePhase() == GameBoard.GamePhase.BuildingPhase)
        {
            _clearBeforeBattle = false;
        }
        //Debug.Log("TEH: Confirmed operation is " + TowerOperation);
        if (_confirmed)
        {
            //Debug.Log("TEH: Confirmed operation is " + TowerOperation);
            switch (TowerOperation)
            {
                case Operation.Nop:
                    break;
                case Operation.TankTower:
                    // Debug.Log("TEH: Trying to build a tower build at " + GridX + "," + GridY + "," + _towerExist + " " + _ongui);
                    // ask tower controller to build(check avaliable gold)
                    _towerGameObject = _towerController.BuildTower(this, GridX, GridY, _towerIndex);
                    if (null == _towerGameObject)
                    {
                        Debug.Log("TEH: towerGameObject is null");
                    }
                    else
                    {
                        _towerExist = true;
                        _currentTowerType = 0;
                        _tankTowerPtr = _towerGameObject.GetComponent<TankTower>(); // get scripts
                        _tankTowerPtr.Setup(this);
                        // check if it blocks the last path
                        if (!_gameBoard.BuildTower(_tankTowerPtr))
                        {
                            SellTower(true);
                            _towerExist = false;
                            //_notificationPanel.DisAppear();
                            _notificationPanel.SetNotificationType("Block");
                            _notificationPanel.Appear();
                        }
                        else
                        {
                            _levelManager.UseGold(_tankTowerPtr.BuildCost);
                        }
                    }
                    break;
                case Operation.RangeTower:
                    // Debug.Log("TEH: Trying to build a tower build at " + GridX + "," + GridY + "," + _towerExist + " " + _ongui);
                    // ask tower controller to build(check avaliable gold)
                    _towerGameObject = _towerController.BuildTower(this, GridX, GridY, _towerIndex);
                    if (null == _towerGameObject)
                    {
                        Debug.Log("TEH: towerGameObject is null");
                    }
                    else
                    {
                        _towerExist = true;
                        _currentTowerType = 1;
                        _rangeTowerPtr = _towerGameObject.GetComponent<RangeTower>(); // get scripts
                        _rangeTowerPtr.Setup(this);
                        // check if it blocks the last path
                        if (!_gameBoard.BuildTower(_rangeTowerPtr))
                        {
                            SellTower(true);
                            _towerExist = false;
                            //_notificationPanel.DisAppear();
                            _notificationPanel.SetNotificationType("Block");
                            _notificationPanel.Appear();
                        }
                        else
                        {
                            _levelManager.UseGold(_rangeTowerPtr.BuildCost);
                        }
                    }
                    break;
                case Operation.SlowTower:
                    // Debug.Log("TEH: Trying to build a tower build at " + GridX + "," + GridY + "," + _towerExist + " " + _ongui);
                    // ask tower controller to build(check avaliable gold)
                    _towerGameObject = _towerController.BuildTower(this, GridX, GridY, _towerIndex);
                    if (null == _towerGameObject)
                    {
                        Debug.Log("TEH: towerGameObject is null");
                    }
                    else
                    {
                        _towerExist = true;
                        _currentTowerType = 2;
                        _slowTowerPtr = _towerGameObject.GetComponent<SlowTower>(); // get scripts
                        _slowTowerPtr.Setup(this);
                        // check if it blocks the last path
                        if (!_gameBoard.BuildTower(_slowTowerPtr))
                        {
                            SellTower(true);
                            _towerExist = false;
                            //_notificationPanel.DisAppear();
                            _notificationPanel.SetNotificationType("Block");
                            _notificationPanel.Appear();
                        }
                        else
                        {
                            _levelManager.UseGold(_slowTowerPtr.BuildCost);
                        }
                    }
                    break;
                case Operation.HealTower:
                    break;
                case Operation.MoneyTower:
                    break;
                case Operation.Upgrade:
                    Debug.Log("TEH: Upgrade Operation called");
                    if (0 == _currentTowerType) { _tankTowerPtr.Upgrade(); }
                    if (1 == _currentTowerType) { _rangeTowerPtr.Upgrade(); }
                    if (2 == _currentTowerType) { _slowTowerPtr.Upgrade(); }
                    // TODO~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                    break;
                case Operation.Repair:
                    if (0 == _currentTowerType) { _tankTowerPtr.Repair(); }
                    if (1 == _currentTowerType) { _rangeTowerPtr.Repair(); }
                    if (2 == _currentTowerType) { _slowTowerPtr.Repair(); }

                    // TODO~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                    break;
                case Operation.Sell:
                    //Debug.Log("TEH: Sell operation executing");
                    SellTower(false);
                    break;
            }
            _confirmed = false;
        }
    }


    void OnMouseDown()
    {
        //Debug.Log("TEH: Pressed click at " + GridX + "," + GridY + "," + _towerExist + " " + _currentTowerType);
        if(_levelManager.CurrentGamePhase() == GameBoard.GamePhase.BuildingPhase)
        {
            _gameBoard.ClearHighlightTiles();
            _gameBoard.HighlightTileAt(GridX, GridY);
            if (_towerExist)
            {
                Debug.Log("TEH: Click on an existing tower at " + GridX + "," + GridY + ", type is " + _currentTowerType);
                _sellButton.setTowerEventHandler(this);
                _upgradeButton.setTowerEventHandler(this);
                _repairButton.setTowerEventHandler(this);
                _yesButton.setTileEventHandler(this);
                _towerOperationPanel.Appear();
                int[] towerInfo = new int[11];
                if (0 == _currentTowerType) { DisplayAttackRange(_tankTowerPtr.GetTowerInfo(towerInfo)); }
                if (1 == _currentTowerType) { DisplayAttackRange(_rangeTowerPtr.GetTowerInfo(towerInfo)); }
                if (2 == _currentTowerType) { DisplayAttackRange(_slowTowerPtr.GetTowerInfo(towerInfo)); }
                // TODO~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                _towerInfoPanel.SetTowerInfo(towerInfo);
                _towerInfoPanel.Appear();
                _towerBuildPanel.DisAppear();
                _buildCheckPanel.DisAppear();
                _notificationPanel.DisAppear();
            }
            else
            {
                _tankTowerButton.setTowerEventHandler(this);
                _rangeTowerButton.setTowerEventHandler(this);
                _slowTowerButton.setTowerEventHandler(this);
                // TODO~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                _yesButton.setTileEventHandler(this);
                _towerBuildPanel.Appear();
                _towerOperationPanel.DisAppear();
                _towerInfoPanel.DisAppear();
                _buildCheckPanel.DisAppear();
                _notificationPanel.DisAppear();
            }
        }


    }


    public void SellTower(bool blockCase)
    {
        //Debug.Log("TEH: Trying to sell tower");
        _gameBoard.ClearHighlightTiles();
        Destroy(_towerGameObject);
        _towerExist = false;
        if (! blockCase) // there is money refund for the non-blockCase
        {
            if (0 == _currentTowerType) { _levelManager.AddGold(_tankTowerPtr.SellGain[_tankTowerPtr.GetLevel()]); }
            if (1 == _currentTowerType) { _levelManager.AddGold(_rangeTowerPtr.SellGain[_rangeTowerPtr.GetLevel()]); }
            if (2 == _currentTowerType) { _levelManager.AddGold(_slowTowerPtr.SellGain[_slowTowerPtr.GetLevel()]); }
            // TODO~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        }
        _currentTowerType = -1;
        //Debug.Log("TEH: Tower object removed");
    }

    public void OperationConfirmed()
    {
        _confirmed = true;
    }


    public void SelectTowerType(int type)
    {
        _towerIndex = type;
    }


    public void SetOperation(int op)
    {
        TowerOperation = (Operation) op;
        //Debug.Log("TEH: Operation reset to " + TowerOperation+ " at " + GridX + " " + GridY);
    }


    public Tower GetTowerScript()
    {
        if (0 == _currentTowerType) { return _tankTowerPtr; }
        if (1 == _currentTowerType) { return _rangeTowerPtr; }
        if (2 == _currentTowerType) { return _slowTowerPtr; }
        //TODO~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        return null;
    }


    public void DisplayAttackRange(int range)
    {
        for (uint i = 0; i <= range; i++)
        {
            for (uint j = 0; j <= range; j++)
            {
                if (GridX + i < 10 && GridY + j < 20)
                {
                    _gameBoard.HighlightTileAt(GridX + i, GridY + j);
                }
                if (GridX >= i && GridY >= j)
                {
                    _gameBoard.HighlightTileAt(GridX - i, GridY - j);
                }
                if (i != 0 && j != 0)
                {
                    if (GridX + i < 10 && GridY >= j)
                    {
                        _gameBoard.HighlightTileAt(GridX + i, GridY - j);
                    }
                    if (GridX >= i && GridY + j < 20)
                    {
                        _gameBoard.HighlightTileAt(GridX - i, GridY + j);
                    }
                }
            }
        }
    }
}
