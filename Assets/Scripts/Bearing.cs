using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bearing : MonoBehaviour
{
    private float _latA;
    private float _lonA;
    private float _latB;
    private float _lonB;


    public float Calculate(float latA, float lonA, float latB, float lonB)
    {
        _latA = latA;
        _lonA = lonA;
        _latB = latB;
        _lonB = lonB;
        return this.CalculateBearing();
    }


    // =DEGREES(
    //      ATAN2(
    //          COS(RADIANS(latA))*SIN(RADIANS(latB))-SIN(RADIANS(latA))*COS(RADIANS(latB))*COS(RADIANS(lonB-lonA)),
    //          SIN(RADIANS(lonB-lonA))*COS(RADIANS(latB))
    //      )
    //  )
    private float CalculateBearing()
    {
        float bearing = RadToDeg(Mathf.Atan2(CalculateX(), CalculateY()));
        if (bearing < 0.0f)
        {
            return bearing + 360.0f;
        }
        return bearing;
    }


    private float CalculateX()
    {
        // SIN(RADIANS(lonB-lonA))*COS(RADIANS(latB))
        float lonDelta = CalculateLonDelta();
        float x = Mathf.Cos(DegToRad(_latB)) * Mathf.Sin(lonDelta);
        return x;
    }


    private float CalculateY()
    {
        // COS(RADIANS(latA))*SIN(RADIANS(latB))-SIN(RADIANS(latA))*COS(RADIANS(latB))*COS(RADIANS(lonB-lonA))
        float lonDelta = CalculateLonDelta();
        float y = Mathf.Cos(DegToRad(_latA)) * Mathf.Sin(DegToRad(_latB)) -
            Mathf.Sin(DegToRad(_latA)) * Mathf.Cos(DegToRad(_latB)) * Mathf.Cos(lonDelta);
        return y;
    }


    private float CalculateLonDelta()
    {
        return DegToRad(_lonB - _lonA);
    }


    private float DegToRad(float degree)
    {
        return degree * Mathf.Deg2Rad;
    }


    private float RadToDeg(float radian)
    {
        return radian * Mathf.Rad2Deg;
    }
}
