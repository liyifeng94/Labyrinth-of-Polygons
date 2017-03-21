using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;

public class GridSystem
{
    private readonly uint _width;
    private readonly uint _height;
    private readonly uint _entrances;
    private readonly uint _obstacles;

    public Grid MainGameGrid { get; private set; }

    public class Cell
    {
        public readonly int X;
        public readonly int Y;
        public bool IsBlocked { get; private set; }
        public bool IsEntrance { get; private set; }
        public bool IsExit { get; private set; }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
            IsBlocked = false;
            IsEntrance = false;
            IsExit = false;
        }

        public void ResetCell()
        {
            IsEntrance = false;
            IsExit = false;
            IsBlocked = false;
        }

        public void SetEntrance()
        {
            IsEntrance = true;
            IsExit = false;
        }

        public void SetExit()
        {
            IsExit = true;
            IsEntrance = false;
        }

        //Returns the previous blocked state
        public bool SetCell(bool blocked)
        {
            bool temp = IsBlocked;
            IsBlocked = blocked;
            return temp;
        }
    }

    public class Grid
    {
        public readonly uint Width;
        public readonly uint Height;
        private readonly Cell[ , ] _grid;
        private readonly List<Cell> _availableCells;
        public List<Cell> Entrances { get; private set; }
        public List<Cell> Exits { get; private set; }
        public List<Cell> Obstacles { get; private set; }

        public Grid(uint width, uint height)
        {
            Width = width;
            Height = height;
            _grid = new Cell[Width, Height];
            _availableCells = new List<Cell>();
            Entrances = new List<Cell>();
            Exits = new List<Cell>();
            Obstacles = new List<Cell>();


            //Create grid
            for (int y = 0; y < Height; ++y)
            {
                for (int x = 0; x < Width; ++x)
                {
                    Cell newCell = new Cell(x, y);
                    _grid[x, y] = newCell;
                    if (y > 1 && y < Height - 2)
                    {
                        _availableCells.Add(newCell);
                    }
                }
            }

            //create entrances and exits
            for (int i = 0; i < Width; ++i)
            {
                _grid[i, Height - 1].SetEntrance();
                Entrances.Add(_grid[i, Height - 1]);
                _grid[i,0].SetExit();
                Exits.Add(_grid[i, 0]);
            }
        }

        public Cell GetCellAt(int x, int y)
        {
            return _grid[x, y];
        }

        public bool SetNumberOfEntries(uint n)
        {
            if (n <= 0 || n > Width || n > Entrances.Count)
            {
                return false;
            }

            uint num = (uint)Entrances.Count - n;
            for (uint i = 0; i < num; ++i)
            {
                int index = Random.Range(0, Entrances.Count - 1);
                Entrances[index].ResetCell();
                Entrances.RemoveAt(index);
                Exits[index].ResetCell();
                Exits.RemoveAt(index);
            }

            return true;
        }

        public uint SetNumberOfObstacles(uint n)
        {
            List<Cell> availableCells = new List<Cell>(_availableCells);
            ListExt.Shuffle(availableCells);
            Queue<Cell> obstacleQueue = new Queue<Cell>(availableCells);
            Obstacles = new List<Cell>();
            uint obstacles = 0;
            Cell lastCell = null;
            while (obstacles < n)
            {
                Cell obstacleCell = obstacleQueue.Dequeue();
                obstacleCell.SetCell(true);
                bool validGrid = ValidGrid();
                if (validGrid)
                {
                    ++obstacles;
                    Obstacles.Add(obstacleCell);
                }
                else
                {
                    obstacleCell.SetCell(false);
                    obstacleQueue.Enqueue(obstacleCell);
                    if (lastCell == null)
                    {
                        lastCell = obstacleCell;
                    }
                    else if (lastCell == obstacleCell)
                    {
                        Debug.Log("Max obstacles: " + obstacles);
                        return obstacles;
                    }

                }
            }
            Debug.Log("Obstacles: " + obstacles);
            return obstacles;
        }

        public bool ValidGrid()
        {
            foreach (var entrance in Entrances)
            {
                List<GridSystem.Cell> path = GameManager.Instance.SearchPathFrom(entrance.X, entrance.Y);
                if (path.Count == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }

    public GridSystem(GameOptions gameOptions)
    {
        _width = gameOptions.Width;
        _height = gameOptions.Height;
        _entrances = gameOptions.Entrances;
        _obstacles = gameOptions.Obstacles;
        CreateGrid(gameOptions);
    }

    public void CreateGrid(GameOptions gameOptions)
    {
        MainGameGrid = new Grid(_width, _height);
        MainGameGrid.SetNumberOfEntries(_entrances);
        GameManager.Instance.CreatePathFinder(this);
        gameOptions.Obstacles = MainGameGrid.SetNumberOfObstacles(_obstacles);
    }
}
