using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    private PlayerLife playerLife;
    private PlayerManager playerManager;
    [HideInInspector] public GameObject projectilesHolder;

    [Header("Boss Stats")]
    private DifficultyManager difficultyManager;
    public int applyDamageValue;
    private int given;
    public int buffGiven;

    [Header("Boss Movement")]
    public float enemySpeed;
    [HideInInspector] public bool isFlip = false;

    [Header("Boss Attributes")]
    private GameObject playerTarget;
    [HideInInspector] public GameObject shootPoint;
    private BossZone bossZone;

    public float fireRate;
    private float cachedfireRate;

    private float randomizer;
    public GameObject projectilToUse;
    public GameObject[] projectileList;
    private KillBoss killBoss;

    public Transform getWaypointsList;
    private Transform[] waypointList;
    private Transform startPoint;
    private int endPoint = 0;

    private void Awake()
    {
        playerManager = GameObject.FindObjectOfType<PlayerManager>();
        difficultyManager = GameObject.FindObjectOfType<DifficultyManager>();
        given = applyDamageValue + difficultyManager.offsetDamageBoss;
        buffGiven = given - applyDamageValue;

        killBoss = GameObject.FindObjectOfType<KillBoss>();
        playerLife = GameObject.FindObjectOfType<PlayerLife>();
        waypointList = getWaypointsList.GetComponentsInChildren<Transform>();
        startPoint = waypointList[0];
    }

    private void Start()
    {
        cachedfireRate = fireRate;
        bossZone = GameObject.FindObjectOfType<BossZone>();
        playerTarget = GameObject.FindGameObjectWithTag("Player");
        projectilToUse = projectileList[0]; // set first projectil otherwise function randomizer isn't call at time before projectiluse instanciated
    }

    void Update()
    {
        if (given != applyDamageValue)
            applyDamageValue = given;

        if (!killBoss.isDead)
        {
            Vector3 dir = startPoint.transform.position - transform.position;
            transform.Translate(dir.normalized * enemySpeed * Time.deltaTime * Time.timeScale, Space.World);

            if (Vector3.Distance(transform.position, startPoint.transform.position) < 0.3f)
            {
                endPoint = (endPoint + 1) % waypointList.Length;
                startPoint = waypointList[endPoint];
            }

            if (startPoint.position == waypointList[1].position && isFlip)
                Flip();
            else if (startPoint.position == waypointList[0].position && !isFlip)
                Flip();

            StartCoroutine(temp());

            if (bossZone.hasDetectedTarget && playerManager.canBeHit)
            {
                if (fireRate <= 0)
                {
                    Instantiate(projectilToUse, shootPoint.transform.position, Quaternion.identity, projectilesHolder.transform);
                    fireRate = cachedfireRate;
                }
                else
                {
                    fireRate -= Time.deltaTime;
                }
            }
        }
    }

    private void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        isFlip = !isFlip;
    }

    private void randomize()
    {
        randomizer = UnityEngine.Random.value;
        if (randomizer >= 0.5)
            projectilToUse = projectileList[0];
        else
            projectilToUse = projectileList[1];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !killBoss.hasbeenHit && playerManager.canBeHit)
            playerLife.ApplyDamage(given);
    }

    private IEnumerator temp()
    {
        yield return new WaitForSeconds(cachedfireRate);
        randomize();
    }
}