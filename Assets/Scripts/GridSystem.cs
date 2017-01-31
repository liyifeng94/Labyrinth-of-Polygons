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
        private uint _x;
        private uint _y;
        private bool _blocked;
        private bool _isEntrance;
        private bool _isExit;
        
        public Cell(uint x, uint y)
        {
            _x = x;
            _y = y;
            _blocked = false;
            _isEntrance = false;
            _isExit = false;
        }

        public bool IsBlocked()
        {
            return _blocked;
        }

        public bool IsEntrance()
        {
            return _isEntrance;
        }

        public bool IsExit()
        {
            return _isExit;
        }
    }

    public class Grid
    {
        private uint _width;
        private uint _height;
        private Cell[ , ] _grid;
        private List<Cell> _entrances;
        private List<Cell> _exits;

        public Grid(uint width, uint height)
        {
            _width = width;
            _height = height;
            _grid = new Cell[width,height];

            for (uint y = 0; y < height; ++y)
            {
                for (uint x = 0; x < width; ++x)
                {
                    _grid[x,y] = new Cell(x,y);
                }
            }
        }

        public Cell GetCellAt(uint x, uint y)
        {
            return _grid[x, y];
        }
    }


	// Use this for initialization
	void Awake ()
    {
        _mainGameGrid = new Grid(Width,Height);
    }
}
