using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    private Manager manager;
    private string levelToLoad;
    private FadeScene fadeScene;

    private void Start()
    {
        fadeScene = GameObject.FindObjectOfType<FadeScene>();
        manager = GameObject.FindObjectOfType<Manager>();
        levelToLoad = manager.currentLevel.ToString();
    }
    public void ExitGameOver()
    {
        StartCoroutine(fadeScene.GoToNextScene("LevelSelector"));
    }

    public void RestartGame()
    {
        StartCoroutine(fadeScene.GoToNextScene(levelToLoad));
    }
}