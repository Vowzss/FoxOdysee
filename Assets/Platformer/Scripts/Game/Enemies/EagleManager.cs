using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleManager : MonoBehaviour
{
    private PlayerManager playerManager;
    private PlayerLife playerLife;

    [Header("Eagle Stats")]
    private DifficultyManager difficultyManager;
    public int dammage;
    private int given;
    public int buffGiven;


    [Header("Eagle Attributes")]
    public float detectionZone;
    public float eagleSpeed;
    public bool hasDetectedPlayer = true;
    public Transform[] targetObject;
    private int targetIndex;

    float distancePlayer;
    float distancePatrol;

    private void Awake()
    {
        difficultyManager = GameObject.FindObjectOfType<DifficultyManager>();
        given = dammage + difficultyManager.offsetDamageEagle;
        buffGiven = given - dammage;

        playerManager = GameObject.FindObjectOfType<PlayerManager>();
        playerLife = GameObject.FindObjectOfType<PlayerLife>();
    }

    private void Update()
    {
        if (given != dammage)
            dammage = given;

        TargetToChoose();
        Flip();

        distancePlayer = Vector2.Distance(targetObject[0].position, transform.position);
        distancePatrol = Vector2.Distance(targetObject[1].position, transform.position);

        if (targetIndex == 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetObject[0].position, eagleSpeed * Time.deltaTime);
        }
        else if (targetIndex == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetObject[1].position, eagleSpeed * Time.deltaTime);
        }
    }

    private void Flip()
    {
        if (targetObject[targetIndex].position.x > transform.position.x)
        {
            transform.localScale = new Vector3(4, 4, 4);
        }
        else if (targetObject[targetIndex].position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-4, 4, 4);
        }
    }

    private void TargetToChoose()
    {
        if (playerManager.isHiddenInBush)
            hasDetectedPlayer = false;

        if (distancePlayer < detectionZone && !playerManager.isHiddenInBush)
            hasDetectedPlayer = true;

        if (!playerLife.canTakeDamage)
            hasDetectedPlayer = false;

        if (distancePlayer > detectionZone)
            hasDetectedPlayer = false;

        if (hasDetectedPlayer)
            targetIndex = 0;
        else
            targetIndex = 1;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionZone);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerLife.canTakeDamage)
        {
            playerLife.ApplyDamage(given);
        }
    }
}