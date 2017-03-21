using UnityEngine;
using System.Collections;

public class HealTowerButton : MonoBehaviour {

    public static HealTowerButton Instance;

    private GameBoard _gameBoard;

    private BuildCheckPanel _buildCheckPanel;
    private TowerInfoPanel _towerInfoPanel;
    private TileEventHandler _tileEventHandler;
    private TowerController _towerController;
    private NotificationPanel _notificationPanel;


    void Awake()
    {
        Instance = this;
    }

    public void setTowerEventHandler(TileEventHandler teh)
    {
        _tileEventHandler = teh;
        if (null == _gameBoard)
        {
            _towerController = TowerController.Instance; // used for check range
            _gameBoard = GameManager.Instance.CurrentLevelManager.GameBoardSystem; // used for highlight

            _buildCheckPanel = BuildCheckPanel.Instance; // used for set appear
            _towerInfoPanel = TowerInfoPanel.Instance; // used for set appear
            _notificationPanel = NotificationPanel.Instance; // used for notification, set appear
        }
    }

    public void HealTowerSelected()
    {
        _gameBoard.ClearHighlightTiles();
        _tileEventHandler.SelectTowerType(3);
        _tileEventHandler.SetOperation(4);
        int[] towerInfo = new int[11];
        int range = _towerController.CheckTowerInfo(3, towerInfo);
        //Debug.Log("Range is " + range + " " + _tileEventHandler.GridX + " " + _tileEventHandler.GridY);
        _tileEventHandler.DisplayAttackRange(range);
        _buildCheckPanel.Appear();
        _towerInfoPanel.SetTowerInfo(towerInfo);
        _towerInfoPanel.Appear();
        _notificationPanel.SetNotificationType("Heal");
        _notificationPanel.Appear();
    }
}