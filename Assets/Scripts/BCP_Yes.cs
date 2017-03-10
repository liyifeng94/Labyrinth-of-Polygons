using UnityEngine;
using System.Collections;

public class BCP_Yes : MonoBehaviour
{

    public static BCP_Yes Instance;
    private TileEventHandler _tileEventHandler;
    private LevelManager _levelManager;
    private GameBoard _gameBoard;
    private TowerBuildPanel _towerBuildPanel;
    private TowerOperationPanel _towerOperationPanel;
    private BuildCheckPanel _buildCheckPanel;
    private TowerInfoPanel _towerInfoPanel;

    void Awake()
    {
        Instance = this;
    }

    void Start () {

    }

	void Update () {
	
	}

    public void OperationConfirmed()
    {
        _towerBuildPanel.DisAppear();
        _buildCheckPanel.DisAppear();
        _towerInfoPanel.DisAppear();
        _towerOperationPanel.DisAppear();
        _gameBoard.ClearHighlightTiles();
        _tileEventHandler.SetYes();
    }

    public void setTileEventHandler(TileEventHandler teh)
    {
        if (null == _gameBoard)
        {
            _levelManager = GameManager.Instance.CurrentLevelManager;
            _gameBoard = _levelManager.GameBoardSystem;
            _towerBuildPanel = TowerBuildPanel.Instance;
            _buildCheckPanel = BuildCheckPanel.Instance;
            _towerInfoPanel = TowerInfoPanel.Instance;
            _towerOperationPanel = TowerOperationPanel.Instance;
        }
        _tileEventHandler = teh;
    }
}
