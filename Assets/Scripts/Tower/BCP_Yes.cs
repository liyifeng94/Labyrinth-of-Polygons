using UnityEngine;
using System.Collections;

public class BCP_Yes : MonoBehaviour
{

    public static BCP_Yes Instance;

    private TileEventHandler _tileEventHandler;
    private GameBoard _gameBoard;

    private bool _upgradeCase;

    private TowerBuildPanel _towerBuildPanel;
    private TowerOperationPanel _towerOperationPanel;
    private BuildCheckPanel _buildCheckPanel;
    private TowerInfoPanel _towerInfoPanel;
    private NotificationPanel _notificationPanel;


    void Awake()
    {
        Instance = this;
    }


    public void OperationConfirmed()
    {
        _tileEventHandler.OperationConfirmed();
        _towerBuildPanel.DisAppear();
        if (! _upgradeCase)
        {
            _buildCheckPanel.DisAppear();
            _towerInfoPanel.DisAppear();
            _towerOperationPanel.DisAppear();
            _notificationPanel.DisAppear();
            _gameBoard.ClearHighlightTiles();
        }
    }


    public void setTileEventHandler(TileEventHandler teh)
    {
        _upgradeCase = false;
        _tileEventHandler = teh;
        if (null == _gameBoard)
        {
            _gameBoard = GameManager.Instance.CurrentLevelManager.GameBoardSystem;
            _towerBuildPanel = TowerBuildPanel.Instance;
            _buildCheckPanel = BuildCheckPanel.Instance;
            _towerInfoPanel = TowerInfoPanel.Instance;
            _towerOperationPanel = TowerOperationPanel.Instance;
            _notificationPanel = NotificationPanel.Instance;
        }
    }


    public void SetUpgradeCase()
    {
        _upgradeCase = true;
    }
}
