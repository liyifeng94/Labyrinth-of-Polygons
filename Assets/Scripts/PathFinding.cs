using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class PathFinding
{
    
    GridSystem.Grid grid; // get through game manager
    private GridSystem.Cell[,] prev; /* for every cell at (x,y), which cell is the previous cell in the shortest path */
    private int[,] dist; /* the distance between every cell and entrance */
    private List<GridSystem.Cell> _queue = new List<GridSystem.Cell>();
    private GridSystem _gameGrid;

    Hashtable _hashpath = new Hashtable();
    Hashtable _initialpath = new Hashtable();

    public PathFinding(GridSystem mainGameGrid)
    {
        _gameGrid = mainGameGrid;
        grid = _gameGrid.MainGameGrid;

        dist = new int[grid.Width, grid.Height];

        /* initiate distance to be INFINITY */
        FillMax(dist, _queue);

        /* intiate the previous cell of every cell to be null */
        prev = new GridSystem.Cell[grid.Width, grid.Height];
        foreach (var e in grid.Entrances)
        {
            _hashpath.Add(e.X, new List<GridSystem.Cell>());
            _initialpath.Add(e.X, new List<GridSystem.Cell>());
        }
    }

    private void FillMax(int[,] arr, List<GridSystem.Cell> q)
    {
        for (int i = 0; i < grid.Width; i++)
        {
            for (int j = 0; j < grid.Height; j++)
            {
                arr[i, j] = int.MaxValue;
                q.Add(grid.GetCellAt(i, j));
            }
        }
    }

    /* get a cell with the shortest distance */
    private bool GetNextCell(ref GridSystem.Cell u)
    {
        var min = int.MaxValue;
        var ret = _queue[0];

        foreach (var t in _queue)
        {
            if (dist[t.X, t.Y] >= min) continue;
            min = dist[t.X, t.Y];
            ret = grid.GetCellAt(t.X, t.Y);
        }

        /* if every cell has a distance == INFINITE, it means there are no available paths */
        if (min == int.MaxValue)
        {
            return false;
        }

        // if (ret == null) return false;

        u = ret;
        _queue.Remove(ret);
        return true;
    }

    /* return true if the (neigh) is an exit point */
    private bool _CheckNeighbour(GridSystem.Cell neigh, GridSystem.Cell from,int alt)
    {
        // Debug.Log("pathfinding triggered");
        if (neigh.IsExit)
        {
            dist[neigh.X, neigh.Y] = alt;
            prev[neigh.X, neigh.Y] = from;
            return true;
        }

        if (!neigh.IsBlocked)
        {
            if (alt < dist[neigh.X, neigh.Y])
            {
                dist[neigh.X, neigh.Y] = alt;
                prev[neigh.X, neigh.Y] = from;
            }
        }
        return false;
    }

    public List<GridSystem.Cell> SearchFlying(int x, int y)
    {
        var path = ((List<GridSystem.Cell>)_initialpath[x]);
        if (path.Count == 0)
        {
            path = Search(x, y);
            _initialpath[x] = path;
        }
        return path;
    }

    private void RefreshCache(int x, List<GridSystem.Cell> path)
    {
        _queue = new List<GridSystem.Cell>();
        FillMax(dist, _queue);
        prev = new GridSystem.Cell[grid.Width, grid.Height];
        _hashpath[x] = path;
    }

    /* argument: (x): int. As the x-coordinate of the starting point, and
     *           (y): int. As an y-coordinate of the starting point.
     * return: List<GridSystem.Cell>: the list contains all cells on the path 
     *           from the starting point to the closest exit point; However,
     *           if there are no available path, the list should be EMPTY!    */
    public List<GridSystem.Cell> Search(int x, int y)
    {
        var path = ((List<GridSystem.Cell>) _hashpath[x]);
        if (path.Count > 0)
        {
            bool modified = false;
            foreach (var e in path)
            {
                if (e.IsBlocked)
                {
                    modified = true;
                    path = new List<GridSystem.Cell>();
                    break;
                }
            }
            if (!modified)
                return path;
        }
        
        var u = new GridSystem.Cell(0,0);
        var neigh = new GridSystem.Cell(0, 0);
        dist[x, y] = 0;

        /* keep searching until we run out all cells */
        while (_queue.Count > 0)
        {
            /* assign u with the lowest dist in queue 
               if false returned, there're no available path.
               pathfinding fail.                             */
            if (!GetNextCell(ref u))
            {
                RefreshCache(x, path);
                return path;
            }

            var alt = dist[u.X, u.Y] + 1; /* the distance is simply dist[u] + 1 */

            /* foreach neighbour of u, updates their distance from an
             entry point. Whenever the neighbour is an exit point, break.
             for we have found the shortest path.                         */
            if (u.Y != 0)
            {
                neigh = grid.GetCellAt(u.X, u.Y - 1); // the one close to the bottom of screen
                if (_CheckNeighbour(neigh, u, alt)) break;
            }

            if (u.X + 1 < grid.Width)
            {
                neigh = grid.GetCellAt(u.X + 1, u.Y); // the right one
                if (_CheckNeighbour(neigh, u, alt)) break;
            }

            if (u.X != 0)
            {
                neigh = grid.GetCellAt(u.X - 1, u.Y); // the left one
                if (_CheckNeighbour(neigh, u, alt)) break;
            }

            if (u.Y + 1 < grid.Height)
            {
                neigh = grid.GetCellAt(u.X, u.Y + 1); // the upper one
                if (_CheckNeighbour(neigh, u, alt)) break;
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
        RefreshCache(x, path);

        return path;
    }
}
