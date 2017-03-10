using UnityEngine;
using System.Collections;

public class BCP_Yes : MonoBehaviour
{

    public static BCP_Yes Instance;

    private TileEventHandler _tileEventHandler;
    private GameBoard _gameBoard;

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
        _towerBuildPanel.DisAppear();
        _buildCheckPanel.DisAppear();
        _towerInfoPanel.DisAppear();
        _towerOperationPanel.DisAppear();
        _notificationPanel.DisAppear();
        _gameBoard.ClearHighlightTiles();
        _tileEventHandler.OperationConfirmed();
    }

    public void setTileEventHandler(TileEventHandler teh)
    {
        if (null == _tileEventHandler)
        {
            _gameBoard = GameManager.Instance.CurrentLevelManager.GameBoardSystem;
            _towerBuildPanel = TowerBuildPanel.Instance;
            _buildCheckPanel = BuildCheckPanel.Instance;
            _towerInfoPanel = TowerInfoPanel.Instance;
            _towerOperationPanel = TowerOperationPanel.Instance;
            _notificationPanel = NotificationPanel.Instance;
        }
        _tileEventHandler = teh;
    }


}
