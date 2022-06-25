using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    private AudioManager audioManager;
    private DataSaver dataSaver;
    private Manager manager;
    private FadeScene fadeScene;

    public GameObject[] levelObjects;
    public Button[] levelButtons;


    private void Awake()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        dataSaver = GameObject.FindObjectOfType<DataSaver>();
        fadeScene = GameObject.FindObjectOfType<FadeScene>();
        manager = GameObject.FindObjectOfType<Manager>();
        levelObjects = GameObject.FindGameObjectsWithTag("LevelButton");
    }

    private void Start()
    {
        levelButtons = new Button[levelObjects.Length];

        for (int i = 0; i < levelObjects.Length; i++)
            levelButtons[i] = levelObjects[i].GetComponent<Button>();
    }

    private void Update()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if(i + 1 > manager.levelUnlocked)
                levelButtons[i].interactable = false;
            else
                levelButtons[i].interactable = true;
        }
    }

    public void LoadUnlockedLevel(string level)
    {
        audioManager.musicIsPlaying = false;
        dataSaver.canRefreshDifficulty = false;
        StartCoroutine(fadeScene.GoToNextScene(level));
    }
}