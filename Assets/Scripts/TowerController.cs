using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerController : MonoBehaviour
{
    public static TowerController Instance;
    public List<GameObject> Towers;
    private Tower _towerPtr;
    private GameObject towerGameObject;

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public Tower BuildTower(uint x, uint y, int index)
    {
        Vector3 towerPosition;
        towerPosition.x = x;
        towerPosition.y = y;
        towerPosition.z = 0f;
        towerGameObject = Instantiate(Towers[index], towerPosition, Quaternion.identity) as GameObject;
        _towerPtr = towerGameObject.GetComponent<Tower>(); // get scripts
        return _towerPtr;
    }

    public void RemoveTower()
    {
        _towerPtr.Destory();
    }

    public void RepairTower()
    {
        _towerPtr.Repair();
    }

    public void UpgradeTower()
    {
        _towerPtr.Upgrade();
    }

}
