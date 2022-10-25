using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [HideInInspector] public bool needToRefresh = false;

    private TimerLevels timerLevels;
    private PlayerLife playerLife;
    private NextLevel nextLevel;
    private DataSaver inventory;

    private void Awake()
    {
        inventory = GameObject.FindObjectOfType<DataSaver>();
        nextLevel = GameObject.FindObjectOfType<NextLevel>();
        timerLevels = GameObject.FindObjectOfType<TimerLevels>();
        playerLife = GameObject.FindObjectOfType<PlayerLife>();
    }

    private void Update()
    {
        if(playerLife.playerIsDead || timerLevels.timeRemaining <= 0)
        {
            if (!needToRefresh)
            {
                inventory.playerDeath++;
                StartCoroutine(nextLevel.GoToNextScene("GameOver"));
                needToRefresh = true;
            }
        }
    }
}
