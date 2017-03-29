using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Enemy : MonoBehaviour
{
    private const float Tolerance = (float)0.01;

    public enum Direction { Up = 0, Left = 1, Down = 2, Right = 3 }
    public enum Type { Normal = 0, Fast = 1, Flying = 2, Attacking = 3, BossAttack = 4, BossFly = 5, BossTank = 6}

    public int GridX { get; private set; }
    public int GridY { get; private set; }

    public static float Tolerance1
    {
        get
        {
            return Tolerance;
        }
    }

    public float Speed, OriginalSpeed;
    public int AttackRange;
    public int Damage = 2;
    public int Hp = 1;
    public int Gold = 5;
    public int Score = 10;
    public int AttackDamage = 0;
    private Direction Dir = Direction.Down;
    public Type EnemyType;
    public List<GameObject> Bullets;
    private int _pos = 0;
    private float _distance = (float)0.0;
    private GameBoard _gameBoard;
    private LevelManager _levelManager;
    private EnemyController _enemyController;
    private HashSet<Tower> _towers;
    private float _attackSpeed;
    private bool _isSlowed;
    private float _start, _end, _slowstart, _slowend;
    private List<GameBoard.Tile> _path;
    private List<GridSystem.Cell> _cells;


    void Start()
    {
        _levelManager = GameManager.Instance.CurrentLevelManager;
        _gameBoard = _levelManager.GameBoardSystem;
        _enemyController = EnemyController.Instance;
        _start = Time.time;
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
        Direction original = Dir;
        if (_path[_pos + 1].Position.x > _path[_pos].Position.x)
        {
            if (Dir == Direction.Down) transform.Rotate(Vector3.forward * 90);
            if (Dir == Direction.Up) transform.Rotate(Vector3.forward * -90);
            Dir = Direction.Right;
        }
        if (_path[_pos + 1].Position.x < _path[_pos].Position.x)
        {
            if (Dir == Direction.Down) transform.Rotate(Vector3.forward * -90);
            if (Dir == Direction.Up) transform.Rotate(Vector3.forward * 90);
            Dir = Direction.Left;
        }
        if (_path[_pos + 1].Position.y > _path[_pos].Position.y)
        {
            if (Dir == Direction.Left) transform.Rotate(Vector3.forward * -90);
            if (Dir == Direction.Right) transform.Rotate(Vector3.forward * 90);
            Dir = Direction.Up;
        }
        if (_path[_pos + 1].Position.y < _path[_pos].Position.y)
        {
            if (Dir == Direction.Left) transform.Rotate(Vector3.forward * 90);
            if (Dir == Direction.Right) transform.Rotate(Vector3.forward * -90);
            Dir = Direction.Down;
        }
        if (original != Dir && _pos>0)
        {
            Vector3 position = transform.position;
            //Debug.Log("enemy: " + position.x + " " + position.y + ", tile: " + _path[_pos].Position.x + " " + _path[_pos].Position.y);
            if (GridX != _cells[_pos].X || GridY != _cells[_pos].Y) Debug.Log("Grid enemy:" + GridX + " " + GridY + " cell: " + _cells[_pos].X + " " + _cells[_pos].Y);
            
            position.x = _path[_pos].Position.x;
            position.y = _path[_pos].Position.y;


   
            transform.position = position;

        }
    }

    void ReachTileCenter()
    {
        if (_pos == _path.Count - 1) ReachEnd();
        else ChangeDir();
    }

    void ReachEnd()
    {
        _gameBoard.EnemyReachedExit(this);
        _enemyController.RemoveEnemy(this);
        Destroy(gameObject);
    }

    //Called every frame
    void Update()
    {
        StopSlowing();
        Attack();

        EnemyMove();
    }

    private void EnemyMove()
    {
        Vector3 position = transform.position;
        float temp = Speed * Time.deltaTime;

        _distance += temp;
        if (Dir == Direction.Right) position.x += temp;
        if (Dir == Direction.Left) position.x -= temp;
        if (Dir == Direction.Up) position.y += temp;
        if (Dir == Direction.Down) position.y -= temp;

        transform.position = position;
        if ((((_pos == 0 || _pos + 1 == _path.Count) && _distance > 0.5) ||
            _distance >= 1) && _pos<_cells.Count-1)
        {
            _pos++;
            GridX = _cells[_pos].X;
            GridY = _cells[_pos].Y;
            _gameBoard.UpdateEnemyPosition(this);
            if (_distance >= 1) _distance = _distance - 1;
            else _distance -= 0.5f;
        }
        if ((transform.position.x > _path[_pos].Position.x && Dir == Direction.Right) ||
            (transform.position.x < _path[_pos].Position.x && Dir == Direction.Left) ||
            (transform.position.y > _path[_pos].Position.y && Dir == Direction.Up) ||
            (transform.position.y < _path[_pos].Position.y && Dir == Direction.Down))
        {
            ReachTileCenter();

        }
    }


    public void SetupEnemy(int x, int y, List<GameBoard.Tile> path, List<GridSystem.Cell> cells, Type type)
    {
        int currentLevel = GameManager.Instance.CurrentLevelManager.GetCurrentLevel();
        EnemyType = type;
        GridX = x;
        GridY = y;
        _path = path;
        _cells = cells;
        _isSlowed = false;
        _towers = new HashSet<Tower>();
        _enemyController = EnemyController.Instance;
        int hpGrowth = _enemyController.GetHpGrowth(EnemyType);
        float goldFactor = _enemyController.GetGoldGrowth(EnemyType);
        float attackFactor = _enemyController.GetAttackGrowth(EnemyType);
        switch (EnemyType)
        {
            case Type.Normal:
                Hp = 10 + hpGrowth;
                AttackRange = 0;
                Speed = 2;
                Score = 10;
                Gold = (int)(20 * goldFactor);
                Damage = 2;
                break;
            case Type.Attacking:
                Hp = 10 + hpGrowth;
                AttackRange = 2;
                Speed = 2;
                Score = 30;
                _attackSpeed = 1.0f;
                AttackDamage = (int)(10 * attackFactor);
                Gold = (int)(30 * goldFactor);
                Damage = 2;
                break;
            case Type.Fast:
                Hp = 8 + hpGrowth;
                AttackRange = 0;
                Speed = 3;
                Score = 20;
                Gold = (int)(20 * goldFactor);
                Damage = 2;
                break;
            case Type.Flying:
                Hp = 15 + hpGrowth;
                AttackRange = 0;
                Speed = 2;
                Score = 40;
                Gold = (int)(25 * goldFactor);
                Damage = 2;
                break;
            case Type.BossAttack:
                Hp = 80 + hpGrowth;
                AttackRange = 4;
                Speed = 2;
                Score = 100;
                _attackSpeed = 0.5f;
                AttackDamage = 10 + 3 * currentLevel;
                AttackDamage = (int)(20 * attackFactor);
                Gold = (int)(500 * goldFactor);
                Damage = 10;
                break;
            case Type.BossFly:
                Hp = 50 + hpGrowth;
                AttackRange = 2;
                Speed = 3;
                Score = 100;
                Damage = 10;
                _attackSpeed = 1f;
                AttackDamage = (int) (10 * attackFactor);
                Gold = (int)(500 * goldFactor);
                break;
            case Type.BossTank:
                Hp = 100 + hpGrowth;
                AttackRange = 3;
                Speed = 1;
                Score = 100;
                Damage = 10;
                _attackSpeed = 1f;
                AttackDamage = (int)(10 * attackFactor);
                Gold = (int)(500 * goldFactor);
                break;
        }
    }

    public void SetPos(int xPos, int yPos)
    {
        GridX = xPos;
        GridY = yPos;
    }

    public void AddTower(Tower t)
    {
        _towers.Add(t);
    }

    public void Attack()
    {
        _end = Time.time;
        if (AttackDamage <= 0 || _end - _start < _attackSpeed) return;

        Tower tower = GetAttackTower();
        if (tower == null)
        {
            _towers.Clear();
            return;
        }
        GameObject bulletObj = Instantiate(Bullets[0], transform.position, Quaternion.identity) as GameObject;
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        bullet.MakeBullet(transform.position.x,transform.position.y,tower.transform.position.x,tower.transform.position.y,Vector3.Distance(transform.position,tower.transform.position), tower, AttackDamage);

        _towers.Clear();
        _start = Time.time;

    }

    public Tower GetAttackTower()
    {
        int i = 10;
        Tower ret = null;
        foreach (var tower in _towers)
        {
            int type = _enemyController.GetTowerPriority(tower.Type);
            if (!tower.IsDestory() && i>type && type >-1)
            {
                ret = tower;
                i = type;
            }
        }
        return ret;
    }

    public void GetDamaged(int damage)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            _levelManager.AddGold(Gold);
            _levelManager.AddScore(Score);
            Die();
        }
    }

    public void Die()
    {
        _gameBoard.RemoveEnemy(this);
        _enemyController.RemoveEnemy(this);
        Destroy(gameObject);
    }

    public void SlowDown(float p)
    {
        if (!_isSlowed) OriginalSpeed = Speed;
        _isSlowed = true;
        Speed *= (1-p);
        _slowstart = Time.time;
    }

    public void StopSlowing()
    {
        _slowend = Time.time;
        if (_isSlowed && _slowend - _slowstart > 3)
        {
            Speed = OriginalSpeed;
        }
    }
}