using UnityEngine;
using System.Collections;

public class TileEventHandler : MonoBehaviour
{
    [HideInInspector]
    public uint GridX;
    [HideInInspector]
    public uint GridY;

    private bool _towerExist;
    private bool _operationReceived;
    private int _towerIndex;

    private Texture2D _image_1;
    private Texture2D _image_2;
    private Texture2D _image_3;
    private Texture2D _image_4;
    private LevelManager _levelManager;
    private GameBoard _gameBoard;
    private Tower _towerPtr;
    private GameObject towerGameObject;
    private TowerController _towerController;

    private TowerBuildPanel _towerBuildPanel;
    private TowerOperationPanel _towerOperationPanel;
    private TowerInfoPanel _towerInfoPanel;
    private BuildCheckPanel _buildCheckPanel;
    private TankTowerButton _tankTowerButton;
    private SellButton _sellButton;
    private BCP_Yes _yesButton;

    private bool _ongui;
    public enum Operation { Nop, Tower1, Tower2, Tower3, Tower4, Tower5, Upgrade, Repair, Sell}
    public Operation TowerOperation;


    // Use this for initialization
    void Start ()
    {
        _towerExist = false;
        _gameBoard = null;
        _operationReceived = false;
        _towerIndex = -1;
        TowerOperation = Operation.Nop;

        _towerController = TowerController.Instance;
        _levelManager = GameManager.Instance.CurrentLevelManager;
        _gameBoard = _levelManager.GameBoardSystem;

        _towerBuildPanel = TowerBuildPanel.Instance;
        _towerOperationPanel = TowerOperationPanel.Instance;
        _towerInfoPanel = TowerInfoPanel.Instance;
        _buildCheckPanel = BuildCheckPanel.Instance;
        _tankTowerButton = TankTowerButton.Instance;
        _sellButton = SellButton.Instance;
        _yesButton = BCP_Yes.Instance;

        _image_1 = (Texture2D)Resources.Load("TowerImage_1");
        _image_2 = (Texture2D)Resources.Load("SellImage");
        _image_3 = (Texture2D)Resources.Load("Repair");
        _image_4 = (Texture2D)Resources.Load("Upgrade");
    }
	
	void Update () {
        if (_operationReceived)
        {
            switch (TowerOperation)
            {
                case Operation.Nop:
                    break;
                case Operation.Tower1:
                    // Debug.Log("TEH: Trying to build a tower build at " + GridX + "," + GridY + "," + _towerExist + " " + _ongui);
                    // ask tower controller to build(check avaliable gold)
                    towerGameObject = _towerController.BuildTower(this, GridX, GridY, _towerIndex);
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
                    break;
                case Operation.Repair:
                    break;
                case Operation.Sell:
                    SellTower(false);
                    break;
            }
            _operationReceived = false;
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
                // TODO: set repair and upgrade button
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

    void OnGUI()
    {
        return;

#if UNITY_EDITOR
        float x = 4;
        float y = 0;
        int size = 30;
#else
        float x = 40;
        float y = 500;
        int size = 90;

#endif
        if (_towerExist)
        {
            // remove case
            if (_ongui && GUI.Button(new Rect(x + 4, y, size, size), _image_2))
            {
                _ongui = false;
                _towerExist = false;
                Debug.Log("TEH: Trying to sell tower");
                SellTower(false);
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
                        SellTower(true);
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
        _towerPtr.Repair();
    }


    public void SellTower(bool blockCase)
    {
        Debug.Log("TEH: Trying to sell tower");
        //RemoveTower(false);
        _gameBoard.ClearHighlightTiles();
        Destroy(towerGameObject);
        _towerExist = false;
        if (! blockCase) // there is money refund for the non-blockCase
        {
            _levelManager.AddGold(_towerPtr.sellGain[_towerPtr.getLevel()]);
        }
        _towerPtr.Remove();
        Debug.Log("TEH: Tower object removed");
    }

    public void UpgradeTower()
    {
        _towerPtr.Upgrade();
    }

    public void SetTowerExist(bool towerExist)
    {
        _towerExist = towerExist;
    }

    public void SetYes()
    {
        _operationReceived = true;
    }

    public void SetTowerIndex(int i)
    {
        _towerIndex = i;
    }

    public void SetOperation(int op)
    {
        TowerOperation = (Operation) op;
    }
}
