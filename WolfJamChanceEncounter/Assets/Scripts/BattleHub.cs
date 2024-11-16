using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHub : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text levelText;
    public TMP_Text staminaPercentage;

    public Slider hpSlider;

    public void PlayerSetHUD(Player unit)
    {
        nameText.text = unit.unitName;
        levelText.text = "Lvl: " + unit.unitLevel;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
        SetStamina(unit);
    }

    public void EnemySetHUD(Enemy unit)
    {
        nameText.text = unit.unitName;
        levelText.text = "Lvl: " + unit.unitLevel;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
    }

    public void SetStamina(Player unit)
    {
        /*float percentage = (unit.currentStamina / unit.maxStamina) *100;
        Debug.Log(percentage);*/
        staminaPercentage.text = ((unit.currentStamina / unit.maxStamina) * 100).ToString();
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;

    }


}
