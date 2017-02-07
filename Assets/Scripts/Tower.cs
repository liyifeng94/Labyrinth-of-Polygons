using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

	public uint X { get; private set; }
	public uint Y { get; private set; }
	public uint AttackRange { get; private set; }


	public uint hitPoint;
	public uint attackDamage;
	public uint attackSpeed;  // trsf only
	//private uint targetEnemy;
	//private Manager manager; // class name issue
	//private Block[] block;   // class name issue
	//private uint bIndex;
	//private Enemy target;    // class name issue

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LastUpdate () {
	
	}
}
