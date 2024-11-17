using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Battle_System : MonoBehaviour
{
    public enum BattleState
    {
        Start,
        PlayerTurn,
        EnemyTurn,
        Won,
        Lost,
        NextEnemy
    }

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

    private GameObject enemy;
    private GameObject prefab;

    private int amountKilled;

    public Perks perks;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetUpBattle()
    {
        GameObject player = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = player.GetComponent<Player>();

        enemy = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemy.GetComponent<Enemy>();

        dialogText.text = enemyUnit.unitName + " approaches...";

        playerHud.PlayerSetHUD(playerUnit);
        enemyHud.EnemySetHUD(enemyUnit);

        state = BattleState.PlayerTurn;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        dialogText.text = "Choose an action: ";
    }
}
