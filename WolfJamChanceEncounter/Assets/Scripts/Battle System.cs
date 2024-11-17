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

    public DialogBoxHub dialogBoxHub;

    public Player playerUnit;
    public Enemy enemyUnit;

    [SerializeField] private GameObject dpsSprite;
    [SerializeField] private GameObject tankSprite;
    [SerializeField] private GameObject healSprite;

    private GameObject enemyGo;
    private GameObject prefab;

    private int amountKilled;

    public Perks perks;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetUpBattle());
       
    }

    IEnumerator SetUpBattle()
    {
        GameObject playerGo = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGo.GetComponent<Player>();

        enemyGo = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGo.GetComponent<Enemy>();

        dialogText.text = enemyUnit.unitName + " approaches...";

        playerHud.PlayerSetHUD(playerUnit);
        enemyHud.EnemySetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        //Damage the enemy 
        playerUnit.currentStamina -= (int)(playerUnit.maxStamina * 0.3);
        playerHud.SetStamina(playerUnit);
        //Debug.Log("Current Stamina "+playerUnit.currentStamina+" max stamina " + playerUnit.maxStamina);
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
            amountKilled++;
            if(amountKilled == 1)//amountKilled %2 == 0)
            {
                state = BattleState.WON;
                StartCoroutine(Perks());
                //EndBattle();
            }
            else
            {
                state = BattleState.NEXTENEMY;
                StartCoroutine(SetNextEnemy());
            }
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
        Enemy nextEnemy = EnemySpawner.Instance.getEnemy();
        nextEnemy.unitLevel = enemyUnit.unitLevel + 1;
        
        switch (nextEnemy.type)
        {
            case UnitType.DPS:
                nextEnemy.maxHP = enemyUnit.maxHP + Random.Range(1, 5);
                nextEnemy.currentHP = nextEnemy.maxHP;
                nextEnemy.damage = enemyUnit.damage + Random.Range(3, 5);
                nextEnemy.healAmount = enemyUnit.healAmount + Random.Range(1, 3);
                nextEnemy.unitName = "Attack Enemy";
                prefab = dpsSprite;
                //nextEnemy.GetComponentInChildren<SpriteRenderer>().sprite = dpsSprite;
                //enemyGo.GetComponentInChildren<SpriteRenderer>().sprite = dpsSprite;
                break;
            case UnitType.TANK:
                nextEnemy.maxHP = enemyUnit.maxHP + Random.Range(5, 10);
                nextEnemy.currentHP = nextEnemy.maxHP;
                nextEnemy.damage = enemyUnit.damage + Random.Range(1, 3);
                nextEnemy.healAmount = enemyUnit.healAmount + Random.Range(1, 3);
                nextEnemy.unitName = "Tank Enemy";
                prefab = tankSprite;
                //nextEnemy.GetComponentInChildren<SpriteRenderer>().sprite = tankSprite;
                //enemyGo.GetComponentInChildren<SpriteRenderer>().sprite = tankSprite;
                break;
            case UnitType.HEAL:
                nextEnemy.maxHP = enemyUnit.maxHP + Random.Range(1, 5);
                nextEnemy.currentHP = nextEnemy.maxHP;
                nextEnemy.damage = enemyUnit.damage + Random.Range(1, 3);
                nextEnemy.healAmount = enemyUnit.healAmount + Random.Range(5, 10);
                nextEnemy.unitName = "Heal Enemy";
                prefab = healSprite;
                //nextEnemy.GetComponentInChildren<SpriteRenderer>().sprite = healSprite;
                //enemyGo.GetComponentInChildren<SpriteRenderer>().sprite = healSprite;
                break;
        }
        Destroy(enemyGo);
        GameObject enemyGos = Instantiate(prefab, enemyBattleStation);
        enemyUnit = enemyGos.GetComponent<Enemy>();
        enemyUnit = nextEnemy;
        //enemyUnit.GetComponentInChildren<SpriteRenderer>().sprite = nextEnemy.GetComponentInChildren<SpriteRenderer>().sprite;
        //enemyUnit.sRend = nextEnemy.sRend;

        dialogText.text = enemyUnit.unitName + " approaches...";

        playerHud.PlayerSetHUD(playerUnit);
        enemyHud.EnemySetHUD(enemyUnit);

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
        if (playerUnit.guard)
        {
            playerUnit.guard = false;
        }

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            playerUnit.RecoverStamina();
            playerHud.SetStamina(playerUnit);
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    IEnumerator Perks()
    {
        int random = Random.Range(0, perks.allPerks.Count);
        dialogText.text = perks.allPerks[random].message;
        Debug.Log("Random num: " + random);
        yield return new WaitForSeconds(1.5f);
        dialogBoxHub.attackButton.gameObject.SetActive(false);
        dialogBoxHub.healButton.gameObject.SetActive(false);
        dialogBoxHub.guardButton.gameObject.SetActive(false);
        if (state == BattleState.WON)
        {
            if (perks.allPerks[random].perkType == Reward.damage)
            {
                playerUnit.damage += (int)perks.allPerks[random].modifier;
                dialogText.text = "Damaged increased";
                dialogBoxHub.UIPerk("fire");
            }
            else if (perks.allPerks[random].perkType == Reward.heal)
            {
                playerUnit.maxHP += (int)perks.allPerks[random].modifier;
                playerUnit.healAmount += (int)perks.allPerks[random].modifier;
                dialogText.text = "Health increased";
                dialogBoxHub.UIPerk("heart");

            }
            else if (perks.allPerks[random].perkType == Reward.stamina )
            {
                playerUnit.staminaRecovery += perks.allPerks[random].modifier;
                dialogText.text = "Stamina recovery increased";
                dialogBoxHub.UIPerk("star");
            }
            yield return new WaitForSeconds(1.5f);
        }
        dialogBoxHub.PerkOver();
        state = BattleState.NEXTENEMY;
        StartCoroutine(SetNextEnemy());
        
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
        playerUnit.currentStamina -= (int)(playerUnit.maxStamina * 0.5);
        playerHud.SetStamina(playerUnit);
        playerUnit.Heal(playerUnit.healAmount);

        playerHud.SetHP(playerUnit.currentHP);

        dialogText.text = $"You heal {playerUnit.healAmount} hitpoints";

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator PlayerGuard()
    {
        Debug.Log("Guard");
        playerUnit.GuardCheck();

        dialogText.text = $"{playerUnit.unitName} braces for impact: ";

        yield return new WaitForSeconds(1.5f);
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
    #region Attack and Heal buttons
    public void OnAttackButton()
    {
        if(state == BattleState.PLAYERTURN && (playerUnit.currentStamina >= (0.3 * playerUnit.maxStamina)))
        {
            StartCoroutine(PlayerAttack());
        }
        else
        {
            if (playerUnit.currentStamina < (0.3 * playerUnit.maxStamina))
            {
                dialogText.text = "Not enough stamina!";
            }
            return;
        }

    }

    public void OnHealButton()
    {
        if (state == BattleState.PLAYERTURN && (playerUnit.currentStamina >= (0.5 * playerUnit.maxStamina)))
        {
            StartCoroutine(PlayerHeal());
        }
        else
        {
            if (playerUnit.currentStamina < (0.5 * playerUnit.maxStamina))
            {
                dialogText.text = "Not enough stamina!";
            }
            return;
        }
    }

    public void OnGuardButton()
    {
        if(state != BattleState.PLAYERTURN)
        {
            return;
        }
        else
        {
            StartCoroutine(PlayerGuard());
          
        }
    }
    #endregion 


}
