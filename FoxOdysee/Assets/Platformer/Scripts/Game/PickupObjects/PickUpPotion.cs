using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPotion : MonoBehaviour
{
    private PlayerLife playerLife;

    [Header("Potion Attributes")]
    private DifficultyManager difficultyManager;
    public int healToGive;
    private int given;
    public int buffGiven;

    [Header("Potion UI/UX")]
    private float offset;
    private Animator animator;

    public void Awake()
    {
        difficultyManager = GameObject.FindObjectOfType<DifficultyManager>();
        given = healToGive + difficultyManager.offsetHealPotion;
        buffGiven = given - healToGive;

        playerLife = GameObject.FindObjectOfType<PlayerLife>();
        animator = GetComponent<Animator>();
        animator.SetFloat("offset", Random.Range(0.1f, 1.0f));
        offset = animator.GetFloat("offset");
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
