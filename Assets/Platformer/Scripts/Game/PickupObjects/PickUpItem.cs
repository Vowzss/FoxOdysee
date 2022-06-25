using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    private PlayerLife playerLife;

    [Header("Item Attributes")]
    private DifficultyManager difficultyManager;
    private int given;
    public int healToGive;
    public int buffGiven;

    public void Awake()
    {
        difficultyManager = GameObject.FindObjectOfType<DifficultyManager>();
        given = healToGive + difficultyManager.offsetHeal;
        buffGiven = given - healToGive;

        playerLife = GameObject.FindObjectOfType<PlayerLife>();

    }

    private void Update()
    {
        if (given != healToGive)
            healToGive = given;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            playerLife.ApplyHeal(given);
        }
    }
}
