using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TileEventHandler : MonoBehaviour
{
    [HideInInspector]
    public int GridX;
    [HideInInspector]
    public int GridY;

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
    private HealTower _healTowerPtr;
    private GoldTower _goldTowerPtr;
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
    private HealTowerButton _healTowerButton;
    private GoldTowerButton _goldTowerButton;
    // TODO~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    private UpgradeButton _upgradeButton;
    private RepairButton _repairButton;
    private SellButton _sellButton;
    private BCP_Yes _yesButton;

    public enum Operation { Nop, TankTower, RangeTower, SlowTower, HealTower, GoldTower, Upgrade, Repair, Sell}
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
        _healTowerButton = HealTowerButton.Instance;
        _goldTowerButton = GoldTowerButton.Instance;
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
                            _towerController.AddTileEventHandler(this);
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
                            _towerController.AddTileEventHandler(this);
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
                            _towerController.AddTileEventHandler(this);
                        }
                    }
                    break;
                case Operation.HealTower:
                    // Debug.Log("TEH: Trying to build a tower build at " + GridX + "," + GridY + "," + _towerExist + " " + _ongui);
                    // ask tower controller to build(check avaliable gold)
                    _towerGameObject = _towerController.BuildTower(this, GridX, GridY, _towerIndex);
                    if (null == _towerGameObject)
                    {
                        //Debug.Log("TEH: not enough money or blocking the path");
                    }
                    else
                    {
                        _towerExist = true;
                        _currentTowerType = 3;
                        _healTowerPtr = _towerGameObject.GetComponent<HealTower>(); // get scripts
                        _healTowerPtr.Setup(this);
                        // check if it blocks the last path
                        if (!_gameBoard.BuildTower(_healTowerPtr))
                        {
                            SellTower(true);
                            _towerExist = false;
                            //_notificationPanel.DisAppear();
                            _notificationPanel.SetNotificationType("Block");
                            _notificationPanel.Appear();
                        }
                        else
                        {
                            _levelManager.UseGold(_healTowerPtr.BuildCost);
                            _towerController.AddTileEventHandler(this);
                            _towerController.AddHealTileEventHandler(this);
                        }
                    }
                    break;
                case Operation.GoldTower:
                    // Debug.Log("TEH: Trying to build a tower build at " + GridX + "," + GridY + "," + _towerExist + " " + _ongui);
                    // ask tower controller to build(check avaliable gold)
                    _towerGameObject = _towerController.BuildTower(this, GridX, GridY, _towerIndex);
                    if (null == _towerGameObject)
                    {
                        //Debug.Log("TEH: towerGameObject is null");
                    }
                    else
                    {
                        _towerExist = true;
                        _currentTowerType = 4;
                        _goldTowerPtr = _towerGameObject.GetComponent<GoldTower>(); // get scripts
                        _goldTowerPtr.Setup(this);
                        // check if it blocks the last path
                        if (!_gameBoard.BuildTower(_goldTowerPtr))
                        {
                            SellTower(true);
                            _towerExist = false;
                            //_notificationPanel.DisAppear();
                            _notificationPanel.SetNotificationType("Block");
                            _notificationPanel.Appear();
                        }
                        else
                        {
                            _levelManager.UseGold(_goldTowerPtr.BuildCost);
                            _towerController.AddTileEventHandler(this);
                        }
                    }
                    break;
                case Operation.Upgrade:
                    //Debug.Log("TEH: Upgrade Operation called");
                    if (0 == _currentTowerType) _tankTowerPtr.Upgrade();
                    if (1 == _currentTowerType) _rangeTowerPtr.Upgrade();
                    if (2 == _currentTowerType) _slowTowerPtr.Upgrade();
                    if (3 == _currentTowerType) _healTowerPtr.Upgrade();
                    if (4 == _currentTowerType) _goldTowerPtr.Upgrade();
                    break;
                case Operation.Repair:
                    if (0 == _currentTowerType)
                    {
                        if (_tankTowerPtr.RepairCost > _levelManager.GetGold())
                        {
                            _notificationPanel.SetNotificationType("NotEnoughMoney");
                            _notificationPanel.Appear();
                            break;
                        }
                        _tankTowerPtr.Repair();
                    }
                    if (1 == _currentTowerType)
                    {
                        if (_rangeTowerPtr.RepairCost > _levelManager.GetGold())
                        {
                            _notificationPanel.SetNotificationType("NotEnoughMoney");
                            _notificationPanel.Appear();
                            break;
                        }
                        _rangeTowerPtr.Repair();
                    }
                    if (2 == _currentTowerType)
                    {
                        if (_slowTowerPtr.RepairCost > _levelManager.GetGold())
                        {
                            _notificationPanel.SetNotificationType("NotEnoughMoney");
                            _notificationPanel.Appear();
                            break;
                        }
                        _slowTowerPtr.Repair();
                    }
                    if (3 == _currentTowerType)
                    {
                        if (_healTowerPtr.RepairCost > _levelManager.GetGold())
                        {
                            _notificationPanel.SetNotificationType("NotEnoughMoney");
                            _notificationPanel.Appear();
                            break;
                        }
                        _healTowerPtr.Repair();
                    }
                    if (4 == _currentTowerType)
                    {
                        if (_goldTowerPtr.RepairCost > _levelManager.GetGold())
                        {
                            _notificationPanel.SetNotificationType("NotEnoughMoney");
                            _notificationPanel.Appear();
                            break;
                        }
                        _goldTowerPtr.Repair();
                    }
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
        _gameBoard.ClearHighlightTiles();
        _towerInfoPanel.ResetTextColor();
        if (_towerExist)
        {
            _sellButton.SellOptButton.interactable = true;
            if (_levelManager.CurrentGamePhase() == GameBoard.GamePhase.BattlePhase)
            {
                _sellButton.SellOptButton.interactable = false;
            }
            //Debug.Log("TEH: Click on an existing tower at " + GridX + "," + GridY + ", type is " + _currentTowerType);
            _sellButton.setTowerEventHandler(this);
            _upgradeButton.setTowerEventHandler(this);
            _repairButton.setTowerEventHandler(this);
            _yesButton.setTileEventHandler(this);
            _towerOperationPanel.Appear();
            int[] towerInfo = new int[11];
            int[] upgradedTowerInfo = new int[11];
            // highlight attack range and if there is enough money to upgrade or repair
            if (0 == _currentTowerType)
            {
                DisplayAttackRange(_tankTowerPtr.GetTowerInfo(towerInfo));
                _tankTowerPtr.GetTowerUpgradedInfo(upgradedTowerInfo);
                _towerInfoPanel.SetTankTower(_tankTowerPtr);
                if (_tankTowerPtr.UpgradeCost > _levelManager.GetGold()) _upgradeButton.SetGoldCheckFlag();
                if (_tankTowerPtr.RepairCost > _levelManager.GetGold()) _repairButton.SetGoldCheckFlag();
            }
            if (1 == _currentTowerType)
            {
                DisplayAttackRange(_rangeTowerPtr.GetTowerInfo(towerInfo));
                _rangeTowerPtr.GetTowerUpgradedInfo(upgradedTowerInfo);
                _towerInfoPanel.SetRangeTower(_rangeTowerPtr);
                if (_rangeTowerPtr.UpgradeCost > _levelManager.GetGold()) _upgradeButton.SetGoldCheckFlag();
                if (_rangeTowerPtr.RepairCost > _levelManager.GetGold()) _repairButton.SetGoldCheckFlag();
            }
            if (2 == _currentTowerType)
            {
                DisplayAttackRange(_slowTowerPtr.GetTowerInfo(towerInfo));
                _slowTowerPtr.GetTowerUpgradedInfo(upgradedTowerInfo);
                _towerInfoPanel.SetSlowTower(_slowTowerPtr);
                if (_slowTowerPtr.UpgradeCost > _levelManager.GetGold()) _upgradeButton.SetGoldCheckFlag();
                if (_slowTowerPtr.RepairCost > _levelManager.GetGold()) _repairButton.SetGoldCheckFlag();
            }
            if (3 == _currentTowerType)
            {
                DisplayAttackRange(_healTowerPtr.GetTowerInfo(towerInfo));
                _healTowerPtr.GetTowerUpgradedInfo(upgradedTowerInfo);
                _towerInfoPanel.SetHealTower(_healTowerPtr);
                if (_healTowerPtr.UpgradeCost > _levelManager.GetGold()) _upgradeButton.SetGoldCheckFlag();
                if (_healTowerPtr.RepairCost > _levelManager.GetGold()) _repairButton.SetGoldCheckFlag();
            }
            if (4 == _currentTowerType)
            {
                DisplayAttackRange(_goldTowerPtr.GetTowerInfo(towerInfo));
                _goldTowerPtr.GetTowerUpgradedInfo(upgradedTowerInfo);
                _towerInfoPanel.SetGoldTower(_goldTowerPtr);
                if (_goldTowerPtr.UpgradeCost > _levelManager.GetGold()) _upgradeButton.SetGoldCheckFlag();
                if (_goldTowerPtr.RepairCost > _levelManager.GetGold()) _repairButton.SetGoldCheckFlag();
            }
            // TODO~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            _towerInfoPanel.SetTowerInfo(towerInfo);
            _towerInfoPanel.SetUpgradedTowerInfo(upgradedTowerInfo);
            _towerInfoPanel.Appear();
            _towerBuildPanel.DisAppear();
            _buildCheckPanel.DisAppear();
            _notificationPanel.DisAppear();
        }
        else
        {
            _gameBoard.HighlightTileAt(GridX, GridY, new Color(0f, 1f, 0.2f, 0.5f));
            if (GameBoard.GamePhase.BattlePhase == _gameBoard.CurrentGamePhase)
            {
                _gameBoard.ClearHighlightTiles();
                _buildCheckPanel.DisAppear();
                _towerInfoPanel.DisAppear();
                _towerOperationPanel.DisAppear();
                _notificationPanel.DisAppear();
                return;
            }
            _tankTowerButton.setTowerEventHandler(this);
            _rangeTowerButton.setTowerEventHandler(this);
            _slowTowerButton.setTowerEventHandler(this);
            _healTowerButton.setTowerEventHandler(this);
            _goldTowerButton.setTowerEventHandler(this);
            // TODO~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            _yesButton.setTileEventHandler(this);
            _towerBuildPanel.Appear();
            _towerOperationPanel.DisAppear();
            _towerInfoPanel.DisAppear();
            _buildCheckPanel.DisAppear();
            _notificationPanel.DisAppear();
        }

    }


    public void SellTower(bool blockCase)
    {
        //Debug.Log("TEH: Trying to sell tower");
        _gameBoard.ClearHighlightTiles();
        _towerExist = false;
        if (! blockCase) // there is money refund for the non-blockCase
        {
            if (0 == _currentTowerType)
            {
                _levelManager.AddGold(_tankTowerPtr.SellGain);
                _gameBoard.RemoveTower(_tankTowerPtr);
            }
            if (1 == _currentTowerType)
            {
                _levelManager.AddGold(_rangeTowerPtr.SellGain);
                _gameBoard.RemoveTower(_rangeTowerPtr);
            }
            if (2 == _currentTowerType)
            {
                _levelManager.AddGold(_slowTowerPtr.SellGain);
                _gameBoard.RemoveTower(_slowTowerPtr);
            }
            if (3 == _currentTowerType)
            {
                _levelManager.AddGold(_healTowerPtr.SellGain);
                _gameBoard.RemoveTower(_healTowerPtr);
            }
            if (4 == _currentTowerType)
            {
                _levelManager.AddGold(_goldTowerPtr.SellGain);
                _gameBoard.RemoveTower(_goldTowerPtr);
            }
            // TODO~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            _towerController.RemoveTileEventHandler(this);
        }
        _currentTowerType = -1;
        Destroy(_towerGameObject);
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
        if (3 == _currentTowerType) { return _healTowerPtr; }
        if (4 == _currentTowerType) { return _goldTowerPtr; }
        //TODO~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        return null;
    }


    public void DisplayAttackRange(int range)
    {
        for (int i = 0; i <= range; i++)
        {
            for (int j = 0; j <= range; j++)
            {
                if (0 == i && 0 == j)
                {
                    _gameBoard.HighlightTileAt(GridX + i, GridY + j, new Color(0, 1, 0.2f, 0.5f));
                    continue;
                }
                if (GridX + i < 10 && GridY + j < 20)
                {
                    _gameBoard.HighlightTileAt(GridX + i, GridY + j, new Color(1,0,0,0.25f));
                }
                if (GridX >= i && GridY >= j)
                {
                    _gameBoard.HighlightTileAt(GridX - i, GridY - j, new Color(1, 0, 0, 0.25f));
                }
                if (i != 0 && j != 0)
                {
                    if (GridX + i < 10 && GridY >= j)
                    {
                        _gameBoard.HighlightTileAt(GridX + i, GridY - j, new Color(1, 0, 0, 0.25f));
                    }
                    if (GridX >= i && GridY + j < 20)
                    {
                        _gameBoard.HighlightTileAt(GridX - i, GridY + j, new Color(1, 0, 0, 0.25f));
                    }
                }
            }
        }
    }


    public int GetHealTowerRange()
    {
        return _healTowerPtr.AttackRange;
    }


    public HealTower GetHealTowerPtr()
    {
        return _healTowerPtr;
    }    
}
