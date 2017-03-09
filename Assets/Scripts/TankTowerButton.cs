using UnityEngine;
using System.Collections;

public class TankTowerButton : MonoBehaviour {

    //public BuildCheckPanel BuildCheckPanel;
    public static TankTowerButton Instance;
    private BuildCheckPanel _buildCheckPanel;
    private TowerInfoPanel _towerInfoPanel;
    private TileEventHandler _tileEventHandler;
    private TowerController _towerController;
    private LevelManager _levelManager;
    private GameBoard _gameBoard;
    private GameObject towerGameObject;
    private Tower _towerPtr;

    void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start ()   
    {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setTowerEventHandler(TileEventHandler teh)
    {
        _tileEventHandler = teh;
        if (null == _gameBoard)
        {
            _buildCheckPanel = BuildCheckPanel.Instance;
            _towerInfoPanel = TowerInfoPanel.Instance;
            _towerController = TowerController.Instance;
            _levelManager = GameManager.Instance.CurrentLevelManager;
            _gameBoard = _levelManager.GameBoardSystem;
        }
    }

    public void TankTowerSelected()
    {
        Debug.Log("TankTowerButton clicked");
        // Debug.Log("TEH: Trying to build a tower build at " + GridX + "," + GridY + "," + _towerExist + " " + _ongui);
        // ask tower controller to build(check avaliable gold)
        towerGameObject = _towerController.BuildTower(_tileEventHandler, _tileEventHandler.GridX, _tileEventHandler.GridY, 0);
        _towerInfoPanel.Appear();
        if (null == towerGameObject)
        {
            Debug.Log("TEH: towerGameObject is null");
        }
        else
        {
            _buildCheckPanel.Appear();
            _tileEventHandler.SetTowerExist(true);
            _towerPtr = towerGameObject.GetComponent<Tower>(); // get scripts
            _towerPtr.Setup(_tileEventHandler);
            // check if it blocks the last path
            if (!_gameBoard.BuildTower(_towerPtr))
            {
                //_tileEventHandler.RemoveTower(true);
                RemoveTower(true);
                _tileEventHandler.SetTowerExist(false);
            }
            else
            {
                _levelManager.UseGold(_towerPtr.buildCost);
            }
        }
        _gameBoard.ClearHighlightTiles();
    }

    void RemoveTower(bool blockCase)
    {
        Destroy(towerGameObject);
        if (!blockCase)
        {
            _levelManager.AddGold(_towerPtr.sellGain[_towerPtr.getLevel()]);
        }
        _towerPtr.Remove();
        Debug.Log("TC: Tower object removed");
    }


}
