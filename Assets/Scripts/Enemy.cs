using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{

    public enum Direction { Up = 0, Left = 1, Down = 2, Right = 3 }

    public uint GridX { get; private set; }
    public uint GridY { get; private set; }

    private uint _x;
    private uint _y;
    private uint _speed;
    private uint _attackRange;
    private uint _hp;
    private uint index;

    private List<GridSystem.Cell> _path;

    private Direction _dir = Direction.Down;

    public Enemy(uint x, uint y,List<GridSystem.Cell> path)
    {
        _x = x;
        _y = y;
        _speed = 50;
        _hp = 1;
        _path = path;
        index = 0;

    }

    public void SetPos(uint xPos, uint yPos) {
        _x = xPos;
        _y = yPos;
    }

    public uint GetX() { return _x; }

    public uint GetY() { return _y; }

    public void Move() {
        switch (_dir)
        {
            //TODO: change speed to unit/time
            case Direction.Up:
                _y -= _speed;
                break;
            case Direction.Down:
                _y += _speed;
                break;
            case Direction.Left:
                _x -= _speed;
                break;
            case Direction.Right:
                _x += _speed;
                break;
            default:
                break;
        }
        
    }

    public void Blocked() {
        Turn(Direction.Left);
    }

    public void Turn(Direction newDir)
    {
        _dir = newDir;
    }

    public void Attack() { }

    public void GetDamaged(uint damage)
    {
        _hp -= damage;
        if (_hp<=0)
        {
            Die();
        }
    }

    public void Die()
    {

    }

}