using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ReadQuakes : MonoBehaviour
{
    public string url = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/4.5_day.geojson";

    void Start()
    {
        StartCoroutine(GetData(url));
    }

    IEnumerator GetData(string url)
    {
        Debug.Log($"sending request {url}");
        UnityWebRequest req = UnityWebRequest.Get(url);
        yield return req.SendWebRequest();
        if (req.isNetworkError)
            Debug.Log($"Error ({url}): {req.error}");
        else
        {
            JSONObject data = new JSONObject(req.downloadHandler.text, -2, false, false);
            for (int i = 0; i < data["metadata"]["count"].n; i++)
            {
                float lat = data["features"][i]["geometry"]["coordinates"][1].n;
                float lon = data["features"][i]["geometry"]["coordinates"][0].n;
                float mag = data["features"][i]["properties"]["mag"].n;
                Debug.Log($"lat: {lat}  lon: {lon}   mag: {mag}");
            }
        }
    }
}
