using UnityEngine;
using System.Collections;

public class SellButton : MonoBehaviour {

    public static SellButton Instance;
    private BuildCheckPanel _buildCheckPanel;
    //private TowerBuildPanel _towerBuildPanel;
    private TowerInfoPanel _towerInfoPanel;
    private TileEventHandler _tileEventHandler;
    //private TowerController _towerController;
    private LevelManager _levelManager;
    private GameBoard _gameBoard;
    private GameObject towerGameObject;
    private Tower _towerPtr;

    void Awake()
    {
        Instance = this;
    }

    void Start () {
	
	}
	
	void Update () {
	
	}

    public void setTowerEventHandler(TileEventHandler teh)
    {
        _tileEventHandler = teh;
        if (null == _gameBoard)
        {
            _buildCheckPanel = BuildCheckPanel.Instance;
            //_towerBuildPanel = TowerBuildPanel.Instance;
            _towerInfoPanel = TowerInfoPanel.Instance;
            //_towerController = TowerController.Instance;
            _levelManager = GameManager.Instance.CurrentLevelManager;
            _gameBoard = _levelManager.GameBoardSystem;
        }
    }

    public void SellButtonSelected()
    {
        //_gameBoard.ClearHighlightTiles();
        _tileEventHandler.SetOperation(8);
        _buildCheckPanel.Appear();
        _towerInfoPanel.Appear();
    }
}
