using UnityEngine;
using System.Collections.Generic;

public class PathFinding : MonoBehaviour {
    
    GridSystem.Grid grid; // get through game manager
    private GridSystem.Cell[,] prev; /* for every cell at (x,y), which cell is the previous cell in the shortest path */
    private int[,] dist; /* the distance between every cell and entrance */
    private List<GridSystem.Cell> entry; /* the list of entrance cells */
    private List<GridSystem.Cell> exit; /* the list of exit cells */
    private List<GridSystem.Cell> queue = new List<GridSystem.Cell>();

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public PathFinding(GridSystem.Grid MainGameGridgrid)
    {
        grid = MainGameGridgrid;
        entry = MainGameGridgrid.Entrances;
        exit = MainGameGridgrid.Exits;

        /* initiate distance to be INFINITY */
        for (uint i = 0; i < MainGameGridgrid.Width; i++)
        {
            for (uint j = 0; j < MainGameGridgrid.Height; j++)
            {
                dist[i, j] = int.MaxValue;
                if (grid.GetCellAt(i, j).IsEntrance)
                    dist[i, j] = 0;
                queue.Add(grid.GetCellAt(i, j));
            }
        }

        /* intiate the previous cell of every cell to be null */
        prev = new GridSystem.Cell[MainGameGridgrid.Width, MainGameGridgrid.Height];

    }

    /* get the cell with the shortest distance */
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
        /* if every cell has a distance == INFINITE, means no available path */
        if (min == int.MaxValue)
        {
            return false;
        }

        if (Ret != null)
        {
            u = Ret;
            queue.Remove(Ret);
        }
        return true;
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

    public List<GridSystem.Cell> Search()
    {
        GridSystem.Cell u, neigh;

        u = new GridSystem.Cell(0, 0);
        neigh = new GridSystem.Cell(0, 0);
        while (queue.Count > 0)
        {
            /* assign u with the lowest dist in queue 
               if false returned, there're no available path.
               pathfinding fail.                             */
            if ( !GetNextCell(u) )
                return null;

            int alt = dist[u.X, u.Y] + 1; /* the distance is simply dist[u] + 1 */

            /* foreach neightbour of u */
            if (u.Y + 1 <= grid.Height)
            {
                neigh = grid.GetCellAt(u.X, u.Y + 1); // the one close to the bottom of screen
                if (_CheckNeighbour(neigh, alt)) break;
            }

            if (u.X + 1 <= grid.Width)
            {
                neigh = grid.GetCellAt(u.X + 1, u.Y); // the righter one
                if (_CheckNeighbour(neigh, alt)) break;
            }

            if (u.X - 1 >= 0)
            {
                neigh = grid.GetCellAt(u.X - 1, u.Y); // the lefter one
                if (_CheckNeighbour(neigh, alt)) break;
            }

            if (u.Y - 1 >= 0)
            {
                neigh = grid.GetCellAt(u.X, u.Y - 1); // the upper one
                if (_CheckNeighbour(neigh, alt)) break;
            }
        }

        List<GridSystem.Cell> path = new List<GridSystem.Cell>();
        path.Add(neigh);
        while (prev[neigh.X, neigh.Y] != null)
        {
            neigh = prev[neigh.X, neigh.Y];
            path.Add(neigh);
        }
        path.Reverse();

        return path;
    }
}
