using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ChangeColor : MonoBehaviour
{
    private Material[] material_list = new Material[4];
    public GameObject obj;
    private bool beingHandled = false;
    private string URL = "https://api.openweathermap.org/data/2.5/weather";
    private string urlParameters = "appid=caa108cfd7c38543a5e12d424e76f80e";


    void Start(){
        material_list[0] = Resources.Load("SIGN_red", typeof(Material)) as Material;
        material_list[1] = Resources.Load("SIGN_orange", typeof(Material)) as Material;
        material_list[2] = Resources.Load("SIGN_yellow", typeof(Material)) as Material;
        material_list[3] = Resources.Load("SING_green", typeof(Material)) as Material;
        
        StartCoroutine(changeMaterial());
    }

    private IEnumerator changeMaterial() {
        StartCoroutine(GetRequest(10.7941034426, 106.721491814));
        obj.GetComponent<Renderer>().material = material_list[0];
        beingHandled = true;
        yield return new WaitForSeconds(5);
        beingHandled = false;
        StartCoroutine(changeMaterial());
    }
    IEnumerator GetRequest(double lat, double lon)
    {
        string urlParameters_new = "?lat=" + lat + "&lon=" + lon + "&" + urlParameters;
        string uri = URL + urlParameters_new;
        Debug.Log(uri);
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
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
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
