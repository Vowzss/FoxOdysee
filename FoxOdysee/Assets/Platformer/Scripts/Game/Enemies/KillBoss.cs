using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBoss : MonoBehaviour
{
    private PlayerManager playerManager;
    private DataSaver inventory;

    [Header("Boss Stats")]
    private DifficultyManager difficultyManager;
    public int scoreGiven;
    private int given;
    public int buffGiven;

    [Header("Boss Drop")]
    private DropItem dropItem;
    private bool dropped = false;
    private Transform cachedPosition;
    private bool refreshed = false;

    [Header("Enemy UI/UX")]
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    [Header("Boss State")]
    public int maxLife = 5;
    public int currentLife;
    public bool isDead = false;
    [HideInInspector] public bool canbeHit = true;
    [HideInInspector] public bool hasbeenHit = false;

    [Header("Enemy Attributes")]
    private GameObject enemyObject;
    private BossManager bossManager;
    private BoxCollider2D[] boxCollider2D;
    private Color[] colorDamage;
    private int colorIndex;

    private void Awake()
    {
        difficultyManager = GameObject.FindObjectOfType<DifficultyManager>();
        given = scoreGiven + difficultyManager.offsetScoreGivenBoss;
        buffGiven = given - scoreGiven;

        inventory = GameObject.FindObjectOfType<DataSaver>();
        cachedPosition = transform.parent;
        dropItem = FindObjectOfType<DropItem>();
        boxCollider2D = gameObject.GetComponentsInParent<BoxCollider2D>();
        bossManager = gameObject.GetComponentInParent<BossManager>();
        animator = GetComponentInParent<Animator>();
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
        playerManager = GameObject.FindObjectOfType<PlayerManager>();

        colorDamage = new Color[2];
        colorDamage[0] = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
        colorDamage[1] = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);

        currentLife = maxLife;
    }

    private void Update()
    {
        if (given != scoreGiven)
            scoreGiven = given;

        if (currentLife == 0 && !refreshed) isDead = true;

        if (isDead && !refreshed)
        {
            if (dropped == false) DropObjectOnDeath();

            boxCollider2D[0].enabled = false;
            boxCollider2D[1].enabled = false;
            spriteRenderer.enabled = false;
            animator.enabled = false;
            bossManager.enabled = false;
            canbeHit = false;

            currentLife = maxLife;
            refreshed = true;
        }
        else if (playerManager.isHiddenInBush && !refreshed)
        {
            boxCollider2D[0].enabled = false;
            boxCollider2D[1].enabled = false;
        }
        else if (!refreshed)
        {
            boxCollider2D[0].enabled = true;
            boxCollider2D[1].enabled = true;
            spriteRenderer.enabled = true;
            animator.enabled = true;
            bossManager.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CheckIfCanBeKilled();
        }
    }

    private void DropObjectOnDeath()
    {
        dropItem.Drop(cachedPosition);
        inventory.AddScore(given);
        dropped = true;
    }

    private void CheckIfCanBeKilled()
    {
        if (playerManager.canBeHit && !isDead && currentLife > 0)
        {
            if (canbeHit)
            {
                currentLife--;
                hasbeenHit = true;
                canbeHit = false;
            }
            StartCoroutine(SetHit());
        }
    }

    private IEnumerator SetHit()
    {
        if (!canbeHit)
        {
            spriteRenderer.color = colorDamage[1];
            yield return new WaitForSeconds(3);
            spriteRenderer.color = colorDamage[0];
            hasbeenHit = false;
            canbeHit = true;
        }
    }
}
