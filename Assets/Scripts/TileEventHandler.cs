using UnityEngine;
using System.Collections;

public class TileEventHandler : MonoBehaviour
{
    [HideInInspector]
    public uint GridX;
    [HideInInspector]
    public uint GridY;

    private bool _towerExist;
    private TowerHandler _towerHandler;

    // Use this for initialization
    void Start ()
    {
        _towerExist = false;
        _towerHandler = TowerHandler.Instance;
    }
	
	// Update is called once per frame
	void Update () {

    }

    void OnMouseDown()
    {
        Debug.Log("Pressed click at " + GridX + "," + GridY + "," + _towerExist);
        _towerExist = true;
        //_towerHandler.BuildTower(GridX, GridY, 1); // make it simple, just tower type 1
    }
}
