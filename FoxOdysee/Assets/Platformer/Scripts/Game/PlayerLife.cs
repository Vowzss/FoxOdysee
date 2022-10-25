using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    private KillBoss killBoss;
    private PlayerManager playerManager;

    [Header("Player States")]
    public float damageAnimationDelay;
    public float delayAfterDamageReEnable;
    [HideInInspector] public bool canTakeDamage = true;

    [Tooltip("Stands for Invincibility timer, the higher the long you will be invincible.")]
    public float vanishTimer;
    private bool isInvincible;

    private Color[] colorDamage;
    private int colorIndex;

    [Header("Player Life")]
    private DifficultyManager difficultyManager;
    [HideInInspector] public bool playerIsDead = false;
    public int maxLife = 100;
    public int currentLife;

    [Header("Player UI/UX")]
    [HideInInspector] public LifeManager lifeBar;
    private Animator animator;

    private void Start()
    {
        difficultyManager = GameObject.FindObjectOfType<DifficultyManager>();

        playerManager = GameObject.FindObjectOfType<PlayerManager>();
        currentLife = maxLife + difficultyManager.offsetHealth;
        maxLife = currentLife;

        colorIndex = 0;

        colorDamage = new Color[2];
        colorDamage[0] = new Color(playerManager.display[0].color.r, playerManager.display[0].color.g, playerManager.display[0].color.b, 1);
        colorDamage[1] = new Color(playerManager.display[0].color.r, playerManager.display[0].color.g, playerManager.display[0].color.b, 0.5f);

        lifeBar = GameObject.FindObjectOfType<LifeManager>();
        animator = GetComponent<Animator>();

        lifeBar.SetMaxLife(maxLife);
        killBoss = GameObject.FindObjectOfType<KillBoss>();
    }

    private void Update()
    {
        lifeBar.SetText(currentLife.ToString() + " / " + maxLife);
        lifeBar.SetLife(currentLife);
        HandleLife();

        if (vanishTimer > 0)
            isInvincible = true;

        if (isInvincible)
        {
            vanishTimer -= Time.deltaTime;
            VanishPlayer();
        }
    }

    public void ApplyDamage(int damage)
    {
        if (canTakeDamage)
        {
            currentLife -= damage;
            canTakeDamage = false;

            StartCoroutine(DamageAnimation());
            StartCoroutine(DamageDelay());
        }
    }
    public void ApplyHeal(int heal)
    {
        if (currentLife == maxLife)
            return;
        else
        {
            currentLife += heal;

            if (currentLife > maxLife)
                currentLife = maxLife;
        }
    }

    private void HandleLife()
    {
        if (currentLife <= 0)
            playerIsDead = true;
        else
            playerIsDead = false;
    }

    private void VanishPlayer()
    {
        if (vanishTimer > 0 && colorIndex == 0)
        {
            canTakeDamage = false;
            playerManager.display[0].color = colorDamage[1];
        }
        else if(vanishTimer <= 0 && colorIndex != 1)
        {
            vanishTimer = 0;
            canTakeDamage = true;
            playerManager.display[0].color = colorDamage[0];
            isInvincible = false;
        }
    }

    private IEnumerator DamageAnimation()
    {
        while(!canTakeDamage)
        {
            playerManager.display[0].color = colorDamage[1];
            animator.SetBool("gotHurt", true);
            yield return new WaitForSeconds(damageAnimationDelay);
            animator.SetBool("gotHurt", false);
            playerManager.display[0].color = colorDamage[0];
        }
    }

    private IEnumerator DamageDelay()
    {
        yield return new WaitForSeconds(delayAfterDamageReEnable);
        canTakeDamage = true;
    }
}
