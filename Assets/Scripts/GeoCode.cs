using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeoCode : MonoBehaviour
{
    public string Name { get; }
    public float Latitude { get; }
    public float Longitude { get; }
    public float Altitude { get; }
    public bool IsShown { get; set; }
    public GameObject Prefab { get; set; }


    public GeoCode(string name, float latitude, float longitude, float altitude)
    {
        Name = name;
        Latitude = latitude;
        Longitude = longitude;
        Altitude = altitude;
        IsShown = false;
    }
}
