using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEagle : MonoBehaviour
{
    private PlayerManager playerManager;
    private DataSaver inventory;

    [Header("Eagle Drop")]
    private DropItem dropItem;
    [HideInInspector] public bool dropItemOnDeath = false;
    private Transform cachedPosition;
    public int scoreGiven;

    [Header("Eagle UI/UX")]
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    [Header("Eagle State")]
    public bool isDead = false;
    public float timeBeforeRespawn = 20;
    private float storedTimeBeforeRespawn;

    [Header("Eagle Attributes")]
    public Transform enemyRespawnPoint;
    public GameObject enemyObject;

    private EagleManager eagleManager;
    private BoxCollider2D boxCollider2D;
    private CircleCollider2D circleCollider2D;

    private void Awake()
    {
        inventory = GameObject.FindObjectOfType<DataSaver>();
        cachedPosition = transform.parent;

        storedTimeBeforeRespawn = timeBeforeRespawn;

        dropItem = GameObject.FindObjectOfType<DropItem>();
        circleCollider2D = gameObject.GetComponentInParent<CircleCollider2D>();
        eagleManager = gameObject.GetComponentInParent<EagleManager>();
        boxCollider2D = GetComponent<BoxCollider2D>();

        animator = GetComponentInParent<Animator>();
        spriteRenderer = GetComponentInParent<SpriteRenderer>();

        playerManager = GameObject.FindObjectOfType<PlayerManager>();
    }

    private void Update()
    {
        //if(!isDead) dropItemOnDeath = false; //if activated difficulty is very easy ... can farm mobs if you have low hp

        if (isDead)
        {
            if (dropItemOnDeath == false)
                DropObjectOnDeath();

            enemyObject.transform.position = enemyRespawnPoint.position;
            circleCollider2D.enabled = false;
            boxCollider2D.enabled = false;
            spriteRenderer.enabled = false;
            animator.enabled = false;
            eagleManager.enabled = false;

            RespawnHandler();
        }
        else
        {
            circleCollider2D.enabled = true;
            boxCollider2D.enabled = true;
            spriteRenderer.enabled = true;
            animator.enabled = true;
            eagleManager.enabled = true;
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
            isDead = true;
    }

    private void DropObjectOnDeath()
    {
        dropItem.Drop(cachedPosition);
        inventory.AddScore(scoreGiven);
        dropItemOnDeath = true;
    }
}