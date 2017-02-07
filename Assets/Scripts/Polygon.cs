using UnityEngine;
using System.Collections;

public class Polygon : MonoBehaviour
{

    public enum Direction { UP = 0, LEFT = 1, DOWN = 2, RIGHT = 3 }

    private uint x;
    private uint y = 0;
    private uint speed;
    private uint attackRange;

    private Direction dir = Direction.DOWN;

    public void setPos(uint xPos, uint yPos) {
        x = xPos;
        y = yPos;
    }

    public uint getX() { return x; }

    public uint getY() { return y; }

    public void move() {
        switch (dir)
        {
            //TODO: change speed to unit/time
            case Direction.UP:
                y -= speed;
                break;
            case Direction.DOWN:
                y += speed;
                break;
            case Direction.LEFT:
                x -= speed;
                break;
            default:
                x += speed;
                break;
        }

    }

    public void blocked() {
        turn(Direction.LEFT);
    }

    public void turn(Direction newDir)
    {
        dir = newDir;
    }

    public void attack() { }



}