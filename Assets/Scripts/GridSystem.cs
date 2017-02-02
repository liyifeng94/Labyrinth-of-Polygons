using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridSystem
{
    private readonly uint _width;
    private readonly uint _height;
    private readonly uint _entrances;
    private readonly uint _obstacles;

    private Grid _mainGameGrid;

    public class Cell
    {
        public readonly uint X;
        public readonly uint Y;
        public bool IsBlocked { get; private set; }
        public bool IsEntrance { get; private set; }
        public bool IsExit { get; private set; }

        public Cell(uint x, uint y)
        {
            X = x;
            Y = y;
            IsBlocked = false;
            IsEntrance = false;
            IsExit = false;
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
        public List<Cell> Entrances { get; private set; }
        public List<Cell> Exits { get; private set; }
        public HashSet<Cell> Obstacles { get; private set; }

        public Grid(uint width, uint height)
        {
            Width = width;
            Height = height;
            _grid = new Cell[Width, Height];
            Entrances = new List<Cell>();
            Exits = new List<Cell>();
            Obstacles = new HashSet<Cell>();

            //Create grid
            for (uint y = 0; y < Height; ++y)
            {
                for (uint x = 0; x < Width; ++x)
                {
                    _grid[x,y] = new Cell(x,y);
                }
            }

            //create entrances and exits
            for (uint i = 0; i < Width; ++i)
            {
                _grid[0,i].SetEntrance();
                Entrances.Add(_grid[0, i]);
                _grid[Height-1, i].SetExit();
                Entrances.Add(_grid[Height - 1, i]);
            }
        }

        public Cell GetCellAt(uint x, uint y)
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
                Entrances.RemoveAt(index);
                Exits.RemoveAt(index);
            }

            return true;
        }

        public void SetNumberOfObstacles(uint n)
        {
            bool validGrid;
            do
            {
                Obstacles = new HashSet<Cell>();
                while (n < Obstacles.Count)
                {
                    int x = Random.Range(0, (int)Width - 1);
                    int y = Random.Range(0, (int)Height - 1);
                    Obstacles.Add(_grid[x, y]);
                    _grid[x, y].SetCell(true);
                }
                validGrid = ValidGrid();
            } while (validGrid == false);
        }

        public bool ValidGrid()
        {
            //TODO: Check path 
            return true;
        }
    }

    public GridSystem(uint width, uint hight, uint entrances, uint obstacles)
    {
        _width = width;
        _height = hight;
        _entrances = entrances;
        _obstacles = obstacles;
    }

    public void CreateGrid()
    {
        _mainGameGrid = new Grid(_width, _height);
        _mainGameGrid.SetNumberOfEntries(_entrances);
        _mainGameGrid.SetNumberOfObstacles(_obstacles);
    }

    // Returns true if a tower can be built here
    public bool BuildAtCell(uint x, uint y)
    {
        bool wasBlocked = _mainGameGrid.GetCellAt(x, y).SetCell(true);
        if (wasBlocked)
        {
            return false;
        }
        //TODO: check if path is valid
        //_mainGameGrid.GetCellAt(x, y).SetCell(false);
        return true;
    }

    public bool DestoryAtCell(uint x, uint y)
    {
        _mainGameGrid.GetCellAt(x, y).SetCell(false);
        return false;
    }
}
