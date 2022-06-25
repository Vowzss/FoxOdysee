using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DontDestroy : MonoBehaviour
{
    private Manager manager;
    private ActualLevel actualLevel;
    private GameObject UI;
    public GameObject currentSceneUI;

    private ShopManager shopManager;

    public GameObject[] dontDestroyObjects;
    private void Awake()
    {
        actualLevel = GameObject.FindObjectOfType<ActualLevel>();
        shopManager = GameObject.FindObjectOfType<ShopManager>();
        manager = GameObject.FindObjectOfType<Manager>();
        UI = GameObject.FindGameObjectWithTag("HUD");

        foreach (var item in dontDestroyObjects)
        {
            DontDestroyOnLoad(item);
        }
    }

    private void Update()
    {
        currentSceneUI = GameObject.FindGameObjectWithTag("CurrentSceneUI");

        if (manager.currentLevel == 0)
        {
            UI.GetComponent<GraphicRaycaster>().enabled = false;
            UI.GetComponent<Canvas>().enabled = false;
        }
        else if (shopManager.isShopOppenned)
        {
            UI.GetComponent<GraphicRaycaster>().enabled = true;
            currentSceneUI.GetComponent<GraphicRaycaster>().enabled = false;
        }
        else if (!shopManager.isShopOppenned && manager.currentLevel != 0)
        {
            UI.GetComponent<Canvas>().enabled = true;
            currentSceneUI.GetComponent<GraphicRaycaster>().enabled = true;
            UI.GetComponent<GraphicRaycaster>().enabled = false;
        }
    }
}
