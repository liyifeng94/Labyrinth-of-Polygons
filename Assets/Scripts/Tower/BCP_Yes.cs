using UnityEngine;

public class BCP_Yes : MonoBehaviour
{

    public static BCP_Yes Instance;

    private TileEventHandler _tileEventHandler;
    private LevelManager _levelManager;
    private GameBoard _gameBoard;

    private bool _upgradeCase;
    private bool _upgradeCost;

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
        if (_upgradeCase)
        {
            if (!EnoughGoldToUpgrade()) // not enough gold to upgrade
            {
                _notificationPanel.DisAppear();
                _notificationPanel.SetNotificationType("NotEnoughMoney");
                _notificationPanel.Appear();
                _buildCheckPanel.DisAppear();
            }
            else
            {
                _tileEventHandler.OperationConfirmed();
            }
        }
        else
        {
            _tileEventHandler.OperationConfirmed();
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
            _levelManager = GameManager.Instance.CurrentLevelManager;
            _gameBoard = _levelManager.GameBoardSystem;
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


    public bool EnoughGoldToUpgrade()
    {
        int uCost = _tileEventHandler.GetCurrentTowerUpgradeCost();
        Debug.Log("gold check: " + uCost + " " + _levelManager.GetGold());
        if (uCost > _levelManager.GetGold())
        {
            return false; // not enough gold to upgrade
        }
        return true;
    }
}
