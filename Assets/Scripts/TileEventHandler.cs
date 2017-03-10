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
    private Tower _towerPtr;
    private GameObject _towerGameObject;

    private TowerBuildPanel _towerBuildPanel;
    private TowerOperationPanel _towerOperationPanel;
    private TowerInfoPanel _towerInfoPanel;
    private BuildCheckPanel _buildCheckPanel;

    private TankTowerButton _tankTowerButton;
    // TODO: more tower button here
    private UpgradeButton _upgradeButton;
    private RepairButton _repairButton;
    private SellButton _sellButton;
    private BCP_Yes _yesButton;

    public enum Operation { Nop, Tower1, Tower2, Tower3, Tower4, Tower5, Upgrade, Repair, Sell}
    public Operation TowerOperation;


    void Start ()
    {
        _towerExist = false;
        _confirmed = false;
        _towerIndex = -1;
        _clearBeforeBattle = false;

        _levelManager = GameManager.Instance.CurrentLevelManager;
        _gameBoard = _levelManager.GameBoardSystem;
        _towerController = TowerController.Instance;

        _towerBuildPanel = TowerBuildPanel.Instance;
        _towerOperationPanel = TowerOperationPanel.Instance;
        _towerInfoPanel = TowerInfoPanel.Instance;
        _buildCheckPanel = BuildCheckPanel.Instance;

        _tankTowerButton = TankTowerButton.Instance;
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
            _gameBoard.ClearHighlightTiles();
            _clearBeforeBattle = true;
	        return;
	    }
        // make the above code work for the entire game
        if (_levelManager.CurrentGamePhase() == GameBoard.GamePhase.BuildingPhase)
        {
            _clearBeforeBattle = false;
        }

        if (_confirmed)
        {
            switch (TowerOperation)
            {
                case Operation.Nop:
                    break;
                case Operation.Tower1:
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
                        _towerPtr = _towerGameObject.GetComponent<Tower>(); // get scripts
                        _towerPtr.Setup(this);
                        // check if it blocks the last path
                        if (!_gameBoard.BuildTower(_towerPtr))
                        {
                            SellTower(true);
                            _towerExist = false;
                        }
                        else
                        {
                            _levelManager.UseGold(_towerPtr.buildCost);
                        }
                    }
                    break;
                case Operation.Tower2:
                    break;
                case Operation.Tower3:
                    break;
                case Operation.Tower4:
                    break;
                case Operation.Tower5:
                    break;
                case Operation.Upgrade:
                    _towerPtr.Upgrade();
                    break;
                case Operation.Repair:
                    _towerPtr.Repair();
                    break;
                case Operation.Sell:
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
                _towerOperationPanel.Appear();
                _towerInfoPanel.Appear();
                _towerBuildPanel.DisAppear();
                _buildCheckPanel.DisAppear();
            }
            else
            {
                _tankTowerButton.setTowerEventHandler(this);
                // TODO: set the rest four towers
                _yesButton.setTileEventHandler(this);
                _towerBuildPanel.Appear();
                _towerOperationPanel.DisAppear();
                _towerInfoPanel.DisAppear();
                _buildCheckPanel.DisAppear();
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
            _levelManager.AddGold(_towerPtr.sellGain[_towerPtr.GetLevel()]);
        }
        //_towerPtr.Remove();
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
    }
}
