using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour
{
    public static EnemyController Instance;
    public List<GameObject> Enemies;
    private List<Enemy> _enemies;


    public void SpawnEnemy()
    {
        List<GridSystem.Cell> entrances = GameManager.Instance.CurrentLevelManager.GameBoardSystem.GameGridSystem.MainGameGrid.Entrances;
        List<GridSystem.Cell> path = GameManager.Instance.SearchPathFrom(entrances[0].X, entrances[0].Y);

        //randomly chose a entrance
        GridSystem.Cell startCell = entrances[0];
        GameBoard.Tile starTile = GameManager.Instance.CurrentLevelManager.GameBoardSystem.BoardTiles[startCell.X, startCell.Y];

        Vector3 spawnPosition = starTile.Position;

        GameObject enemeyGameObject = Instantiate(Enemies[0], spawnPosition, Quaternion.identity) as GameObject;
        enemeyGameObject.transform.SetParent(this.transform);
        Enemy enemy = enemeyGameObject.GetComponent<Enemy>();
        //TODO: pathfinding 
        enemy.SetupEnemy(0,0,null);
    }

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
    void Start () {
	
	}

    public bool WaveCleared()
    {
        return _enemies.Count==0;
    }



	// Update is called once per frame
	public void Update () {
	    
	}
}
