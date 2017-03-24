using UnityEngine;
using System.Collections;

public class RepairButton : MonoBehaviour {

    public static RepairButton Instance;

    private bool _enoughGold;

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
        if (null == _tileEventHandler)
        {
            _buildCheckPanel = BuildCheckPanel.Instance;
            _towerInfoPanel = TowerInfoPanel.Instance;
            _notificationPanel = NotificationPanel.Instance;
        }
        _tileEventHandler = teh;
    }


    public void RepairButtonSelected()
    {
        if (!_enoughGold)
        {
            _notificationPanel.SetNotificationType("NotEnoughMoney");
            _notificationPanel.Appear();
            _buildCheckPanel.DisAppear();
            return;
        }
        _tileEventHandler.SetOperation(7);
        _buildCheckPanel.Appear();
        _towerInfoPanel.RequireCase();
        _towerInfoPanel.ResetTextColor();
        _towerInfoPanel.SetOriginalowerInfo();
        _towerInfoPanel.Appear();
        _notificationPanel.SetNotificationType("Repair");
        _notificationPanel.Appear();
    }


    public void SetGoldCheckFlag()
    {
        _enoughGold = false;
    }
}
