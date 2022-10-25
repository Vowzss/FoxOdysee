using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLock : MonoBehaviour
{
    private Manager manager;

    [Header("Level Info")]
    public string levelName;
    public bool isUnlocked;
    private Image[] levelImages;

    private void Awake()
    {
        levelName = name;
        manager = GameObject.FindObjectOfType<Manager>();
        levelImages = GetComponentsInChildren<Image>();
    }

    private void Update()
    {
        if (manager.levelUnlocked >= int.Parse(levelName))
            isUnlocked = true;

        if (isUnlocked)
        {
            levelImages[1].gameObject.SetActive(false);
            levelImages[2].gameObject.SetActive(true);
        }
        else
        {
            levelImages[1].gameObject.SetActive(true);
            levelImages[2].gameObject.SetActive(false);
        }
    }
}