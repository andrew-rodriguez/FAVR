using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeoCodes : MonoBehaviour
{
    public List<GeoCode> Locations { get; }

    private List<GeoCode> _geoCodes;


    public GeoCodes()
    {
        _geoCodes = new List<GeoCode>();

        // LV       40.740138, -74.364801
        // Chanel   40.740096, -74.364751   
        // Burberry 40.740010, -74.364906
        // Balenciaga   40.740015, -74.364768

        //_geoCodes.Add(new GeoCode("Louis Vuitton", 40.740138f, -74.364801f, 0.0f));
        //_geoCodes.Add(new GeoCode("Chanel", 40.740096f, -74.364751f, 0.0f));
        //_geoCodes.Add(new GeoCode("Burberry", 40.740010f, -74.364906f, 0.0f));
        //_geoCodes.Add(new GeoCode("Balenciaga", 40.740015f, -74.364768f, 0.0f));
        //_geoCodes.Add(new GeoCode("AAAA", 40.8433f, -73.99878f, 0.0f));
        //_geoCodes.Add(new GeoCode("Home", 40.80895f, -73.99058f, 21.60954f));
        //_geoCodes.Add(new GeoCode("Park", 40.80917f, -73.99017f, 5.973346f));
        //_geoCodes.Add(new GeoCode("AT&T", 40.804814f, -73.991970f, 5.0f));
        //_geoCodes.Add(new GeoCode("Panera", 40.8071f, -73.98944f, 6.125429f));
        //_geoCodes.Add(new GeoCode("Starbucks", 40.813103f, -73.984724f, 5.0f));

        Locations = _geoCodes;
    }
}

//[
//    { name: 'Louis Vuitton', lat: 40.740138, lon: -74.364801, alt: 0.0 },
//    { name: 'Chanel', lat: 40.740096, lon: -74.364751, alt: 0.0 },
//    { name: 'Burberry', lat: 40.740010, lon: -74.364906, alt: 0.0 },
//    { name: 'Balenciaga', lat: 40.740015, lon: -74.364768, alt: 0.0 },
//    { name: 'AAAA', lat: 40.8433, lon: -73.99878, alt: 0.0 },
//    { name: 'Home', lat: 40.80895, lon: -73.99058f, alt: 0.0 },
//    { name: 'Park', lat: 40.80917, lon: -73.99017, alt: 0.0 },
//    { name: 'AT&T', lat: 40.804814, lon: -73.991970, alt: 0.0 },
//    { name: 'Panera', lat: 40.8071, lon: -73.98944, alt: 0.0 },
//    { name: 'Starbucks', lat: 40.813103, lon: -73.984724, alt: 0.0 }
//]