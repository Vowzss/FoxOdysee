using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reset : MonoBehaviour
{
    private DataSaver dataSaver;

    [Header("Debug Attributes")]
    [SerializeField] private Button button;
    [SerializeField] private Text text;
    [SerializeField] private Image image;

    private void Awake()
    {
        dataSaver = GameObject.FindObjectOfType<DataSaver>();
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        text = GetComponentInChildren<Text>();
    }

    public void OnClickEvent()
    {
        dataSaver.WipeData();
        dataSaver.wasWiped = true;
    }
}
