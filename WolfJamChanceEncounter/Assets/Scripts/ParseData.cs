using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class ParseData : MonoBehaviour
{
    [System.Serializable]
    public class WebData
    {

        public string lonelyInput;
        public int creature;
        public string positiveInput;
        public int powerUp;
    }
    [System.Serializable]
    public class WebDataList
    {
        public List<WebData> data;   
    }

    public string json;
    public WebDataList webDataList;
    [SerializeField]
    public Perks perks;
    [SerializeField]
    public Enemy enemy;
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
            webDataList = JsonUtility.FromJson<WebDataList>(json);

            for (int i = 0; i < webDataList.data.Count; i++)
            {
                perks.CreatePerk((Reward)webDataList.data[i].powerUp - 1, webDataList.data[i].positiveInput);
                enemy.createEnemy((UnitType)webDataList.data[i].creature, webDataList.data[i].lonelyInput);
                Debug.Log((Reward)webDataList.data[i].powerUp - 1);
            }   
        }
        else
        {
            Debug.LogError("Error");
        }

    }
}
