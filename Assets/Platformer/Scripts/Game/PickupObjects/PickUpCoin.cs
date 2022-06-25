using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCoin : MonoBehaviour
{
    [Header("Coin Attributes")]
    private DifficultyManager difficultyManager;
    public string coinType;

    public int coinsToGive;
    private int given;
    public int buffGiven;


    [Header("Coin UI/UX")]
    private float offset;
    private DataSaver dataSaver;
    private Animator animator;


    public void Awake()
    {
        difficultyManager = GameObject.FindObjectOfType<DifficultyManager>();
        given = coinsToGive + difficultyManager.offsetCoin;
        buffGiven = given - coinsToGive;

        dataSaver = GameObject.FindObjectOfType<DataSaver>();
        animator = GetComponent<Animator>();
        animator.SetFloat("offset", Random.Range(0.1f, 1.0f));
        offset = animator.GetFloat("offset");
        coinType = transform.parent.gameObject.name;
    }

    private void Update()
    {
        if (given != coinsToGive)
            coinsToGive = given;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            dataSaver.AddCoin(given);
        }
    }
}
