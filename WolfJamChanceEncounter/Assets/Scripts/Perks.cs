using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perks : MonoBehaviour
{
    public enum type
    {
        damage,
        heal,
        stamina
    }
    public type enemyType;
    private float modifier;
    public string message;
    private List<Perks> allPerks = new List<Perks>();
    [SerializeField]
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreatePerk(type type, string message)
    {
        enemyType = type;
        this.message = message;
        switch (type)
        {
            case type.damage:
                modifier = Random.Range(10,21);
                break;
            case type.heal:
                modifier = Random.Range(10, 21);
                break;
            case type.stamina:
                modifier = Random.Range(10, 11);
                break;
        }

        allPerks.Add(this);
    }
}
