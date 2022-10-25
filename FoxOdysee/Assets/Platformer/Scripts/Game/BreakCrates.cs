using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakCrates : MonoBehaviour
{
    private PlayerManager playerManager;
    public Vector3 objectPosition;

    [Header("Crate State")]
    [SerializeField] private float timeBeforeRespawn;
    [SerializeField] private float timeBeforeBreak;
    private float storedTimeBeforeBreak;
    private float storedTimeBeforeRespawn;
    [SerializeField] private bool isBroken;
    [SerializeField] private bool isColliding = false;

    [Header("Crate UI/UX")]
    private Animator animator;
    private BoxCollider2D boxCollider2D;

    private void Start()
    {
        playerManager = GameObject.FindObjectOfType<PlayerManager>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("isBroken", false);
        storedTimeBeforeBreak = timeBeforeBreak;
        storedTimeBeforeRespawn = timeBeforeRespawn;
    }

    private void Update()
    {
        HandleTimeRemaning();
        HandleColliding();

        RespawnHandler();
    }

    private void HandleTimeRemaning()
    {
        if (timeBeforeBreak < 0)
        {
            isColliding = false;
            isBroken = true;
            animator.SetBool("isBroken", true);
        }
        else
        {
            isBroken = false;
            animator.SetBool("isBroken", false);
        }
    }

    private void HandleColliding()
    {
        if(isColliding)
            timeBeforeBreak -= Time.deltaTime;
    }

    private void RespawnCrate()
    {
        timeBeforeBreak = storedTimeBeforeBreak;
        timeBeforeRespawn = storedTimeBeforeRespawn;
        isBroken = false;
    }

    private void RespawnHandler()
    {
        if (isBroken && timeBeforeRespawn >= 0)
        {
            timeBeforeRespawn -= Time.deltaTime;
        }

        if (timeBeforeRespawn < 0)
            RespawnCrate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerManager.isOnCrate = true;
            isColliding = true;
            objectPosition = collision.gameObject.transform.position;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerManager.isOnCrate = false;
            isColliding = false;
        }
    }
}
