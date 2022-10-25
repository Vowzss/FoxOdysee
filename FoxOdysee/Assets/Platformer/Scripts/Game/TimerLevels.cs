using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TimerLevels : MonoBehaviour
{
    private Text timerText;
    private Manager manager;

    public bool needToRefreshed;

    private GameObject imageHolder;
    private Image[] timerImages;
    private Image timerImage;

    [HideInInspector] public float timerCached;

    [Header("Timer State")]
    public float timeRemaining = 150;
    public bool timerOff = false;

    private void Awake()
    {
        manager = GameObject.FindObjectOfType<Manager>();

        timerCached = timeRemaining;
        needToRefreshed = false;

        timerText = GetComponentInChildren<Text>();
        timerImage = GetComponentInChildren<Image>();

        imageHolder = GameObject.FindGameObjectWithTag("imageHolder");
        timerImages = imageHolder.GetComponentsInChildren<Image>();
    }

    private void Update()
    {
        if (manager.currentLevel == 0)
        {
            needToRefreshed = false;
            timerOff = true;
            timeRemaining = timerCached;
        }
        else if(timeRemaining >= 150)
        {
            needToRefreshed = true;
            timerOff = false;
        }

        if (!timerOff && needToRefreshed)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime * Time.timeScale;
                timerText.text = "Time Remaining: " + timeRemaining.ToString("n2");
            }
        }

        if (timerOff) needToRefreshed = false;
        else needToRefreshed = true;

        HandleTimeRemaining();
    }

    private void HandleTimeRemaining()
    {
        if(timeRemaining < timerCached/ 2)
            timerImage.sprite = timerImages[1].sprite;
        else if(timeRemaining < timerCached/4)
            timerImage.sprite = timerImages[2].sprite;
        else
            timerImage.sprite = timerImages[0].sprite;
    }

    public void AddTime(float time)
    {
        timeRemaining += time;
    }
}
