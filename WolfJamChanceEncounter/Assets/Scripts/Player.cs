using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    public int damage;

    public int maxHP;
    public int currentHP;

    public int healAmount;

    public float maxStamina;
    public float currentStamina;
    public float staminaRecovery;

    public bool guard;

    public bool Guard
    {
        get { return guard;  } set { guard = value;  }
    }

    public bool TakeDamage(int damage)
    {
        if(guard == true)
        {
            damage /= 2;
        }
        currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = 1;
            return false;
        }
        else
            return false;
    }

    public void GuardCheck()
    {
        guard = true;
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    public void RecoverStamina()
    {
        currentStamina += (maxStamina * 0.2f);
        if(currentStamina > maxStamina)
        {
            currentStamina = maxStamina;
        }

    }
}
