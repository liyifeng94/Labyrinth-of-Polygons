using UnityEngine;
using System.Collections.Generic;

public class GameBoard : MonoBehaviour
{
    private Transform _boardHolder;

    private HashSet<Enemy> _enemiesHolder;

    private HashSet<Tower> _towersHolder;

    private LevelManager _levelManager;

    private List<GameObject> _highlightTile;

    private GameManager _gmInstance;

    public GridSystem GameGridSystem { get; private set; }

    public uint BlockSize = 10;
    public uint GridWidth = 10;
    public uint GridHeight = 20;
    public uint GridEntrances = 4;
    public uint GridObstacles = 0;

    public GameObject TileGound;
    public GameObject TileEntrance;
    public GameObject TileExit;
    public GameObject TileObstacles;
    public GameObject TileHighlight;

    public float TileSize { get; private set; }

    public Tile[,] BoardTiles { get; private set; } 

    public enum GamePhase
    {
        BuildingPhase,
        BattlePhase
    }

    public GamePhase CurrentGamePhase { get; private set; }

    public class Tile
    {
        public GameObject TileObject { get; private set; }

        public readonly Vector3 Position;
        public readonly uint GridX;
        public readonly uint GridY;
        public readonly float BlockSize;

        public Tile(GameObject gameObject, uint x, uint y, float blockSize, Transform parentTransform, Vector3 startPosition)
        {
            GridX = x;
            GridY = y;
            BlockSize = blockSize;
            Vector3 tilePosition = startPosition;

            tilePosition.x += (int)GridX * blockSize + blockSize / 2;
            tilePosition.y += (int)GridY * blockSize + blockSize / 2;
            tilePosition.z += 0f;

            Position = tilePosition;

            TileObject = Instantiate(gameObject, tilePosition, Quaternion.identity) as GameObject;
            if (TileObject == null)
            {
                Debug.LogAssertion("Unable to create tile at " + x + ","+ y);
                return;
            }
            TileObject.transform.SetParent(parentTransform);
            TileEventHandler eventHandler = TileObject.GetComponent<TileEventHandler>();
            if (eventHandler != null)
            {
                eventHandler.GridX = GridX;
                eventHandler.GridY = GridY;
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        TileSize = (uint)TileGound.GetComponent<SpriteRenderer>().bounds.size.x;
        _gmInstance = GameManager.Instance;
        _levelManager = _gmInstance.CurrentLevelManager;
        GameBoardSetup();
        CurrentGamePhase = GamePhase.BuildingPhase;
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
        _highlightTile = new List<GameObject>();
        CreateBoardTiles();
    }

    void CreateBoardTiles()
    {
        BoardTiles = new Tile[GridWidth, GridHeight];
        GridSystem.Grid gameGrid = GameGridSystem.MainGameGrid;
        Vector3 startPosition = new Vector3(0f, 0f, 0f);

        startPosition.x = 0 - (GridWidth/2.0f) * TileSize;
        startPosition.y = 0 - (GridHeight/2.0f) * TileSize;
        startPosition.z = 0f;

        //Create grid
        for (uint y = 0; y < GridHeight; ++y)
        {
            for (uint x = 0; x < GridWidth; ++x)
            {
                GridSystem.Cell cell = gameGrid.GetCellAt(x, y);

                if (cell.IsEntrance)
                {
                    BoardTiles[x, y] = new Tile(TileEntrance, x, y, TileSize, _boardHolder, startPosition);
                }
                else if (cell.IsExit)
                {
                    BoardTiles[x, y] = new Tile(TileExit, x, y, TileSize, _boardHolder, startPosition);
                }
                else if (cell.IsBlocked)
                {
                    BoardTiles[x, y] = new Tile(TileObstacles, x, y, TileSize, _boardHolder, startPosition);
                }
                else
                {
                    BoardTiles[x, y] = new Tile(TileGound, x, y, TileSize, _boardHolder, startPosition);
                }
            }
        }
    }

    // Updates and checks if a tower can be build at grind position
    public bool BuildTower(Tower towerPtr)
    {
        GridSystem.Cell towerCell = GameGridSystem.MainGameGrid.GetCellAt(towerPtr.X, towerPtr.Y);

        towerCell.SetCell(true);

        foreach (var entrance in GameGridSystem.MainGameGrid.Entrances)
        {
            bool valid;
            List<GridSystem.Cell> paths = _gmInstance.SearchPathFrom(entrance.X, entrance.Y);
            valid = paths.Count != 0;
            if (!valid)
            {
                towerCell.SetCell(false);
                return false;
            }
        }


        _towersHolder.Add(towerPtr);

        return true;
    }

    //Updates the game board to remove the tower
    public bool RemoveTower(Tower towerPtr)
    {
        GridSystem.Cell towerCell = GameGridSystem.MainGameGrid.GetCellAt(towerPtr.X, towerPtr.Y);
        towerCell.SetCell(false);
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
                //enemy in range of the towers
                towerPtr.AddEnemy(enemyPtr);
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

    public void EnemyReachedExit(Enemy enemyPtr)
    {
        _enemiesHolder.Remove(enemyPtr);
        _levelManager.RemoveHealth(enemyPtr.Damage);
    }

    public void HighlightTileAt(uint x, uint y)
    {
        Tile targetTile = BoardTiles[x, y];

        Vector3 tilePositon = targetTile.TileObject.gameObject.transform.position;
        GameObject newHighlight = Instantiate(TileHighlight, tilePositon, Quaternion.identity) as GameObject;

        _highlightTile.Add(newHighlight);
    }

    public void ClearHighlightTiles()
    {
        foreach (var tile in _highlightTile)
        {
            Destroy(tile);
        }
    }

    public void EnterBattlePhase()
    {
        CurrentGamePhase = GamePhase.BattlePhase;
        
    }

    public void EnterBuildingPhase()
    {
        CurrentGamePhase = GamePhase.BuildingPhase;
    }
}
