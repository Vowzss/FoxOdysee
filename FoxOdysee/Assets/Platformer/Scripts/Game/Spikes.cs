using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private GameObject spikeObject;
    private PlayerLife playerLife;

    [Header("Spikes Attributes")]
    private DifficultyManager difficultyManager;
    public int damageToApply;
    private int given;
    public int buffGiven;


    private void Awake()
    {
        difficultyManager = GameObject.FindObjectOfType<DifficultyManager>();
        given = damageToApply + difficultyManager.offsetSpikesDamage;
        buffGiven = given - damageToApply;

        playerLife = GameObject.FindObjectOfType<PlayerLife>();
    }

    private void Update()
    {
        if (given != damageToApply)
            damageToApply = given;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerLife.ApplyDamage(given);
        }
    }
}
