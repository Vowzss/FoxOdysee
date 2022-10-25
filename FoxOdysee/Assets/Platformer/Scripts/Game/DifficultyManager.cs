using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public DataSaver dataSaver;

    [Header("Player Stats")]
    public int offsetHealth;

    [Header("Items Stats")]
    public int offsetCoin;
    public int offsetHeal;

    [Header("Enemies Stats")]
    public int offsetDamageFrog;
    public int offsetDamageEagle;
    public int offsetDamageBoss;

    [Header("Score Enemies Stats")]
    public int offsetScoreGivenFrog;
    public int offsetScoreGivenEagle;
    public int offsetScoreGivenBoss;

    [Header("Interactables Stats")]
    public int offsetSpikesDamage;
    public int offsetHealPotion;

    private void Awake()
    {
        dataSaver = GameObject.FindObjectOfType<DataSaver>();
    }

    private void Update()
    {
        switch (dataSaver.currentDifficulty)
        {
            case "EASY":
                offsetHealth = 0;

                offsetCoin = 5;
                offsetHeal = 10;

                offsetDamageFrog = 10;
                offsetDamageEagle = 20;
                offsetDamageBoss = 10;

                offsetScoreGivenFrog = 100;
                offsetScoreGivenEagle = 500;
                offsetScoreGivenBoss = 1000;

                offsetSpikesDamage = 5;
                offsetHealPotion = 5;
                break;

            case "MEDIUM":
                offsetHealth = 30;

                offsetCoin = 3;
                offsetHeal = 5;

                offsetDamageFrog = 20;
                offsetDamageEagle = 30;
                offsetDamageBoss = 15;

                offsetScoreGivenFrog = 75;
                offsetScoreGivenEagle = 300;
                offsetScoreGivenBoss = 800;

                offsetSpikesDamage = 10;
                offsetHealPotion = 10;
                break;

            case "HARD":
                offsetHealth = 50; 

                offsetCoin = 0;
                offsetHeal = 3;

                offsetDamageFrog = 30;
                offsetDamageEagle = 40;
                offsetDamageBoss = 20;

                offsetScoreGivenFrog = 50;
                offsetScoreGivenEagle = 100;
                offsetScoreGivenBoss = 600;

                offsetSpikesDamage = 15;
                offsetHealPotion = 15;
                break;

            default:
                break;
        }
    }
}
