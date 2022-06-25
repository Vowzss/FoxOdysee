using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpTopaz : MonoBehaviour
{
    [Header("Topaz UI/UX")]
    [SerializeField] private float offset;
    [SerializeField] private DataSaver inventory;
    private Animator animator;

    public int amountGiven = 1;

    public void Awake()
    {
        inventory = GameObject.FindObjectOfType<DataSaver>();
        animator = GetComponent<Animator>();
        animator.SetFloat("offset", Random.Range(0.1f, 1.0f));
        offset = animator.GetFloat("offset");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            inventory.AddTopaz(amountGiven);
        }
    }
}
