using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridSystem : MonoBehaviour
{
    public uint Width = 10;
    public uint Height = 20;

    private Grid _mainGameGrid;

    public class Cell
    {
        private readonly uint _x;
        private readonly uint _y;
        public bool IsBlocked { get; private set; }
        public bool IsEntrance { get; private set; }
        public bool IsExit { get; private set; }

        public Cell(uint x, uint y)
        {
            _x = x;
            _y = y;
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

        public void SetCell(bool blocked)
        {
            IsBlocked = blocked;
        }
    }

    public class Grid
    {
        public uint Width { get; }
        public uint Height { get; }
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


	// Use this for initialization
	void Awake ()
	{
	    CreateGrid(4,0);
	}

    public void CreateGrid(uint entries, uint obstacles)
    {
        _mainGameGrid = new Grid(Width, Height);
        _mainGameGrid.SetNumberOfEntries(entries);
        _mainGameGrid.SetNumberOfObstacles(obstacles);
    }
}
