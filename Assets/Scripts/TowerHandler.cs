using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerHandler : MonoBehaviour
{
    public List<GameObject> Towers;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public bool BuildTower(uint x, uint y, uint index)
    {
        GameObject towerGameObject = new GameObject();
        Tower towerPtr = towerGameObject.GetComponent<Tower>();
        return true;
    }
}
