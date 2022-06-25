using UnityEngine;
using UnityEngine.UI;

public class DataSaver : MonoBehaviour
{
    private ScoreManager scoreManager;
    private Manager manager;

    /*[HideInInspector]*/ public bool wasWiped = false;

    [HideInInspector] public bool needToRefresh = false;
    /*[HideInInspector]*/ public bool canRefreshDifficulty = true;

    public string currentDifficulty;
    public int playerDeath = 0;
    public bool crownBought = false;
    public bool cloverBought = false;

    [Header("Actual Inventory Data")]
    public int currCoinsStacked = 0;
    public int currTopazsStacked = 0;
    public int currPlayerScore = 0;

    [Header("Cached Inventory Data")]
    public int cachedCoinsStacked = 0;
    public int cachedTopazsStacked = 0;
    public int cachedPlayerScore = 0;

    [Header("Transit Inventory Data")]
    public int transitCoinsStacked = 0;
    public int transitTopazsStacked = 0;
    public int transitPlayerScore = 0;

    private GameObject coinDisplay;
    private GameObject topazDisplay;
    private GameObject scoreDisplay;

    private Text textCoinsDisplay;
    private Text textTopazsDisplay;
    private Text textScore;

    private void Awake()
    {
        manager = GameObject.FindObjectOfType<Manager>();
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        coinDisplay = GameObject.FindGameObjectWithTag("CoinCounter");
        textCoinsDisplay = coinDisplay.GetComponentInChildren<Text>();
        topazDisplay = GameObject.FindGameObjectWithTag("TopazCounter");
        textTopazsDisplay = topazDisplay.GetComponentInChildren<Text>();
        scoreDisplay = GameObject.FindGameObjectWithTag("ScoreCounter");
        textScore = scoreDisplay.GetComponentInChildren<Text>();
    }

    public void EraseCurrent()
    {
        currCoinsStacked = 0;
        currTopazsStacked = 0;
        currPlayerScore = 0;
    }

    public void WipeData()
    {
        cachedCoinsStacked = 0;
        cachedTopazsStacked = 0;
        cachedPlayerScore = 0;

        currentDifficulty = "EASY";

        playerDeath = 0;

        crownBought = false;
        cloverBought = false;

        manager.currentLevel = 1;

        manager.WipeData();
    }

    public void UpdateInventory()
    {
        transitCoinsStacked = currCoinsStacked + cachedCoinsStacked;
        transitTopazsStacked = currTopazsStacked + cachedTopazsStacked;
        transitPlayerScore = currPlayerScore + cachedPlayerScore;
    }

    public void SaveInCache()
    {
        cachedCoinsStacked = transitCoinsStacked;
        cachedTopazsStacked = transitTopazsStacked;
        cachedPlayerScore = transitPlayerScore;
    }

    private void Update()
    {
        UpdateInventory();
        UpdateText();
        scoreManager.ScoreDisplay();
    }

    public void RemoveCoin(int numCoins)
    {
        var delta = currCoinsStacked - numCoins;

        if (currCoinsStacked > numCoins)
            currCoinsStacked -= numCoins;
        else
        {
            currCoinsStacked -= currCoinsStacked;
            cachedCoinsStacked += delta;
        }
    }

    public void AddCoin(int numCoins)
    {
        currCoinsStacked += numCoins;
    }

    public void AddTopaz(int numTopaz)
    {
        currTopazsStacked += numTopaz;
    }

    public void AddScore(int score)
    {
        currPlayerScore += score;
    }

    public void UpdateText()
    {
        textCoinsDisplay.text = (currCoinsStacked + cachedCoinsStacked).ToString();
        textTopazsDisplay.text = (currTopazsStacked + cachedTopazsStacked).ToString();
        textScore.text = (currPlayerScore + cachedPlayerScore).ToString();
    }
}
