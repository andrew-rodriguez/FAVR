using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class YelpReview : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetReview());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    protected IEnumerator GetReview()
    {
        using (UnityWebRequest req = UnityWebRequest.Get("https://api.yelp.com/v3/businesses/0SEeR7BkTC44QkZnp_XkUg"))
        {
            req.SetRequestHeader("Content-Type", "application/json; charset=utf-8");
            req.SetRequestHeader("Authorization", "Bearer 7_F11pLmkWy_q55pdkYxE7NVlfV09fqLPO29amS1eK9kJezSAlwLruzjQ7CZxuGdRpYdIXannFhkPvJkzJEzkdxPBPF0WgZ532VmfhcXrEW_meyAxibetl6rhn98YXYx");
            yield return req.SendWebRequest();
            while (!req.isDone)
                yield return null;
            byte[] result = req.downloadHandler.data;
            string businessJSON = System.Text.Encoding.Default.GetString(result);

            Debug.Log("Business Info: \n" + businessJSON);

            Business info = JsonUtility.FromJson<Business>(businessJSON);

            Debug.Log("Business Info: \n" + info.name +
                "\n" + info.review_count +
                "\n" + info.rating);
        }
    }
}
