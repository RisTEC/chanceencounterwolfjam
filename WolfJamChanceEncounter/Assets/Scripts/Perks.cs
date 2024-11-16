using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perks : MonoBehaviour
{
    public enum Reward
    {
        damage,
        heal,
        stamina
    }
    public Reward perkType;
    public float modifier;
    public string message;
    public  List<Perks> allPerks = new List<Perks>();
    [SerializeField]
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreatePerk(Reward type, string message)
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

        allPerks.Add(this);
    }
}
