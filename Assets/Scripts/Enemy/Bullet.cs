using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Bullet : MonoBehaviour
{
    public float StartX, StartY, EndX, EndY,SpeedX,SpeedY,Length;
    public float Speed = 7;
    public int Damage;
    private Tower t;
    public void MakeBullet(float SX, float SY, float EX, float EY, float dis, Tower t, int damage)
    {
        StartX = SX;
        StartY = SY;
        EndX = EX;
        EndY = EY;
        //SpeedX = (float) (Math.Cos(Math.Atan(Math.Abs(EndY - StartY)/ Math.Abs(EndX - StartX)))*(Speed*Time.deltaTime));
        //SpeedY = (float) (Math.Sin(Math.Atan(Math.Abs(EndY - StartY) / Math.Abs(EndX - StartX))) * (Speed * Time.deltaTime));
        if (EX - SX < 0) SpeedX *= -1;
        if (EY - SY < 0) SpeedY *= -1;
        Length = dis;;
        this.t = t;
        Damage = damage;
    }

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 position = transform.position;
        float t = Time.deltaTime;
        SpeedX = (float)(Math.Cos(Math.Atan(Math.Abs(EndY - StartY) / Math.Abs(EndX - StartX))) * 7) *t;
        SpeedY = (float)(Math.Sin(Math.Atan(Math.Abs(EndY - StartY) / Math.Abs(EndX - StartX))) * 7) *t;
        if (EndX - StartX < 0) SpeedX *= -1;
        if (EndY - StartY < 0) SpeedY *= -1;
        position.x += SpeedX;
        position.y += SpeedY;
        float dis = (float) Math.Sqrt(SpeedX * SpeedX + SpeedY * SpeedY);
        transform.position = position;
        Length -= dis;
        if (Length <= 0) End();
    }

    public void End()
    {
        t.ReceiveAttack(Damage);
        Destroy(gameObject);
    }
}
