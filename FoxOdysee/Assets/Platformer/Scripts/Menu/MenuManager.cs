using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private FadeScene fadeScene;

    private void Awake()
    {
        fadeScene = GameObject.FindObjectOfType<FadeScene>();
    }
    private void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartIntro() 
    {
        StartCoroutine(fadeScene.GoToNextScene("Introduction"));
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
