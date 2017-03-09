using UnityEngine;
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
    private TowerBuildPanel _towerBuildPanel;
    private LevelManager _levelManager;
    private GameBoard _gameBoard;
    private Tower _towerPtr;
    private GameObject towerGameObject;
    private Texture2D _image_1;
    private Texture2D _image_2;
    private Texture2D _image_3;
    private Texture2D _image_4;
    private TankTowerButton _tankTowerButton;
    private BCP_Yes _bcp_yes;
    private bool _yes;
    private int _towerIndex;


    // Use this for initialization
    void Start ()
    {
        _towerExist = false;
        _ongui = false;
        _gameBoard = null;
        _yes = false;
        _towerIndex = -1;

        _towerController = TowerController.Instance;
        _towerBuildPanel = TowerBuildPanel.Instance;
        _tankTowerButton = TankTowerButton.Instance;
        _bcp_yes = BCP_Yes.Instance;
        _levelManager = GameManager.Instance.CurrentLevelManager;
        _gameBoard = _levelManager.GameBoardSystem;

        _image_1 = (Texture2D)Resources.Load("TowerImage_1");
        _image_2 = (Texture2D)Resources.Load("SellImage");
        _image_3 = (Texture2D)Resources.Load("Repair");
        _image_4 = (Texture2D)Resources.Load("Upgrade");
    }
	
	void Update () {
        if (_yes)
        {
            //Debug.Log("TEH: Trying to build a tower build at " + GridX + "," + GridY + "," + _towerExist + " " + _ongui);
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
                    RemoveTower(true);
                    _towerExist = false;
                }
                else
                {
                    _levelManager.UseGold(_towerPtr.buildCost);
                }
            }
            _yes = false;
        }
    }

    void OnMouseDown()
    {
        //Debug.Log("TEH: Pressed click at " + GridX + "," + GridY + "," + _towerExist + " " + _ongui);
        _ongui = true;
        if(!_towerExist && _levelManager.CurrentGamePhase() == GameBoard.GamePhase.BuildingPhase)
        {
            _gameBoard.HighlightTileAt(GridX, GridY);
            if (_towerExist)
            {
                // TODO: remove, repaire, sell cases
                Debug.Log("TEH: click on an existing tower... do something?");
            }
            else
            {
                _tankTowerButton.setTowerEventHandler(this);
                // TODO: set the rest four towers
                _bcp_yes.setTileEventHandler(this);
                _towerBuildPanel.Appear();
            }
        }


    }

    void OnGUI()
    {
        return;
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
        if (_towerExist)
        {
            // remove case
            if (_ongui && GUI.Button(new Rect(x + 4, y, size, size), _image_2))
            {
                _ongui = false;
                _towerExist = false;
                Debug.Log("TEH: Trying to sell tower");
                RemoveTower(false);
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
                        RemoveTower(true);
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


    public void RemoveTower(bool blockCase)
    {
        Destroy(towerGameObject);
        _towerExist = false;
        // cant move the last line in tower.cs, when tower is destroy by enemy, receive 0 gold
        if (! blockCase)
        {
            _levelManager.AddGold(_towerPtr.sellGain[_towerPtr.getLevel()]);
        }
        _towerPtr.Remove();
        Debug.Log("TC: Tower object removed");
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
        _yes = true;
    }

    public void SetTowerIndex(int i)
    {
        _towerIndex = i;
    }
}
