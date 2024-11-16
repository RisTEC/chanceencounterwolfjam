using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Unit> enemyList;

    public static EnemySpawner Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        //Add units 


        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Unit getEnemy()
    {
        int randomNum = Random.Range(0, enemyList.Count);
        Unit enemy = enemyList[randomNum];
        enemyList.RemoveAt(randomNum);
        return enemy;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
