using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;

public class TowerController : MonoBehaviour
{
    public static TowerController Instance;
    public List<GameObject> Towers;
    private TankTower _tankTowerPtr;
    private RangeTower _rangeTowerPtr;
    private SlowTower _slowTowerPtr;
    private HealTower _healTowerPtr;
    private MoneyTower _moneyTowerPtr;
    // TODO~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    private GameObject _towerGameObject;
    private GameBoard _gameBoard;
    private LevelManager _levelManager;
    private HashSet<Enemy> _enemies;
    private HashSet<TileEventHandler> _tileEventHandlers;
    private NotificationPanel _notificationPanel;


    void Awake()
    {
        Instance = this;
    }


	void Start ()
    {
        _levelManager = GameManager.Instance.CurrentLevelManager;
        _gameBoard = _levelManager.GameBoardSystem;
        _notificationPanel = NotificationPanel.Instance;
        _enemies = new HashSet<Enemy>();
        _tileEventHandlers = new HashSet<TileEventHandler>();
    }
	

	void Update ()
    {
        //TODO~~~~~~~~~~~~~~~~~~~~~~~~~~~~ for heal tower stuff
	}


    public GameObject BuildTower(TileEventHandler teh,uint x, uint y, int index)
    {

        Vector3 gamePosition = _gameBoard.BoardTiles[x, y].TileObject.transform.position;
        _towerGameObject = Instantiate(Towers[index], gamePosition, Quaternion.identity) as GameObject;
        switch (index)
        {
            case 0:
                _tankTowerPtr = _towerGameObject.GetComponent<TankTower>(); // get scripts
                if (_tankTowerPtr.BuildCost > _levelManager.GetGold())
                {
                    //Debug.Log("TC: Not enough gold to build");
                    _notificationPanel.SetNotificationType("NotEnoughMoney");
                    _notificationPanel.Appear();
                    Destroy(_towerGameObject);
                    return null;
                }
                break;
            case 1:
                _rangeTowerPtr = _towerGameObject.GetComponent<RangeTower>(); // get scripts
                if (_rangeTowerPtr.BuildCost > _levelManager.GetGold())
                {
                    //Debug.Log("TC: Not enough gold to build");
                    _notificationPanel.SetNotificationType("NotEnoughMoney");
                    _notificationPanel.Appear();
                    Destroy(_towerGameObject);
                    return null;
                }
                break;
            case 2:
                _slowTowerPtr = _towerGameObject.GetComponent<SlowTower>(); // get scripts
                if (_slowTowerPtr.BuildCost > _levelManager.GetGold())
                {
                    //Debug.Log("TC: Not enough gold to build");
                    _notificationPanel.SetNotificationType("NotEnoughMoney");
                    _notificationPanel.Appear();
                    Destroy(_towerGameObject);
                    return null;
                }
                break;
            case 3:
                _healTowerPtr = _towerGameObject.GetComponent<HealTower>(); // get scripts
                if (_healTowerPtr.BuildCost > _levelManager.GetGold())
                {
                    //Debug.Log("TC: Not enough gold to build");
                    _notificationPanel.SetNotificationType("NotEnoughMoney");
                    _notificationPanel.Appear();
                    Destroy(_towerGameObject);
                    return null;
                }
                break;
            case 4:
                _moneyTowerPtr = _towerGameObject.GetComponent<MoneyTower>(); // get scripts
                if (_moneyTowerPtr.BuildCost > _levelManager.GetGold())
                {
                    //Debug.Log("TC: Not enough gold to build");
                    _notificationPanel.SetNotificationType("NotEnoughMoney");
                    _notificationPanel.Appear();
                    Destroy(_towerGameObject);
                    return null;
                }
                break;
            // TODO~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        }
        //Debug.Log("TC: Tower object created");
        return _towerGameObject;
    }


    public int CheckTowerInfo(int index, int[] info) // return the tower range, store other info array
    {
        _towerGameObject = Instantiate(Towers[index], new Vector3(), Quaternion.identity) as GameObject;
        switch (index)
        {
            case 0:
                _tankTowerPtr = _towerGameObject.GetComponent<TankTower>();
                _tankTowerPtr.GetTowerInfo(info);
                _tankTowerPtr.Remove();
                break;
            case 1:
                _rangeTowerPtr = _towerGameObject.GetComponent<RangeTower>();
                _rangeTowerPtr.GetTowerInfo(info);
                _rangeTowerPtr.Remove();
                break;
            case 2:
                _slowTowerPtr = _towerGameObject.GetComponent<SlowTower>();
                _slowTowerPtr.GetTowerInfo(info);
                _slowTowerPtr.Remove();
                break;
            case 3:
                _healTowerPtr = _towerGameObject.GetComponent<HealTower>();
                _healTowerPtr.GetTowerInfo(info);
                _healTowerPtr.Remove();
                break;
            case 4:
                _moneyTowerPtr = _towerGameObject.GetComponent<MoneyTower>();
                _moneyTowerPtr.GetTowerInfo(info);
                _moneyTowerPtr.Remove();
                break;
            // TODO~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        }
        Destroy(_towerGameObject);
        return info[0];
    }


    public void AddEnemy(Enemy enemy)
    {
        _enemies.Add(enemy);
    }


    public void ClearEnemis()
    {
        _enemies.Clear();
    }


    public bool CheckIfEnemyAlive(Enemy enemy)
    {
        return _enemies.Contains(enemy);
    }


    public void RemoveEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);
    }
}
