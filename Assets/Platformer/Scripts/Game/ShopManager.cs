using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public bool isShopOppenned = false;
    private Manager manager;
    private GameObject shopInterface;
    private GameObject shopTitle;

    public ShopSlot[] shopSlots;

    private void Awake()
    {
        shopSlots = GameObject.FindObjectsOfType<ShopSlot>();
        manager = GameObject.FindObjectOfType<Manager>();
        shopInterface = GameObject.FindGameObjectWithTag("ShopInterface");
        shopTitle = GameObject.FindGameObjectWithTag("ShopTitle");
    }

    public void EraseShopData()
    {
        foreach (ShopSlot slot in shopSlots)
        {
            slot.canBuy = false;
            slot.bought = false;
            slot.interactable = true;
            slot.buttonColor = Color.white;
        }
    }

    private void HandleOpenShop()
    {
        if (isShopOppenned)
        {
            Time.timeScale = 0;
            shopInterface.SetActive(true);
            shopTitle.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            shopInterface.SetActive(false);
            shopTitle.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.B) && manager.currentLevel != 0)
        {
            isShopOppenned = !isShopOppenned; 
        }
    }

    private void Update()
    {
        HandleOpenShop();
    }
}
