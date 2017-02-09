using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinding : MonoBehaviour {

    private Cell[,] _grid;
    private Cell[,] _prev { get; private set; } /* for every cell at (x,y), which cell is the previous cell in the shortest path */
    private int[,] dist { get; private set; } /* the distance between every cell and entrance */
    private List<Cell> _entry; /* the list of entrance cells */
    private List<Cell> _exit; /* the list of exit cells */
    private List<Cell> queue = new List<Cell>();

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public PathFinding(Cell[,] grid, List<Cell> Entrance, List<Cell> Exits)
    {
        _grid = grid;
        _entry = Entrance;
        _exit = Exits;

        /* initiate distance to be INFINITY */
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                dist[i, j] = int.MaxValue;
            }
            queue.Add(grid[i, j]);
        }

        /* intiate the previous cell of every cell to be null */
        _prev = new Cell[Width, Height] { null };
    }

    /* get the cell with the shortest distance */
    private Cell GetNextCell()
    {
        int min = int.MaxValue;
        Cell Ret;

        foreach (int j in queue)
        {
            if (dist[j.x, j.y] <= min)
            {
                min = dist[j.x, j.y];
                Ret = _grid.get(j.x, j.y);
            }
        }
        queue.Remove(Ret);
        return Ret;
    }

    public Cell find()
    {
        Cell u;
        while (queue.Count > 0)
        {
            u = GetNextCell(); /* assign u with the lowest dist in queue */
            int alt = dist[u] + 1; /* the distance is simply dist[u] + 1 */

            /* foreach neightbour of u */
            Cell neigh = _grid.get();
                // if (_grid.get() ) { }
            }
    }
}
