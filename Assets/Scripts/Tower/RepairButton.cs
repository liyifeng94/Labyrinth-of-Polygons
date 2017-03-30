using UnityEngine;

public class RepairButton : MonoBehaviour {

    public static RepairButton Instance;

    private bool _enoughGold;
    private bool _fullHp;

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
        _fullHp = false;
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
        if (_fullHp)
        {
            _notificationPanel.SetNotificationType("RepairWithFullHp");
            _notificationPanel.Appear();
            _buildCheckPanel.DisAppear();
            _towerInfoPanel.SetOriginalowerInfo();
            _towerInfoPanel.ResetTextColor();
            return;
        }
        _tileEventHandler.SetOperation(7);
        _buildCheckPanel.Appear();
        _towerInfoPanel.RepairCase();
        _towerInfoPanel.ResetTextColor();
        _towerInfoPanel.SetOriginalowerInfo();
        _towerInfoPanel.Appear();
        _notificationPanel.SetNotificationType("Repair");
        _notificationPanel.Appear();
    }


    // tileEventHandler would call this function when a existing tower is clicked
    public void SetGoldCheckFlag()
    {
        _enoughGold = false;
    }

    
    // tileEventHandler would call this function when a existing tower is clicked
    public void SetHpCheckFlag()
    {
        _fullHp = true;
    }


    public void ResetHpCheckFlag()
    {
        _fullHp = false;
    }
}
