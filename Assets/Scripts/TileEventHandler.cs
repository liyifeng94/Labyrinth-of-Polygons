using UnityEngine;
using System.Collections;

public class TileEventHandler : MonoBehaviour
{
    [HideInInspector]
    public uint GridX;
    [HideInInspector]
    public uint GridY;

    private bool _towerExist;
    private TowerController _towerController;

    // Use this for initialization
    void Start ()
    {
        _towerExist = false;
        _towerController = TowerController.Instance;
    }
	
	// Update is called once per frame
	void Update () {

    }

    void OnMouseDown()
    {
        Debug.Log("Pressed click at " + GridX + "," + GridY + "," + _towerExist);
        _towerExist = true;
        //_towerController.BuildTower(GridX, GridY, 1); // make it simple, just tower type 1
    }
}
