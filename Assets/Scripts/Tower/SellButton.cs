using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SellButton : MonoBehaviour {

    public static SellButton Instance;

    [HideInInspector] public Button SellOptButton;

    private bool _reActive;
    private bool _active;
    private LevelManager _levelManager;
    private TileEventHandler _tileEventHandler;
    private BuildCheckPanel _buildCheckPanel;
    private TowerInfoPanel _towerInfoPanel;
    private NotificationPanel _notificationPanel;


    void Awake()
    {
        Instance = this;
        SellOptButton = GetComponent<Button>();
    }


    void Start()
    {
        _reActive = false;
        _active = true;
        _levelManager = GameManager.Instance.CurrentLevelManager;
    }


    void Update()
    {
        if (!_reActive && _levelManager.CurrentGamePhase() == GameBoard.GamePhase.BuildingPhase)
        {
            this.SellOptButton.interactable = false;
            _reActive = true;
        }
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
        _towerInfoPanel.ResetTextColor();
        _towerInfoPanel.SetOriginalowerInfo();
        _towerInfoPanel.Appear();
        _notificationPanel.SetNotificationType("Sell");
        _notificationPanel.Appear();
    }
}
