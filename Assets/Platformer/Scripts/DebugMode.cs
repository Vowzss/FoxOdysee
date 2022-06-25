using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugMode : MonoBehaviour
{
    private Manager manager;

    [Header("Debug State")]
    public bool debugMode;

    [Header("Debug Attributes")]
    [SerializeField] private Button button;
    [SerializeField] private Text text;
    [SerializeField] private Image image;

    private void Awake()
    {
        manager = GameObject.FindObjectOfType<Manager>();
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        text = GetComponentInChildren<Text>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Delete))
        {
            debugMode = !debugMode;
        }

        if(debugMode)
        {
            button.enabled = true;
            image.enabled = true;
            text.enabled = true;
        }
        else
        {
            button.enabled = false;
            image.enabled = false;
            text.enabled = false;
        }
    }

    public void OnClickEvent()
    {
        manager.UnlockLevel();
    }
}
