using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private DataSaver inventory;
    private GameObject upgradeHolder;
    private Image[] upgradeImages;
    private int playerScore;
    private Image upgradeImage;
    private Sprite selectedSprite;

    private void Awake()
    {
        inventory = GameObject.FindObjectOfType<DataSaver>();

        upgradeHolder = GameObject.FindGameObjectWithTag("UpgradeHolder");
        upgradeImage = GetComponentInChildren<Image>();
        upgradeImages = upgradeHolder.GetComponentsInChildren<Image>();
    }

    private void Update()
    {
        playerScore = inventory.transitPlayerScore;
        selectedSprite = upgradeImage.sprite;
    }

    public void ScoreDisplay()
    {
        if (playerScore >= 600 && playerScore < 1200)
            upgradeImage.sprite = upgradeImages[1].sprite;
        else if(playerScore >= 1200 && playerScore < 1800)
            upgradeImage.sprite = upgradeImages[2].sprite;

        else if (playerScore >= 1800 && playerScore < 2400)
            upgradeImage.sprite = upgradeImages[3].sprite;
        else if (playerScore >= 2400 && playerScore < 3000)
            upgradeImage.sprite = upgradeImages[4].sprite;
        else if (playerScore >= 3000 && playerScore < 4000)
            upgradeImage.sprite = upgradeImages[5].sprite;

        else if (playerScore >= 4000 && playerScore < 6000)
            upgradeImage.sprite = upgradeImages[6].sprite;
        else if (playerScore >= 6000 && playerScore < 8000)
            upgradeImage.sprite = upgradeImages[7].sprite;
        else if (playerScore >= 8000)
            upgradeImage.sprite = upgradeImages[8].sprite;

        else
           upgradeImage.sprite = upgradeImages[0].sprite;
    }
}
