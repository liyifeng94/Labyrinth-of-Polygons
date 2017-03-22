using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearScreenHandler : MonoBehaviour {

    private GameBoard _gameBoard;

    private BuildCheckPanel _buildCheckPanel;
    private TowerInfoPanel _towerInfoPanel;
    private NotificationPanel _notificationPanel;
    private TowerBuildPanel _towerBuildPanel;
    private TowerOperationPanel _towerOperationPanel;


    void Start () {
        _buildCheckPanel = BuildCheckPanel.Instance;
        _towerInfoPanel = TowerInfoPanel.Instance;
        _notificationPanel = NotificationPanel.Instance;
        _towerBuildPanel = TowerBuildPanel.Instance;
        _towerOperationPanel = TowerOperationPanel.Instance;
    }


    void Clicked()
    {
        _buildCheckPanel.DisAppear();
        _towerInfoPanel.DisAppear();
        _notificationPanel.DisAppear();
        _towerBuildPanel.DisAppear();
        _towerOperationPanel.DisAppear();
    }

}
