using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    private ActualLevel refLevel;

    [Header("Level Data")]
    [SerializeField] public int previousLevel;
    [SerializeField] public int currentLevel;
    [SerializeField] public int nextLevelToUnlock;

    [Header("Level Already Unlocked")]
    [SerializeField] public int levelUnlocked;

    private void Awake()
    {
        levelUnlocked = 1;
    }

    private void Update()
    {
        refLevel = GameObject.FindObjectOfType<ActualLevel>();

        previousLevel = refLevel.previousLevel;
        currentLevel = refLevel.curentLevel;
        nextLevelToUnlock = refLevel.nextLevel;
    }

    private void LateUpdate()
    {
        Invoke("UpdateLevelUnlocked", 1);
    }

    public void WipeData()
    {
        previousLevel = 1;
        currentLevel = 0;
        nextLevelToUnlock = 1;

        levelUnlocked = 1;
    }

    private void UpdateLevelUnlocked()
    {
        if (!refLevel.refreshed)
        {
            if (currentLevel >= 0)
            {
                if(levelUnlocked < currentLevel)
                    levelUnlocked = currentLevel;
                refLevel.refreshed = !refLevel.refreshed;
            }
        }
    }

    public void UnlockLevel()
    {
        levelUnlocked++;

        if (levelUnlocked > 5)
            levelUnlocked = 5;
    }
}