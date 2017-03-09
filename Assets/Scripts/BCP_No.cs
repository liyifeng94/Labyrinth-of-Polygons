using UnityEngine;
using System.Collections;

public class BCP_No : MonoBehaviour {

    public static BCP_No Instance;
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

    void Start()
    {

    }

    void Update () {
	
	}

    public void BuildDenied()
    {
        if (null == _gameBoard)
        {
            _towerBuildPanel = TowerBuildPanel.Instance;
            _buildCheckPanel = BuildCheckPanel.Instance;
            _towerInfoPanel = TowerInfoPanel.Instance;
            _levelManager = GameManager.Instance.CurrentLevelManager;
            _gameBoard = _levelManager.GameBoardSystem;
        }
        _towerBuildPanel.DisAppear();
        _buildCheckPanel.DisAppear();
        _towerInfoPanel.DisAppear();
        _gameBoard.ClearHighlightTiles();
    }
}
