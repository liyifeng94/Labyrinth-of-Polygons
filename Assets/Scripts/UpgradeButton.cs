using UnityEngine;
using System.Collections;

public class UpgradeButton : MonoBehaviour {

    public static UpgradeButton Instance;
    private BuildCheckPanel _buildCheckPanel;
    private TowerInfoPanel _towerInfoPanel;
    private TileEventHandler _tileEventHandler;
    private LevelManager _levelManager;
    private GameBoard _gameBoard;

    void Awake()
    {
        Instance = this;
    }

    public void setTowerEventHandler(TileEventHandler teh)
    {
        _tileEventHandler = teh;
        if (null == _gameBoard)
        {
            _buildCheckPanel = BuildCheckPanel.Instance;
            _towerInfoPanel = TowerInfoPanel.Instance;
            _levelManager = GameManager.Instance.CurrentLevelManager;
            _gameBoard = _levelManager.GameBoardSystem;
        }
    }

    public void UpgradeButtonSelected()
    {
        _tileEventHandler.SetOperation(6);
        _buildCheckPanel.Appear();
        _towerInfoPanel.Appear();
    }
}
