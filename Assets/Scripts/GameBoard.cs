using UnityEngine;
using System.Collections;

public class GameBoard : MonoBehaviour
{
    private Transform _boardHolder;

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

        public Tile(GameObject gameObject, uint x, uint y, uint blockSize)
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
        GameGridSystem = new GridSystem(GridWidth, GridHeight, GridEntrances, GridObstacles);
    }

    // Updates and checks if a tower can be build at grind position
    public bool BuildTower(uint  gridx, uint gridy, uint range /*, Tower towerPtr*/)
    {
        //TODO: check if the location is valid
        return true;
    }

    //Updates the game board to remove the tower
    public bool RemoveTower( /*Tower towerPtr*/)
    {
        //TODO: remove tower
        return true;
    }

    // Updates the game board of the movement of the enemy position
    public void UpdateEnemyPosition(/* Enemy enemyPtr */)
    {
        //TODO: update the position of the enemy and notify the towers
    }

    public void RemoveEnemy(/* Enemy enemyPtr */)
    {
        //TODO: remove enemy
    }
}
