using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerHandler : MonoBehaviour
{
    public static TowerHandler Instance;
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
        Vector3 towerPosition;
        towerPosition.x = x;
        towerPosition.y = y;
        towerPosition.z = 0f;
        //GameObject towerGameObject = new GameObject();
        //Tower towerPtr = towerGameObject.GetComponent<Tower>();
        //towerGameObject = Instantiate(towerPtr, towerPosition, Quaternion.identity) as GameObject;
        return true;
    }
}
