using UnityEngine;
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
    // TODO
    private GameObject _towerGameObject;
    private int _currentTowerType;

    private TowerBuildPanel _towerBuildPanel;
    private TowerOperationPanel _towerOperationPanel;
    private TowerInfoPanel _towerInfoPanel;
    private BuildCheckPanel _buildCheckPanel;
    private NotificationPanel _notificationPanel;

    private TankTowerButton _tankTowerButton;
    private RangeTowerButton _rangeTowerButton;
    // TODO: more tower button here
    private UpgradeButton _upgradeButton;
    private RepairButton _repairButton;
    private SellButton _sellButton;
    private BCP_Yes _yesButton;

    public enum Operation { Nop, TankTower, RangeTower, Tower3, Tower4, Tower5, Upgrade, Repair, Sell}
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
        // TODO: more tower button here
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
        //Debug.Log("~~~~~~~~~~~~~~~~~TEH: Confirmed operation is " + TowerOperation);
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
                case Operation.Tower3:
                    break;
                case Operation.Tower4:
                    break;
                case Operation.Tower5:
                    break;
                case Operation.Upgrade:
                    if (0 == _currentTowerType) { _tankTowerPtr.Upgrade(); }
                    if (1 == _currentTowerType) { _rangeTowerPtr.Upgrade(); }

                    break;
                case Operation.Repair:
                    if (0 == _currentTowerType) { _tankTowerPtr.Repair(); }
                    if (1 == _currentTowerType) { _rangeTowerPtr.Upgrade(); }

                    break;
                case Operation.Sell:
                    //Debug.Log("TEH: Sell operation executing");
                    _currentTowerType = -1;
                    SellTower(false);
                    break;
            }
            _confirmed = false;
        }
    }


    void OnMouseDown()
    {
        //Debug.Log("TEH: Pressed click at " + GridX + "," + GridY + "," + _towerExist);
        if(_levelManager.CurrentGamePhase() == GameBoard.GamePhase.BuildingPhase)
        {
            _gameBoard.ClearHighlightTiles();
            _gameBoard.HighlightTileAt(GridX, GridY);
            if (_towerExist)
            {
                _sellButton.setTowerEventHandler(this);
                _upgradeButton.setTowerEventHandler(this);
                _repairButton.setTowerEventHandler(this);
                _yesButton.setTileEventHandler(this);
                _towerOperationPanel.Appear();
                int[] towerInfo = new int[11];
                if (0 == _currentTowerType)
                {
                    _tankTowerPtr.GetTowerInfo(towerInfo);
                }
                if (1 == _currentTowerType) { _rangeTowerPtr.GetTowerInfo(towerInfo); }

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
                // TODO: set the rest four towers
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
        Debug.Log("TEH: Trying to sell tower");
        _gameBoard.ClearHighlightTiles();
        Destroy(_towerGameObject);
        _towerExist = false;
        if (! blockCase) // there is money refund for the non-blockCase
        {
            if (0 == _currentTowerType) { _levelManager.AddGold(_tankTowerPtr.SellGain[_tankTowerPtr.GetLevel()]); }
            if (1 == _currentTowerType) { _levelManager.AddGold(_rangeTowerPtr.SellGain[_rangeTowerPtr.GetLevel()]); }

        }
        //_tankTowerPtr.Remove();
        //_tankTowerPtr.TowerAnimator.SetTrigger("TowerDestroyed");
        //_tankTowerPtr.DestroyByEnemy = true;
        Debug.Log("TEH: Tower object removed");
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
        Debug.Log("TEH: Operation reset to " + TowerOperation+ " at " + GridX + " " + GridY);
    }


    public Tower GetTowerScript()
    {
        if (0 == _currentTowerType) { return _tankTowerPtr; }
        if (1 == _currentTowerType) { return _rangeTowerPtr; }

        return null;
    }
}
