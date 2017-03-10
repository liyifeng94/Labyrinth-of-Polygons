using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerController : MonoBehaviour
{
    public static TowerController Instance;
    public List<GameObject> Towers;
    private Tower _towerPtr;
    private GameObject _towerGameObject;
    private GameBoard _gameBoard;
    private LevelManager _levelManager;
    private HashSet<Enemy> _enemies;

    void Awake()
    {
        Instance = this;
    }


	void Start ()
    {
        _levelManager = GameManager.Instance.CurrentLevelManager;
        _gameBoard = _levelManager.GameBoardSystem;
        _enemies = new HashSet<Enemy>();
    }
	

	void Update ()
    {
	
	}


    public GameObject BuildTower(TileEventHandler teh,uint x, uint y, int index)
    {

        Vector3 gamePosition = _gameBoard.BoardTiles[x, y].TileObject.transform.position;
        _towerGameObject = Instantiate(Towers[index], gamePosition, Quaternion.identity) as GameObject;
        _towerPtr = _towerGameObject.GetComponent<Tower>(); // get scripts
        if (_towerPtr.buildCost < _levelManager.GetGold())
        {
            // gold will not be used here, need to check if it blocks the last path later
            Debug.Log("TC: Enough gold to build");
        }
        else
        {
            Debug.Log("TC: Not enough gold to build");
            // TODO: display the message
            Destroy(_towerGameObject);
            return null;
        }
        Debug.Log("TC: Tower object created");
        return _towerGameObject;
    }


    public int CheckTowerInfo(int index, int[] info) // return the tower range, store other info array
    {
        _towerGameObject = Instantiate(Towers[index], new Vector3(), Quaternion.identity) as GameObject;
        _towerPtr = _towerGameObject.GetComponent<Tower>();
        _towerPtr.GetTowerInfo(info);
        _towerPtr.Remove();
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
