using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    private GameManager _gameManagerInstance;
    public GridSystem GameGridSystem { get; private set; }

    public uint GridWidth = 10;
    public uint GridHeight = 20;
    public uint GridEntrances = 4;
    public uint GridObstacles = 0;

    public GameObject CellGound;
    public GameObject CellEntrance;
    public GameObject CellExit;
    public GameObject CellObstacles;




    // Use this for initialization
    void Awake ()
	{
	    GameManager.Instance.CurrentLevelManager = this;
        GameGridSystem = new GridSystem(GridWidth,GridHeight,GridEntrances,GridObstacles);
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
