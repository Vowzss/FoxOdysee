using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogDamage : MonoBehaviour
{
    [Header("Frog Stats")]
    private DifficultyManager difficultyManager;
    public int applyDamageValue;
    private int given;
    public int buffGiven;

    [Header("Attributes")]
    private PlayerManager playerManager;
    private PlayerLife pLife;
    private KillFrog killFrog;

    private void Awake()
    {
        difficultyManager = GameObject.FindObjectOfType<DifficultyManager>();
        given = applyDamageValue + difficultyManager.offsetDamageFrog;
        buffGiven = given - applyDamageValue;

        playerManager = GameObject.FindObjectOfType<PlayerManager>();
        killFrog = GameObject.FindObjectOfType<KillFrog>();
        pLife = GameObject.FindObjectOfType<PlayerLife>();
    }
    private void Update()
    {
        if (given != applyDamageValue)
            applyDamageValue = given;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            CheckIfCanBeHit();
    }

    private void CheckIfCanBeHit()
    {
        if (playerManager.canBeHit && !killFrog.isDead)
            pLife.ApplyDamage(given);
    }
}
