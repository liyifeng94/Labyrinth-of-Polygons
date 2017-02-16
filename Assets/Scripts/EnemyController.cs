using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour
{
    public static GameManager Instance;
    private List<Enemy> _enemies;
    private float _spawnTimer = Time.time;


    public void SpawnEnemy()
    {
        List<GridSystem.Cell> entrances = Instance.CurrentLevelManager.GameBoardSystem.GameGridSystem.MainGameGrid.Entrances;
        List<GridSystem.Cell> path = Instance.SearchPathFrom(entrances[0].X, entrances[0].Y);
        Enemy newEnemy = new Enemy(entrances[0].X, entrances[0].Y, path);
        _enemies.Add(newEnemy);


    }

	// Use this for initialization
    public void Start () {
	
	}

    public bool WaveCleared()
    {
        return _enemies.Count==0;
    }



	// Update is called once per frame
	public void Update () {
	    
	}
}
