using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class ParseData : MonoBehaviour
{
    public string json;
    void Start()
    {
        //Calls fetchData to get data stored on a server
        StartCoroutine(fetchData());
    }

    IEnumerator fetchData()
    {
        //Sends get request, attempts to recieve a JSON file
        string url = "https://chance-encounter-web-app-b8362524d7c2.herokuapp.com/data";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        //Checks result to see if data was recieved
        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            Debug.Log("Success");
            Debug.Log(json);
            
        }
        else
        {
            Debug.LogError("Error");
        }
    }
}
