using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToSelector : MonoBehaviour
{
    private FadeScene fadeScene;

    private void Awake()
    {
        fadeScene = GameObject.FindObjectOfType<FadeScene>();
    }
    public void OnClickButton()
    {
        StartCoroutine(fadeScene.GoToNextScene("LevelSelector"));
    }
}
