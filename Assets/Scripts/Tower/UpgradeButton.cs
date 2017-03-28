using UnityEngine;
using System.Collections;

public class UpgradeButton : MonoBehaviour {

    public static UpgradeButton Instance;

    private bool _enoughGold;
    private bool _destroy;

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
        _enoughGold = true;
        _destroy = false;
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
        if (! _enoughGold)
        {
            _notificationPanel.SetNotificationType("NotEnoughMoney");
            _notificationPanel.Appear();
            _buildCheckPanel.DisAppear();
            _towerInfoPanel.ResetTextColor();
            return;
        }
        if (_destroy)
        {
            _notificationPanel.SetNotificationType("UpgradeWhenDestroied");
            _notificationPanel.Appear();
            _buildCheckPanel.DisAppear();
            _towerInfoPanel.ResetTextColor();
            return;
        }
        _tileEventHandler.SetOperation(6);
        _buildCheckPanel.Appear();
        _towerInfoPanel.SetUpgradingColor();
        _towerInfoPanel.DisplayUpgradedInfo();
        _towerInfoPanel.Appear();
        /*if (_tileEventHandler.GetTowerScript().CheckMaxLevel())
        {
            _notificationPanel.SetNotificationType("MaxLevel");
        }
        else
        {
            _notificationPanel.SetNotificationType("Upgrade");
        }*/
        _notificationPanel.SetNotificationType("Upgrade");
        _notificationPanel.Appear();
    }


    public void SetGoldCheckFlag()
    {
        _enoughGold = false;
    }


    public void setDestroyFlag()
    {
        _destroy = true;
    }
}
