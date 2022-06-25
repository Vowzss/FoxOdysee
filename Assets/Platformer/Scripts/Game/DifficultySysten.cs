using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySysten : MonoBehaviour
{
    private DataSaver dataSaver;

    public GameObject dropDownObject;
    private Dropdown dropDown;

    private Image[] imageDropDown;
    private Text textDropDown;

    public string[] difficultyList;
    public string actualDifficulty;
    public int difficultyIndex;

    enum DIFFICULTY
    {
        EASY,
        MEDIUM,
        HARD
    }

    private void Awake()
    {
        dataSaver = GameObject.FindObjectOfType<DataSaver>();
        dropDown = GetComponent<Dropdown>();

        imageDropDown = GetComponentsInChildren<Image>();
        textDropDown = GetComponentInChildren<Text>();
    }

    private void Start()
    {
        dropDown.ClearOptions();
        List<string> difficultyOptions = new List<string>();

        difficultyList = new string[3];
        difficultyList[0] = DIFFICULTY.EASY.ToString();
        difficultyList[1] = DIFFICULTY.MEDIUM.ToString();
        difficultyList[2] = DIFFICULTY.HARD.ToString();

        difficultyIndex = 0;
        actualDifficulty = difficultyList[difficultyIndex];
        dataSaver.needToRefresh = true;

        for (int i = 0; i < difficultyList.Length; i++)
            difficultyOptions.Add(difficultyList[i]);

        dropDown.AddOptions(difficultyOptions);
        dropDown.value = difficultyIndex;
        dropDown.RefreshShownValue();

        dropDown.onValueChanged.AddListener
        (
            delegate 
            { 
                changeValue(dropDown); 
            }
        );
    }

    private void Update()
    {
        actualDifficulty = difficultyList[difficultyIndex];

        if (dataSaver.needToRefresh && dataSaver.canRefreshDifficulty)
        {
            dataSaver.currentDifficulty = actualDifficulty;
            dataSaver.needToRefresh = false;
        }

        if (dataSaver.wasWiped)
        {
            dataSaver.canRefreshDifficulty = true;
            dataSaver.wasWiped = false;
            difficultyIndex = 0;
            dropDown.value = difficultyIndex;

            for (int i = 0; i < imageDropDown.Length; i++)
                imageDropDown[i].enabled = true;
            textDropDown.enabled = true;
        }

        else if (!dataSaver.canRefreshDifficulty)
        {
            for (int i = 0; i < imageDropDown.Length; i++)
                imageDropDown[i].enabled = false;
            textDropDown.enabled = false;
        }
    }

    private void changeValue(Dropdown dropdown)
    {
        Debug.Log("selected: " + dropdown.value);
        difficultyIndex = dropdown.value;
        dataSaver.needToRefresh = true;
    }
}
