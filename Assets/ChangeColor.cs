using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
// using System.Serializable;

// using Newtonsoft.Json;

public class ChangeColor : MonoBehaviour
{
    private Material[] material_list = new Material[4];
    public GameObject obj;
    public double ObjLat;
    public double ObjLon;
    private bool beingHandled = false;
    private string URL = "https://api.openweathermap.org/data/2.5/weather";
    private string urlParameters = "appid=caa108cfd7c38543a5e12d424e76f80e";
    public double temp = 0;
    public double pressure = 0;
    public double humidity = 0;

    void Start(){
        material_list[0] = Resources.Load("SIGN_red", typeof(Material)) as Material;
        material_list[1] = Resources.Load("SIGN_orange", typeof(Material)) as Material;
        material_list[2] = Resources.Load("SIGN_yellow", typeof(Material)) as Material;
        material_list[3] = Resources.Load("SING_green", typeof(Material)) as Material;
        
        StartCoroutine(changeMaterial());
    }

    private IEnumerator changeMaterial() {
        StartCoroutine(GetRequest(ObjLat, ObjLon));
        if (temp >= 40 ) {
            obj.GetComponent<Renderer>().material = material_list[0];
        }
        else if (temp >= 35) {
            obj.GetComponent<Renderer>().material = material_list[1];
        }
        else if (temp >= 30) {
            obj.GetComponent<Renderer>().material = material_list[2];
        }
        else {
            obj.GetComponent<Renderer>().material = material_list[3];
        }
        beingHandled = true;
        yield return new WaitForSeconds(3600);
        beingHandled = false;
        StartCoroutine(changeMaterial());
    }
    IEnumerator GetRequest(double lat, double lon)
    {
        string urlParameters_new = "?lat=" + lat + "&lon=" + lon + "&" + urlParameters;
        string uri = URL + urlParameters_new;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text + webRequest.downloadHandler.text.GetType());
                    var objs = JsonUtility.FromJson<WeatherInfo>(webRequest.downloadHandler.text);
                    temp = objs.main.temp - 272.15; // kelvin to celcius
                    pressure = objs.main.pressure;
                    humidity = objs.main.humidity;
                    Debug.Log("Processed: " + temp + " " + pressure + " " + humidity);
                    break;
            }
        }
    }
    
    // private void callAPI(double lat, double lon) {
    //     HttpClient client = new HttpClient();
    //     client.BaseAddress = new Uri(URL);

    //     // Add an Accept header for JSON format.
    //     client.DefaultRequestHeaders.Accept.Add(
    //     new MediaTypeWithQualityHeaderValue("application/json"));

    //     urlParameters = "?lat=" + lat + "&lon=" + lon + "&" + urlParameters;

    //     Debug.Log("urlParameters");
    //     // List data response.
    //     HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
    //     if (response.IsSuccessStatusCode)
    //     {
    //         // Parse the response body.
    //         var dataObjects = response.Content.ReadAsAsync<IEnumerable<DataObject>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
    //         foreach (var d in dataObjects)
    //         {
    //             Debug.Log("{0}", d.Name);
    //         }
    //     }
    //     else
    //     {
    //         Debug.Log("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
    //     }

    //     // Make any other calls using HttpClient here.

    //     // Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
    //     client.Dispose();
    // }
    // // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class coord {
    public double lon;
    public double lat;
}
[System.Serializable]
public class main {
    public double temp;
    public double pressure;
    public double humidity;
}
[System.Serializable]
public class WeatherInfo {
    public coord coord;
    public double lon;
    public double lat;
    public main main;
    // public double speed;
    // public double all;     // cloud
}