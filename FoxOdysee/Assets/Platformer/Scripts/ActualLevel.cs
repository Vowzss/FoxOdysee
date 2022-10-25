using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ActualLevel : MonoBehaviour
{
    private bool parsed = false;
    private Scene currentScene;
    [SerializeField] public bool refreshed = false;

    [Header("Data Tracker")]
    [SerializeField] public int previousLevel;
    [SerializeField] public int curentLevel;
    [SerializeField] public int nextLevel;

    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
        parsed = int.TryParse(currentScene.name, out curentLevel);
        nextLevel = curentLevel + 1;
        previousLevel = curentLevel - 1;
    }

    private void Update()
    {
        if (parsed)
            return;
        else
            curentLevel = 0;

        if (previousLevel <= 0)
            previousLevel = 1;
    }
}