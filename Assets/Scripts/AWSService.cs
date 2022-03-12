using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AWSService : MonoBehaviour
{
    private Businesses _businesses;
    private GeoCodes _geoCodes;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetDeals());
    }


    // Update is called once per frame
    void Update()
    {

    }


    public IEnumerator GetDeals()
    {
        Debug.Log("GetDeals()");

        using (UnityWebRequest req = UnityWebRequest.Get("https://8cvvagdtgh.execute-api.us-east-2.amazonaws.com/default/GetDeals"))
        {
            req.SetRequestHeader("Content-Type", "application/json; charset=utf-8");
            yield return req.SendWebRequest();
            while (!req.isDone)
                yield return null;

            byte[] result = req.downloadHandler.data;
            string businessJSON = System.Text.Encoding.Default.GetString(result);
            _businesses = JsonUtility.FromJson<Businesses>(businessJSON);

            _geoCodes = new GeoCodes();
            foreach (var business in _businesses.deals)
            {
                _geoCodes.Locations.Add(
                    new GeoCode(
                        business.name,
                        business.lat,
                        business.lon,
                        business.alt
                    ));
            }
        }
        Debug.Log("Locations: " + _geoCodes.Locations.Count);
    }


    public GeoCodes GetGeoCodes()
    {
        return _geoCodes;
    }
}
