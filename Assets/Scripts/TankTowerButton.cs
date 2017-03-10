using UnityEngine;
using System.Collections;

public class TankTowerButton : MonoBehaviour {

    public static TankTowerButton Instance;
    private BuildCheckPanel _buildCheckPanel;
    private TowerBuildPanel _towerBuildPanel;
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
            _towerBuildPanel = TowerBuildPanel.Instance;
            _towerInfoPanel = TowerInfoPanel.Instance;
            _towerController = TowerController.Instance;
            _levelManager = GameManager.Instance.CurrentLevelManager;
            _gameBoard = _levelManager.GameBoardSystem;
        }
    }

    public void TankTowerSelected()
    {
        _gameBoard.ClearHighlightTiles();
        _tileEventHandler.SetTowerIndex(0);
        int range = _towerController.CheckAttackRange(0);
        Debug.Log("Range is " + range + " " + _tileEventHandler.GridX + " " + _tileEventHandler.GridY);
        for (uint i = 0; i < range; i++)
        {
            for (uint j = 0; j < range; j++)
            {
                if (_tileEventHandler.GridX + i < 10 && _tileEventHandler.GridY + j < 20)
                {
                    _gameBoard.HighlightTileAt(_tileEventHandler.GridX + i, _tileEventHandler.GridY + j);
                }
                if (_tileEventHandler.GridX >= i && _tileEventHandler.GridY >= j)
                {
                    _gameBoard.HighlightTileAt(_tileEventHandler.GridX - i, _tileEventHandler.GridY - j);
                }
                if (i != 0 && j != 0)
                {
                    if (_tileEventHandler.GridX + i < 10 && _tileEventHandler.GridY >= j)
                    {
                        _gameBoard.HighlightTileAt(_tileEventHandler.GridX + i, _tileEventHandler.GridY - j);
                    }
                    if (_tileEventHandler.GridX >= i && _tileEventHandler.GridY + j < 20)
                    {
                        _gameBoard.HighlightTileAt(_tileEventHandler.GridX - i, _tileEventHandler.GridY + j);
                    }
                }
            }
        }
        _buildCheckPanel.Appear();
        _towerInfoPanel.Appear();
    }
}
