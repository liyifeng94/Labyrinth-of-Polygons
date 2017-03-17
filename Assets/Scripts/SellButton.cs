using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SellButton : Button {

    public static SellButton Instance;

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
        _tileEventHandler = teh;
        //Debug.Log("SB: setTowerEventHandler called, position" + _tileEventHandler.GridX + " " + _tileEventHandler.GridY);
        if (null == _buildCheckPanel)
        {
            _buildCheckPanel = BuildCheckPanel.Instance;
            _towerInfoPanel = TowerInfoPanel.Instance;
            _notificationPanel = NotificationPanel.Instance;
        }
    }


    public void SellButtonSelected()
    {
        //Debug.Log("SB: Sell button clicked");
        _tileEventHandler.SetOperation(8);
        _buildCheckPanel.Appear();
        _towerInfoPanel.Appear();
        _notificationPanel.SetNotificationType("Sell");
        _notificationPanel.Appear();
    }
}
