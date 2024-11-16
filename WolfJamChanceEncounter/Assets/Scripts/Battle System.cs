using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public enum BattleState {
    START,
    PLAYERTURN,
    ENEMYTURN,
    WON,
    LOST,
    NEXTENEMY
}
public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;


    public BattleState state;

    public TMP_Text dialogText;

    public BattleHub playerHud;
    public BattleHub enemyHud;


    Unit playerUnit;
    Unit enemyUnit;
    
    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetUpBattle());
       
    }

    IEnumerator SetUpBattle()
    {
        GameObject playerGo = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGo.GetComponent<Unit>();

        GameObject enemyGo = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGo.GetComponent<Unit>();

        dialogText.text = enemyUnit.unitName + " approaches...";

        playerHud.SetHUD(playerUnit);
        enemyHud.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        //Damage the enemy 
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHud.SetHP(enemyUnit.currentHP);
        dialogText.text = "The attack hits!";


        //Check if enemy is dead 
        if (isDead)
        {
            //End the battle
            /*state = BattleState.WON;
            EndBattle(); */

            //Get next Enemy 
            state = BattleState.NEXTENEMY;
            StartCoroutine(SetNextEnemy());
            
        }
        else
        {
            //Enemy Turn 
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());

        }
        yield return new WaitForSeconds(2f);

        //change state based on what happened 


    }

    IEnumerator SetNextEnemy()
    {
        Unit nextEnemy = EnemySpawner.Instance.getEnemy();
        nextEnemy.unitLevel = enemyUnit.unitLevel + 1;
        switch (nextEnemy.type)
        {
            case UnitType.DPS:
                nextEnemy.maxHP = enemyUnit.maxHP + Random.Range(1, 5);
                nextEnemy.currentHP = nextEnemy.maxHP;
                nextEnemy.damage = enemyUnit.damage + Random.Range(3, 5);
                nextEnemy.healAmount = enemyUnit.healAmount + Random.Range(1, 3);
                nextEnemy.unitName = "Attack Enemy";
                break;
            case UnitType.TANK:
                nextEnemy.maxHP = enemyUnit.maxHP + Random.Range(5, 10);
                nextEnemy.currentHP = nextEnemy.maxHP;
                nextEnemy.damage = enemyUnit.damage + Random.Range(1, 3);
                nextEnemy.healAmount = enemyUnit.healAmount + Random.Range(1, 3);
                nextEnemy.unitName = "Tank Enemy";
                break;
            case UnitType.HEAL:
                nextEnemy.maxHP = enemyUnit.maxHP + Random.Range(1, 5);
                nextEnemy.currentHP = nextEnemy.maxHP;
                nextEnemy.damage = enemyUnit.damage + Random.Range(1, 3);
                nextEnemy.healAmount = enemyUnit.healAmount + Random.Range(5, 10);
                nextEnemy.unitName = "Heal Enemy";
                break;
        }
        enemyUnit = nextEnemy;

        dialogText.text = enemyUnit.unitName + " approaches...";

        playerHud.SetHUD(playerUnit);
        enemyHud.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator EnemyTurn()
    {
        bool isDead = false;
        switch (enemyUnit.type)
        {
            case UnitType.DPS:
                dialogText.text = enemyUnit.unitName + " attacks!";

                yield return new WaitForSeconds(1f);

                isDead = playerUnit.TakeDamage(enemyUnit.damage);

                playerHud.SetHP(playerUnit.currentHP);

                yield return new WaitForSeconds(1f);
                break;
            case UnitType.HEAL:
                if (enemyUnit.currentHP < (enemyUnit.maxHP / 2))
                {
                    dialogText.text = enemyUnit.unitName + " heals!";
                    isDead = false;
                    yield return new WaitForSeconds(1f);
                    //dialogText.text = enemyUnit.unitName + " heals!";
                    // StartCoroutine(EnemyHeal());
                    enemyUnit.Heal(enemyUnit.healAmount);
                    enemyHud.SetHP(enemyUnit.currentHP);
                    yield return new WaitForSeconds(1f);
                }
                else
                {
                    dialogText.text = enemyUnit.unitName + " attacks!";

                    yield return new WaitForSeconds(1f);

                    isDead = playerUnit.TakeDamage(enemyUnit.damage);

                    playerHud.SetHP(playerUnit.currentHP);

                    yield return new WaitForSeconds(1f);
                }
                break;
            case UnitType.TANK:
                if (enemyUnit.currentHP < (enemyUnit.maxHP / 4))
                {
                    dialogText.text = enemyUnit.unitName + " heals!";
                    isDead = false;
                    yield return new WaitForSeconds(1f);
                    //dialogText.text = enemyUnit.unitName + " heals!";
                    // StartCoroutine(EnemyHeal());
                    enemyUnit.Heal(enemyUnit.healAmount);
                    enemyHud.SetHP(enemyUnit.currentHP);
                    yield return new WaitForSeconds(1f);
                }
                else
                {
                    dialogText.text = enemyUnit.unitName + " attacks!";

                    yield return new WaitForSeconds(1f);

                    isDead = playerUnit.TakeDamage(enemyUnit.damage);

                    playerHud.SetHP(playerUnit.currentHP);

                    yield return new WaitForSeconds(1f);
                }
                break;
        }
        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void EndBattle()
    {
        if(state == BattleState.WON)
        {
            dialogText.text = "Battle Won!";
        }
        else if(state == BattleState.LOST)
        {
            dialogText.text = "You were defeated!";
        }
    }

    void PlayerTurn()
    {
        dialogText.text = "Choose an action: ";
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(playerUnit.healAmount);

        playerHud.SetHP(playerUnit.currentHP);

        dialogText.text = "You heal 5 hitpoints";

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyHeal()
    {
        enemyUnit.Heal(enemyUnit.healAmount);

        enemyHud.SetHP(enemyUnit.currentHP);

        dialogText.text = enemyUnit.unitName +" heals for " + enemyUnit.healAmount;

        yield return new WaitForSeconds(2f);
       
    }

    public void OnAttackButton()
    {
        if(state != BattleState.PLAYERTURN)
        {
            return;
        }
        else
        {
            StartCoroutine(PlayerAttack());

        }

    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        else
        {
            StartCoroutine(PlayerHeal());

        }

    }

}
