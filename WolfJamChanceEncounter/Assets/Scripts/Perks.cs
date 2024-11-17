using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Reward
{
    damage,
    heal,
    stamina
}
public class Perks : MonoBehaviour
{

    public Reward perkType;
    public float modifier;
    public string message;
    public BattleSystem battleSystem;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreatePerk(Reward type, string message)
    {
        perkType = type;
        this.message = message;
        switch (type)
        {
            case Reward.damage:
                modifier = Random.Range(5,11);
                break;
            case Reward.heal:
                modifier = Random.Range(5, 11);
                break;
            case Reward.stamina:
                modifier = Random.Range(5, 11);
                break;
        }

        battleSystem.allPerks.Add(this);
    }
}
