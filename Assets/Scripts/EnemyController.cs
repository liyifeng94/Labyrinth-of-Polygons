﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour
{

    private List<Enemy> _enemies;
    private float _spawnTimer = Time.time;


	// Use this for initialization
	void Start () {
	
	}

    bool WaveCleared()
    {
        return _enemies.Count==0;
    }



	// Update is called once per frame
	void Update () {
	
	}
}