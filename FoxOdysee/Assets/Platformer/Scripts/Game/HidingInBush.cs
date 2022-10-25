using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HidingInBush : MonoBehaviour
{
    private PlayerManager playerManager;
    public Canvas display;

    private void Start()
    {
        display = gameObject.transform.parent.gameObject.GetComponentInChildren<Canvas>();
        display.enabled = false;
        playerManager = GameObject.FindObjectOfType<PlayerManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            display.enabled = true;
            playerManager.isInBushBox = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            display.enabled = false;
            playerManager.isInBushBox = false;
            playerManager.isHiddenInBush = false;
        }
    }
}
