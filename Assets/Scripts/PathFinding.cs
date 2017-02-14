using UnityEngine;
using System.Collections.Generic;

public class PathFinding : MonoBehaviour {
    
    GridSystem.Grid grid; // get through game manager
    private GridSystem.Cell[,] prev; /* for every cell at (x,y), which cell is the previous cell in the shortest path */
    private int[,] dist; /* the distance between every cell and entrance */
    //private List<GridSystem.Cell> entry; /* the list of entrance cells */
    //private List<GridSystem.Cell> exit; /* the list of exit cells */
    private List<GridSystem.Cell> queue = new List<GridSystem.Cell>();
    private GameManager _gameManager;
    private GridSystem _gameGrid;

    // Use this for initialization
    void Start () {
        _gameManager = GameManager.Instance;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void initalize(GridSystem gameGrid)
    {
        _gameGrid = gameGrid;
    }

    public PathFinding(GridSystem.Grid MainGameGridgrid)
    {
        grid = MainGameGridgrid;
        //entry = MainGameGridgrid.Entrances;
        //exit = MainGameGridgrid.Exits;

        dist = new int[MainGameGridgrid.Width, MainGameGridgrid.Height];

        /* initiate distance to be INFINITY */
        for (uint i = 0; i < MainGameGridgrid.Width; i++)
        {
            for (uint j = 0; j < MainGameGridgrid.Height; j++)
            {
                dist[i, j] = int.MaxValue;
                // if (grid.GetCellAt(i, j).IsEntrance)
                //    dist[i, j] = 0;
                queue.Add(grid.GetCellAt(i, j));
            }
        }

        /* intiate the previous cell of every cell to be null */
        prev = new GridSystem.Cell[MainGameGridgrid.Width, MainGameGridgrid.Height];
    }

    /* get a cell with the shortest distance */
    private bool GetNextCell(GridSystem.Cell u)
    {
        int min = int.MaxValue;
        GridSystem.Cell Ret = queue[0];

        for (int cell = 0; cell < queue.Count; cell++)
        {
            if (dist[queue[cell].X, queue[cell].Y] < min)
            {
                min = dist[queue[cell].X, queue[cell].Y];
                Ret = grid.GetCellAt(queue[cell].X, queue[cell].Y);
            }
        }

        /* if every cell has a distance == INFINITE, it means there are no available paths */
        if (min == int.MaxValue)
        {
            return false;
        }

        if (Ret != null)
        {
            u = Ret;
            queue.Remove(Ret);
            return true;
        }
        return false;
    }

    private bool _CheckNeighbour(GridSystem.Cell neigh, int alt)
    {
        if (neigh.IsExit)
        {
            dist[neigh.X, neigh.Y] = alt;
            prev[neigh.X, neigh.Y] = neigh;
            return true;
        }

        if (!neigh.IsBlocked)
        {
            if (alt < dist[neigh.X, neigh.Y])
            {
                dist[neigh.X, neigh.Y] = alt;
                prev[neigh.X, neigh.Y] = neigh;
            }
        }
        return false;
    }

    /* argument: (start): GridSystem.Cell. As the starting point, and
     *           (path): List<GridSystem.Cell>. As an empty list of cells.
     * return: true: if successfully found a path from (start) to the closest exit point,
     *              and fill the (path) will all cells on the path in order. Or
     *         false: if there are no available path.                                    */
    public bool Search(GridSystem.Cell start, List<GridSystem.Cell> path)
    {
        GridSystem.Cell u, neigh;
        u = new GridSystem.Cell(0,0);
        neigh = new GridSystem.Cell(0, 0);

        dist[start.X, start.Y] = 0;

        /* keep searching until we run out all cells */
        while (queue.Count > 0)
        {
            /* assign u with the lowest dist in queue 
               if false returned, there're no available path.
               pathfinding fail.                             */
            if ( !GetNextCell(u) )
                return false;

            int alt = dist[u.X, u.Y] + 1; /* the distance is simply dist[u] + 1 */

            /* foreach neighbour of u, updates their distance from an
             entry point. Whenever the neighbour is an exit point, break.
             for we have found the shortest path.                         */
            if (u.Y + 1 <= grid.Height)
            {
                neigh = grid.GetCellAt(u.X, u.Y + 1); // the one close to the bottom of screen
                if (_CheckNeighbour(neigh, alt)) break;
            }

            if (u.X + 1 <= grid.Width)
            {
                neigh = grid.GetCellAt(u.X + 1, u.Y); // the right one
                if (_CheckNeighbour(neigh, alt)) break;
            }

            if (u.X - 1 >= 0)
            {
                neigh = grid.GetCellAt(u.X - 1, u.Y); // the left one
                if (_CheckNeighbour(neigh, alt)) break;
            }

            if (u.Y - 1 >= 0)
            {
                neigh = grid.GetCellAt(u.X, u.Y - 1); // the upper one
                if (_CheckNeighbour(neigh, alt)) break;
            }
        }

        // List<GridSystem.Cell> path = new List<GridSystem.Cell>();

        /* if the program hits here, the neigh now should be one of exit points.
         * push it to the path, and tracking backwards to the start points       */
        path.Add(neigh);
        while (prev[neigh.X, neigh.Y] != null)
        {
            neigh = prev[neigh.X, neigh.Y];
            path.Add(neigh);
        }
        path.Reverse();

        return true;
    }
}
