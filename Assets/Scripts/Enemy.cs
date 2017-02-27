using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    private const float Tolerance=(float)0.01;

    public enum Direction { Up = 0, Left = 1, Down = 2, Right = 3 }

    public uint GridX { get; private set; }
    public uint GridY { get; private set; }

    public static float Tolerance1
    {
        get
        {
            return Tolerance;
        }
    }

    public int _damage;
    public float Speed;
    public uint AttackRange;
    public uint Hp;
    public Direction Dir ;
    public int Pos;
    public float distance;
    private GameBoard _gameBoard;
    private LevelManager _levelManager;
    private EnemyController _enemyController;

    private List<GameBoard.Tile> _path;
    private List<GridSystem.Cell> _cells;
    

    

    void Start()
    {
        _levelManager = GameManager.Instance.CurrentLevelManager;
        _gameBoard = _levelManager.GameBoardSystem;
        _enemyController = EnemyController.Instance;
    }

    bool InTile(GameBoard.Tile tile)
    {
        if ((Math.Abs(transform.position.x - tile.Position.x) > 0.5) ||
            (Math.Abs(transform.position.y - tile.Position.y) > 0.5)) 
            return false;
        return true;
    }

    void ChangeDir()
    {
       
        if (_path[Pos + 1].Position.x >_path[Pos].Position.x) Dir = Direction.Right;
        if (_path[Pos + 1].Position.x < _path[Pos].Position.x) Dir = Direction.Left;
        if (_path[Pos + 1].Position.y > _path[Pos].Position.y) Dir = Direction.Up;
        if (_path[Pos + 1].Position.y < _path[Pos].Position.y) Dir = Direction.Down;

    }

    void ReachTileCenter()
    {
        if (Pos==_path.Count-1) ReachEnd();
        else ChangeDir();
    }

    void ReachEnd()
    {
        _gameBoard.EnemyReachedExit(this);
        Destroy(gameObject);
    }

    //Called every frame
    void Update()
    {
        Vector3 position = transform.position;
        if (((Pos == 0 || Pos+1 == _path.Count) && Math.Abs(distance - 0.5) < Tolerance) ||
            (Math.Abs(distance - 1) < Tolerance))
        {
            Pos++;
            GridX = _cells[Pos].X;
            GridY = _cells[Pos].Y;
            _gameBoard.UpdateEnemyPosition(this);
            distance = 0;
        }
        if (Math.Abs(transform.position.x - _path[Pos].Position.x) < Tolerance &&
            Math.Abs(transform.position.y - _path[Pos].Position.y) < Tolerance)
        {
            ReachTileCenter();
        }
        distance += (float)0.01;
        if (Dir == Direction.Right) position.x += Speed;
        if (Dir == Direction.Left) position.x -= Speed;
        if (Dir == Direction.Up) position.y += Speed;
        if (Dir == Direction.Down) position.y -= Speed;

        transform.position = position;

    }


    public void SetupEnemy(uint x, uint y,List<GameBoard.Tile> path,List<GridSystem.Cell> cells, float speed)
    {
        GridX = x;
        GridY = y;
        Speed = speed;
        Hp = 1;
        _path = path;
        _cells = cells;
        Dir = Direction.Down;
        Pos = 0;
        distance = (float)0.0;
    }

    public void SetPos(uint xPos, uint yPos) {
        GridX = xPos;
        GridY = yPos;
    }

    public void Attack() { }

    public void GetDamaged(uint damage)
    {
        Hp -= damage;
        if (Hp<=0)
        {
            Die();
        }
    }

    public void Die()
    {
        _enemyController.RemoveEnemy(this);
        Destroy(gameObject);
    }

}