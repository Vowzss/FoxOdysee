using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed = 7f;
    public int damage = 10;

    PlayerManager playerManager;
    PlayerLife playerLife;

    public Vector2 target;

    private void Start()
    {
        playerManager = GameObject.FindObjectOfType<PlayerManager>();
        playerLife = GameObject.FindObjectOfType<PlayerLife>();

        target = new Vector2(playerManager.transform.position.x, playerManager.transform.position.y);
    }

    private void Update()
    {
       transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

       if (Vector2.Distance(transform.position, target) < 0.2f)
       {
            DestroyProjectile();
       }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            DestroyProjectile();
            playerLife.ApplyDamage(damage);
        }

        if (collision.CompareTag("Ground"))
        {
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
