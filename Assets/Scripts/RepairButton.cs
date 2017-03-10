using UnityEngine;
using System.Collections;

public class RepairButton : MonoBehaviour {

    public static RepairButton Instance;

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


    public void RepairButtonSelected()
    {
        _tileEventHandler.SetOperation(7);
        _buildCheckPanel.Appear();
        _towerInfoPanel.Appear();
        _notificationPanel.SetNotificationType("Repair");
        _notificationPanel.Appear();
    }
}
