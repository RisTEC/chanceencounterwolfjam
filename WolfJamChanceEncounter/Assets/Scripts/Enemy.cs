using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType
{
    PLAYER,
    DPS,
    TANK,
    HEAL
}


public class Enemy : MonoBehaviour
{
    public string unitName;
    public int unitLevel;
    public string message;

    public int damage;

    public int maxHP;
    public int currentHP;

    public int healAmount;

    public UnitType type;// { get; set; }

    public SpriteRenderer sRend;
    public EnemySpawner enemySpawner;

    public bool TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            return true;
        }
        else
            return false;
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    public void createEnemy(UnitType type, string message)
    {
        this.type = type;
        this.message = message;
        enemySpawner.enemyList.Add(this);
    }
}