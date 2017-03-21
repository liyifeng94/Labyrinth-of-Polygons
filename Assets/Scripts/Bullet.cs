using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Bullet : MonoBehaviour
{
    public float StartX, StartY, EndX, EndY,SpeedX,SpeedY,Length;
    public float Speed = 0.05f;
    public int Damage;
    private Tower t;
    public void MakeBullet(float SX, float SY, float EX, float EY, float dis, Tower t, int damage)
    {
        StartX = SX;
        StartY = SY;
        EndX = EX;
        EndY = EY;
        SpeedX = (float) (Math.Cos(Math.Atan(Math.Abs(EndY - StartY)/ Math.Abs(EndX - StartX)))*Speed);
        SpeedY = (float) (Math.Sin(Math.Atan(Math.Abs(EndY - StartY) / Math.Abs(EndX - StartX))) * Speed);
        if (EX - SX < 0) SpeedX *= -1;
        if (EY - SY < 0) SpeedY *= -1;
        Length = dis;
        Debug.Log("SX: " + SX + " SY: " + SY + " EX: " + EX + " EY: " + EY + " length: " + Length);
        this.t = t;
        Damage = damage;
    }

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 position = transform.position;
        position.x += SpeedX;
        position.y += SpeedY;
        transform.position = position;
        Length -= Speed;
        if (Length <= 0) End();
    }

    public void End()
    {
        t.ReceiveAttack(Damage);
        Destroy(gameObject);
    }
}
