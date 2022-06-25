using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBorders : MonoBehaviour
{
    private PlayerManager playerManager;
    private SpriteRenderer spriteRenderer;

    [Header("Border UI/UX")]
    public Material material;

    private void Awake()
    {
        playerManager = GameObject.FindObjectOfType<PlayerManager>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.material = material;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerManager.isOutOfBorder = true;
        }
    }
}