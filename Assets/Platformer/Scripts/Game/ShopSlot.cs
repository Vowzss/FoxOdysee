using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    private DataSaver inventory;
    private TimerLevels timerLevels;
    private PlayerManager playerManager;
    private Manager manager;

    public Item item;
    public GameObject slot;
    public GameObject priceTitle;
    public GameObject itemTitle;
    private Text slotTitle;


    private Button slotButton;
    private Image[] slotImage;
    private Text slotTexte;
    private Sprite imageToDisplay;
    private Image buttonImage;

    [HideInInspector] public Color buttonColor;

    [HideInInspector] public bool canBuy;
    [HideInInspector] public bool bought;
    [HideInInspector] public bool interactable;
    [HideInInspector] public Color[] color;
    
    private void Awake()
    {
        interactable = true;

        manager = GameObject.FindObjectOfType<Manager>();
        inventory = GameObject.FindObjectOfType<DataSaver>();

        slotImage = slot.GetComponentsInChildren<Image>();
        slotTexte = priceTitle.GetComponentInChildren<Text>();
        slotButton = priceTitle.GetComponent<Button>();
        slotTitle = itemTitle.GetComponentInChildren<Text>();

        imageToDisplay = item.icon;
        slotTexte.text = item.price.ToString();
        slotTitle.text = item.itemName;

        slotImage[1].sprite = imageToDisplay;

        buttonImage = slotButton.GetComponent<Image>();
        color = new Color[2]; color[0] = Color.red; color[1] = Color.green;

        buttonColor = Color.white;
    }

    private void Update()
    {
        timerLevels = GameObject.FindObjectOfType<TimerLevels>();
        playerManager = GameObject.FindObjectOfType<PlayerManager>();

        buttonImage.color = buttonColor;
        slotButton.interactable = interactable;

        if (inventory.crownBought && item.itemName == "Crown")
            interactable = false;
    }

    public void CheckIfCanBuyItem()
    {
        if (inventory.transitCoinsStacked < item.price)
        {
            canBuy = false;
            buttonColor = color[0];
        }
        else
        {
            canBuy = true;
            buttonColor = color[1];
            BuyItem();
        }
    }
    public void BuyItem()
    {
        bought = true; canBuy = false;
        inventory.RemoveCoin(item.price);
        interactable = false;
        Give(item);
    }

    private void Give(Item item)
    {
        switch (item.name)
        {
            case "Topaz Bag":
                inventory.cachedTopazsStacked++;
                break;
            case "Cheese":
                playerManager.playerLife.currentLife += item.attribute;
                break;
            case "Bread":
                playerManager.playerLife.currentLife += item.attribute;
                break;
            case "Crown":
                inventory.crownBought = true;
                break;
            case "Hourglass":
                timerLevels.AddTime(item.attribute);
                break;
            case "Key":
                manager.levelUnlocked++;
                break;
            case "Clover":
                    inventory.cloverBought = true;
                break;
            case "Vanish":
                playerManager.playerLife.vanishTimer += item.attribute;
                break;

            default:
                break;
        }
    }
}
