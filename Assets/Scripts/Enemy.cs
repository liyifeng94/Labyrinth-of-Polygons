using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Enemy : MonoBehaviour
{
    private const float Tolerance=(float)0.01;

    public enum Direction { Up = 0, Left = 1, Down = 2, Right = 3 }
    public enum Type { Normal = 0, Fast = 1, Flying = 2, Attacking = 3, Boss = 4}

    public uint GridX { get; private set; }
    public uint GridY { get; private set; }

    public static float Tolerance1
    {
        get
        {
            return Tolerance;
        }
    }
    
    public float Speed;
    public uint AttackRange;
    public int Damage = 1;
    public int Hp = 1;
    public int Gold = 5;
    public int Score = 10;
    private Direction Dir = Direction.Down ;
    public Type EnemyType;
    private int _pos = 0;
    private float _distance = (float)0.0;
    private GameBoard _gameBoard;
    private LevelManager _levelManager;
    private EnemyController _enemyController;
    private HashSet<Tower> _towers;

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
        if (_path[_pos + 1].Position.x >_path[_pos].Position.x) Dir = Direction.Right;
        if (_path[_pos + 1].Position.x < _path[_pos].Position.x) Dir = Direction.Left;
        if (_path[_pos + 1].Position.y > _path[_pos].Position.y) Dir = Direction.Up;
        if (_path[_pos + 1].Position.y < _path[_pos].Position.y) Dir = Direction.Down;

    }

    void ReachTileCenter()
    {
        if (_pos==_path.Count-1) ReachEnd();
        else ChangeDir();
    }

    void ReachEnd()
    {
        _gameBoard.EnemyReachedExit(this);
        Die();
    }

    //Called every frame
    void Update()
    {
        Attack();
        Vector3 position = transform.position;
        if (((_pos == 0 || _pos+1 == _path.Count) && _distance>0.5) ||
            _distance>1)
        {
            _pos++;
            GridX = _cells[_pos].X;
            GridY = _cells[_pos].Y;
            _gameBoard.UpdateEnemyPosition(this);
            _distance = 0;
        }
        if ((transform.position.x >_path[_pos].Position.x && Dir==Direction.Right) ||
            (transform.position.x < _path[_pos].Position.x && Dir == Direction.Left) ||
            (transform.position.y > _path[_pos].Position.y && Dir == Direction.Up) ||
            (transform.position.y < _path[_pos].Position.y && Dir == Direction.Down))
        {
            ReachTileCenter();
        }
        float temp = Speed * Time.deltaTime;
        _distance += temp;
        if (Dir == Direction.Right) position.x += temp;
        if (Dir == Direction.Left) position.x -= temp;
        if (Dir == Direction.Up) position.y += temp;
        if (Dir == Direction.Down) position.y -= temp;

        transform.position = position;

    }


    public void SetupEnemy(uint x, uint y,List<GameBoard.Tile> path,List<GridSystem.Cell> cells, Type type)
    {
        EnemyType = type;
        GridX = x;
        GridY = y;
        _path = path;
        _cells = cells;
        _towers = new HashSet<Tower>();
        switch (EnemyType)
        {
            case Type.Normal:
                Hp = 5;
                AttackRange = 0;
                Speed = 2;
                Score = 10;
                break;
            case Type.Attacking:
                Hp = 500;
                AttackRange = 20;
                Speed = 2;
                Score = 30;
                break;
            case Type.Fast:
                Hp = 3;
                AttackRange = 0;
                Speed = 4;
                Score = 20;
                break;
            case Type.Flying:
                Hp = 5;
                AttackRange = 0;
                Speed = 2;
                Score = 40;
                break;
            case Type.Boss:
                Hp = 50;
                AttackRange = 0;
                Speed = 1;
                Score = 100;
                break;
        }
    }

    public void SetPos(uint xPos, uint yPos) {
        GridX = xPos;
        GridY = yPos;
    }

    public void AddTower(Tower t)
    {
        _towers.Add(t);
    }

    public void Attack()
    {
        if (EnemyType != Type.Attacking) return;

        //if (_towers.Count>0) _towers.First().ReceiveAttack(1000);

    }

    public void GetDamaged(int damage)
    {
        Hp -= damage;
        if (Hp<=0)
        {
            GameManager.Instance.CurrentLevelManager.AddGold(Gold);
            GameManager.Instance.CurrentLevelManager.AddScore(Score);
            Die();
        }
    }   

    public void Die()
    {
        _enemyController.RemoveEnemy(this);
        Destroy(gameObject);
    }

    public void SlowDown(float p)
    {
        
    }

}