using UnityEngine;
using System.Collections;

public class BCP_No : MonoBehaviour {

    public static BCP_No Instance;
    private GameBoard _gameBoard;

    private TowerBuildPanel _towerBuildPanel;
    private BuildCheckPanel _buildCheckPanel;
    private TowerInfoPanel _towerInfoPanel;
    private TowerOperationPanel _towerOperationPanel;
    private NotificationPanel _notificationPanel;


    void Awake()
    {
        Instance = this;
    }


    public void OperationDenied()
    {
        if (null == _gameBoard)
        {
            _gameBoard = GameManager.Instance.CurrentLevelManager.GameBoardSystem;
            _towerBuildPanel = TowerBuildPanel.Instance;
            _buildCheckPanel = BuildCheckPanel.Instance;
            _towerInfoPanel = TowerInfoPanel.Instance;
            _towerOperationPanel = TowerOperationPanel.Instance;
            _notificationPanel = NotificationPanel.Instance;

        }
        _towerBuildPanel.DisAppear();
        _buildCheckPanel.DisAppear();
        _towerInfoPanel.DisAppear();
        _towerOperationPanel.DisAppear();
        _notificationPanel.DisAppear();
        _gameBoard.ClearHighlightTiles();
    }
}
