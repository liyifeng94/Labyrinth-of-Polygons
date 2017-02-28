using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerController : MonoBehaviour
{
    public static TowerController Instance;
    public List<GameObject> Towers;
    private Tower _towerPtr;
    private GameObject towerGameObject;
    private GameBoard _gameBoard;
    private LevelManager _levelManager;

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start ()
    {
        _levelManager = GameManager.Instance.CurrentLevelManager;
        _gameBoard = _levelManager.GameBoardSystem;

    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public GameObject BuildTower(TileEventHandler teh,uint x, uint y, int index)
    {

        Vector3 GamePosition;
        GamePosition = _gameBoard.BoardTiles[x, y].TileObject.transform.position;
        towerGameObject = Instantiate(Towers[index], GamePosition, Quaternion.identity) as GameObject;
        _towerPtr = towerGameObject.GetComponent<Tower>(); // get scripts
        if (_towerPtr.buildCost < _levelManager.GetGold())
        {
            // gole will not be used here, need to check if it blocks the last path later
            Debug.Log("TC: Enough gold to build");
            //_levelManager.UseGold(_towerPtr.buildCost);
        }
        else
        {
            Debug.Log("TC: Not enough gold to build");
            Destroy(towerGameObject);
            return null;
        }
        Debug.Log("TC: Tower object created");
        return towerGameObject;
    }
}
