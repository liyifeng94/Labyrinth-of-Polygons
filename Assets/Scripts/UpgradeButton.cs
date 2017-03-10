using UnityEngine;
using System.Collections;

public class UpgradeButton : MonoBehaviour {

    public static UpgradeButton Instance;

    private TileEventHandler _tileEventHandler;

    private BuildCheckPanel _buildCheckPanel;
    private TowerInfoPanel _towerInfoPanel;
    private NotificationPanel _notificationPanel;


    void Awake()
    {
        Instance = this;
    }


    public void setTowerEventHandler(TileEventHandler teh)
    {
        if (null == _tileEventHandler)
        {
            _buildCheckPanel = BuildCheckPanel.Instance;
            _towerInfoPanel = TowerInfoPanel.Instance;
            _notificationPanel = NotificationPanel.Instance;
        }
        _tileEventHandler = teh;

    }


    public void UpgradeButtonSelected()
    {
        _tileEventHandler.SetOperation(6);
        _buildCheckPanel.Appear();
        _towerInfoPanel.Appear();
        if (_tileEventHandler.GetTowerScript().CheckMaxLevel())
        {
            _notificationPanel.SetNotificationType("MaxLevel");
        }
        else
        {
            _notificationPanel.SetNotificationType("Upgrade");
        }
        _notificationPanel.Appear();
    }
}
