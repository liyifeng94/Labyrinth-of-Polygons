using UnityEngine;
using System.Collections.Generic;

public class GameBoard : MonoBehaviour
{
    private Transform _boardHolder;

    private HashSet<Enemy> _enemiesHolder;

    private HashSet<Tower> _towersHolder;

    public GridSystem GameGridSystem { get; private set; }

    public uint BlockSize = 10;
    public uint GridWidth = 10;
    public uint GridHeight = 20;
    public uint GridEntrances = 4;
    public uint GridObstacles = 0;

    public GameObject CellGound;
    public GameObject CellEntrance;
    public GameObject CellExit;
    public GameObject CellObstacles;


    public class Tile
    {
        public GameObject TileObject { get; private set; }

        public readonly Vector3 Position;
        public readonly uint GridX;
        public readonly uint GridY;
        public readonly uint BlockSize;

        public Tile(GameObject gameObject, uint x, uint y, uint blockSize, Transform parentTransform)
        {
            GridX = x;
            GridY = y;
            BlockSize = blockSize;
            Vector3 tilePosition;

            tilePosition.x = GridX * blockSize + blockSize / 2;
            tilePosition.y = GridY * blockSize + blockSize / 2;
            tilePosition.z = 0f;

            Position = tilePosition;

            TileObject = Instantiate(gameObject, tilePosition, Quaternion.identity) as GameObject;

            TileObject.transform.SetParent(parentTransform);
        }
    }

    // Use this for initialization
    void Start()
    {
        GameBoardSetup();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GameBoardSetup()
    {
        _boardHolder = new GameObject("Board").transform;
        _enemiesHolder = new HashSet<Enemy>();
        _towersHolder = new HashSet<Tower>();
        GameGridSystem = new GridSystem(GridWidth, GridHeight, GridEntrances, GridObstacles);
    }

    // Updates and checks if a tower can be build at grind position
    public bool BuildTower(Tower towerPtr)
    {
        bool valid = false;
        //TODO: check if the location is valid
        valid = true;
        if (!valid)
        {
            return false;
        }

        _towersHolder.Add(towerPtr);

        return true;
    }

    //Updates the game board to remove the tower
    public bool RemoveTower(Tower towerPtr)
    {
        //TODO: remove tower
        _towersHolder.Remove(towerPtr);
        return true;
    }

    // Updates the game board of the movement of the enemy position and notifies the towers of enemies in range
    public void UpdateEnemyPosition(Enemy enemyPtr)
    {
        int enemyX = (int)enemyPtr.GridX;
        int enemyY = (int)enemyPtr.GridY;

        //TODO: update the position of the enemy and notify the towers

        foreach (var towerPtr in _towersHolder)
        {
            //TODO: check if the enemy is in range of a tower
            int towerX = (int)towerPtr.X;
            int towerY = (int)towerPtr.Y;
            int towerRange = (int)towerPtr.AttackRange;

            if ((enemyX < towerX + towerRange && enemyX > towerX - towerRange) &&
                (enemyY < towerY + towerRange && enemyY > towerY - towerRange))
            {
                //enemy in range of the tower
                //TODO: notify the tower that the enemy is in range
            }
        }
    }

    public void AddEnemy(Enemy enemyPtr)
    {
        //TODO: addEnemy
        _enemiesHolder.Add(enemyPtr);
    }

    public void RemoveEnemy(Enemy enemyPtr)
    {
        //TODO: remove enemy
        _enemiesHolder.Remove(enemyPtr);
    }
}
