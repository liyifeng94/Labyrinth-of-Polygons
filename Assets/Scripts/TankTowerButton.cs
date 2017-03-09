using UnityEngine;
using System.Collections;

public class TankTowerButton : MonoBehaviour {

    public static TankTowerButton Instance;
    private BuildCheckPanel _buildCheckPanel;
    private TowerBuildPanel _towerBuildPanel;
    private TowerInfoPanel _towerInfoPanel;
    private TileEventHandler _tileEventHandler;
    private TowerController _towerController;
    private LevelManager _levelManager;
    private GameBoard _gameBoard;
    private GameObject towerGameObject;
    private Tower _towerPtr;

    void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start ()   
    {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setTowerEventHandler(TileEventHandler teh)
    {
        _tileEventHandler = teh;
        if (null == _gameBoard)
        {
            _buildCheckPanel = BuildCheckPanel.Instance;
            _towerBuildPanel = TowerBuildPanel.Instance;
            _towerInfoPanel = TowerInfoPanel.Instance;
            _towerController = TowerController.Instance;
            _levelManager = GameManager.Instance.CurrentLevelManager;
            _gameBoard = _levelManager.GameBoardSystem;
        }
    }

    public void TankTowerSelected()
    {
        _tileEventHandler.SetTowerIndex(0);
        _buildCheckPanel.Appear();
        _towerInfoPanel.Appear();
    }
}
