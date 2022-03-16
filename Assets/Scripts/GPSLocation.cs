using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSLocation : MonoBehaviour
{
    private float _latitude;
    private float _longitude;
    private float _altitude;
    private float _accuracy;
    private double _timestamp;
    private float _trueHeading;

    private ProximityManager _proximityManager;
    private Bearing _bearing;
    private AWSService _awsService;
    private DisplayConsole _displayConsole;


    private IEnumerator StartLocationService()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield break;

        // Start service before querying location
        Input.location.Start(5.0f, 5.0f);

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            Debug.Log("ConnectLocationService() Timed out!!!");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location");
            yield break;
        }
    }


    private IEnumerator StartCompassService()
    {
        Input.compass.enabled = true;
        yield return new WaitForSeconds(1.0f);

        if (!Input.compass.enabled)
        {
            Debug.Log("Unable to start compass");
            yield break;
        }
    }


    private void StopLocationService()
    {
        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }


    private void GetLocation()
    {
        _altitude = Input.location.lastData.altitude;
        _longitude = Input.location.lastData.longitude;
        _latitude = Input.location.lastData.latitude;
        _accuracy = Input.location.lastData.horizontalAccuracy;
        _timestamp = Input.location.lastData.timestamp;
        _trueHeading = Input.compass.trueHeading;
    }


    private void DisplayLocation()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            string message = "Latitude: " + _latitude +
                "\nLongitude: " + _longitude +
                "\nAltitude: " + _altitude +
                "\nHorizontal Accuracy: " + _accuracy +
                "\nTrue Heading: " + _trueHeading +
                "\n" +
                "\nTimestamp: " + _timestamp +
                "\nDelta Time: " + Time.deltaTime;

            if (_displayConsole != null)
            {
                _displayConsole.DisplayText(message);
            }
        }
        else
        {
            Debug.Log("\nLocation Status: " + Input.location.status);
        }
    }


    private float Distance(float latitude, float longitude)
    {
        float radius = 6378.137f; // Radius of earth in KM
        float lat = _latitude * Mathf.PI / 180 - latitude * Mathf.PI / 180;
        float lon = _longitude * Mathf.PI / 180 - longitude * Mathf.PI / 180;
        float a = Mathf.Sin(lat / 2) * Mathf.Sin(lat / 2) +
            Mathf.Cos(latitude * Mathf.PI / 180) * Mathf.Cos(_latitude * Mathf.PI / 180) *
            Mathf.Sin(lon / 2) * Mathf.Sin(lon / 2);
        var c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        var d = radius * c;
        return d * 1000; // meters
    }


    private bool OnTarget(float bearing, float limit)
    {
        float leftLimit = LeftLimit(bearing, limit);
        float rightLimit = RightLimit(bearing, limit);

        if (limit == 120.0f)
        {
            Debug.Log("leftLimit: " + leftLimit);
            Debug.Log("bearing: " + bearing);
            Debug.Log("rightLimit: " + rightLimit);
        }
        if (leftLimit > rightLimit)
        {
            // _trueHeading between leftLimit and 360-degrees
            // or
            // _trueHeading between 0-degress and rightLimt
            if ((_trueHeading > leftLimit && _trueHeading <= 360.0f)
                || (_trueHeading >= 0.0f && _trueHeading < rightLimit))
            {
                return true;
            }
        }
        else if (_trueHeading > leftLimit && _trueHeading < rightLimit)
        {
            return true;
        }

        return false;
    }


    private float LeftLimit(float bearing, float limit)
    {
        float leftLimit = bearing - limit;
        if (leftLimit < 0.0f)
        {
            leftLimit = 360.0f + leftLimit;
        }
        return leftLimit;
    }


    private float RightLimit(float bearing, float limit)
    {
        float rightLimit = bearing + limit;
        if (rightLimit > 360.0f)
        {
            rightLimit = rightLimit - 360.0f;
        }
        return rightLimit;
    }


    private void Awake()
    {
        _displayConsole = GetComponent<DisplayConsole>();
        _proximityManager = GetComponent<ProximityManager>();
        _awsService = GetComponent<AWSService>();
        _bearing = new Bearing();
    }


    private void Start()
    {
        if (Input.location.isEnabledByUser)
        {
            StartCoroutine(StartLocationService());
            StartCoroutine(StartCompassService());
            GetLocation();
            DisplayLocation();
        }
    }

    
    private void Update()
    {
        //if (_timestamp != Input.location.lastData.timestamp)
        //{
        GetLocation();
        DisplayLocation();
        if (_trueHeading != 0.0f && _altitude != 0.0f)
        {
            foreach (var geoCode in _awsService.GetGeoCodes().Locations)
            {
                float distance = Distance(geoCode.Latitude, geoCode.Longitude);
                float bearing = _bearing.Calculate(_latitude, _longitude,
                        geoCode.Latitude, geoCode.Longitude);

                if (!geoCode.IsShown && distance < 9000.0f)
                {
                    _displayConsole.AppendDisplayText("\n" +
                        geoCode.Name + " is " + distance + " meters away" +
                        "\nBearing " + bearing);

                    if (OnTarget(bearing, 1.0f))
                    {
                        switch (geoCode.Type)
                        {
                            case "Merchant":
                                geoCode.IsShown = true;
                                GameObject dealPrefab = _proximityManager.SpawnDeal(geoCode.Name);
                                if (dealPrefab != null)
                                {
                                    geoCode.Prefab = dealPrefab;
                                }
                                break;

                            case "Placenote":
                                geoCode.IsShown = true;
                                GameObject notePrefab = _proximityManager.SpawnPlacenote(geoCode.Name);
                                if (notePrefab != null)
                                {
                                    geoCode.Prefab = notePrefab;
                                }
                                break;
                        }

                    }
                }
                if (geoCode.IsShown && !OnTarget(bearing, 120.0f))
                {
                    _proximityManager.DestroyDeal(geoCode.Prefab);
                    geoCode.IsShown = false;
                }
            }
        }
        //}
    }
}