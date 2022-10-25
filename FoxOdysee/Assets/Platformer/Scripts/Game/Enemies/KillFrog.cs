using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFrog : MonoBehaviour
{
    private PlayerManager playerManager;
    private DataSaver inventory;

    [Header("Frog Stats")]
    public DifficultyManager difficultyManager;
    public int scoreToGive;
    private int given;
    public int buffGiven;

    [Header("Frog Drop")]
    private DropItem dropItem;
    [HideInInspector] public bool dropItemOnDeath = false;
    private Transform cachedPosition;

    [Header("Frog UI/UX")]
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    [Header("Frog State")]
    public bool isDead = false;
    public float timeBeforeRespawn = 20;
    private float storedTimeBeforeRespawn;

    [Header("Frog Attributes")]
    private GameObject enemyObject;
    private FrogManager frogManager;
    private BoxCollider2D[] boxCollider2D;

    private void Awake()
    {
        storedTimeBeforeRespawn = timeBeforeRespawn;
        cachedPosition = transform.parent;

        difficultyManager = GameObject.FindObjectOfType<DifficultyManager>();
        given = scoreToGive + difficultyManager.offsetScoreGivenFrog;
        buffGiven = given - scoreToGive;

        inventory = GameObject.FindObjectOfType<DataSaver>();
        dropItem = FindObjectOfType<DropItem>();
        boxCollider2D = gameObject.GetComponentsInParent<BoxCollider2D>();
        frogManager = gameObject.GetComponentInParent<FrogManager>();
        animator = GetComponentInParent<Animator>();
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
        playerManager = GameObject.FindObjectOfType<PlayerManager>(); 
    }

    private void Update()   
    {
        if (given != scoreToGive)
            scoreToGive = given;

        //if(!isDead) dropItemOnDeath = false; //if activated difficulty is very easy ... can farm mobs if you have low hp

        if (isDead)
        {
            if (dropItemOnDeath == false)
                DropObjectOnDeath();

            boxCollider2D[0].enabled = false;
            boxCollider2D[1].enabled = false;
            spriteRenderer.enabled   = false;
            animator.enabled         = false;
            frogManager.enabled      = false;

            RespawnHandler();
        }
        else if(playerManager.isHiddenInBush)
        {
            boxCollider2D[0].enabled = false;
            boxCollider2D[1].enabled = false;
        }
        else
        {
            boxCollider2D[0].enabled = true;
            boxCollider2D[1].enabled = true;
            spriteRenderer.enabled   = true;
            animator.enabled         = true;
            frogManager.enabled      = true;
        }
    }

    private void RespawnEnemy()
    {
        timeBeforeRespawn = storedTimeBeforeRespawn;
        isDead = false;
    }

    private void RespawnHandler()
    {
        if (isDead && timeBeforeRespawn >= 0)
            timeBeforeRespawn -= Time.deltaTime;

        if (timeBeforeRespawn < 0)
            RespawnEnemy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            CheckIfCanBeKilled();
    }

    private void CheckIfCanBeKilled()
    {
        if (playerManager.canBeHit && !isDead)
            isDead = true;
        else
            isDead = false;
    }

    private void DropObjectOnDeath()
    {
        dropItem.Drop(cachedPosition);
        inventory.AddScore(given);
        dropItemOnDeath = true;
    }
}