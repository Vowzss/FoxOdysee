using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevel : MonoBehaviour
{
    private DataSaver inventory;
    private ActualLevel actualLevel;
    private ShopManager shopManager;
    private KillBoss killBoss;
    private Manager manager;
    private GameOver gameOver;
    private TimerLevels timerLevels;

    public Canvas display;
    private Animator fadeAnimation;
    private GameObject fadeObject;

    public bool canAccesNextLevel = false;

    private void Awake()
    {
        shopManager = GameObject.FindObjectOfType<ShopManager>();
        inventory = GameObject.FindObjectOfType<DataSaver>();
        actualLevel = GameObject.FindObjectOfType<ActualLevel>();
        killBoss = GameObject.FindObjectOfType<KillBoss>();
        manager = GameObject.FindObjectOfType<Manager>();
        gameOver = GameObject.FindObjectOfType<GameOver>();
        timerLevels = GameObject.FindObjectOfType<TimerLevels>();

        display = GameObject.FindGameObjectWithTag("DoorInteracter").GetComponent<Canvas>();
        display.enabled = false;
        // gives errors mainly because fadeAnimation is in DontDestroyOnLoad which makes it I belive difficult for the code to get the reference ...
        fadeAnimation = GameObject.FindGameObjectWithTag("FadeAnimation").GetComponent<Animator>();

        fadeObject = GameObject.FindGameObjectWithTag("FadeAnimation");
        fadeObject.GetComponent<Image>().enabled = false;
    }

    private void Update()
    {
        SceneSwitcher();
    }

    private void SceneSwitcher()
    {
        if(canAccesNextLevel)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                inventory.SaveInCache();
                StartCoroutine(GoToNextScene(manager.nextLevelToUnlock.ToString()));
            }
        }
    }

    public IEnumerator GoToNextScene(string sceneToLoad)
    {
        timerLevels.needToRefreshed = true;
        timerLevels.timeRemaining = timerLevels.timerCached;

        shopManager.EraseShopData();
        inventory.EraseCurrent();

        if (sceneToLoad == "6")
            sceneToLoad = "GameWin";

        fadeObject.GetComponent<Image>().enabled = true;
        fadeAnimation.SetTrigger("in");

        yield return new WaitForSeconds(0.75f);
        SceneManager.LoadScene(sceneToLoad);
        yield return new WaitForSeconds(0.25f);

        actualLevel.refreshed = false;
        gameOver.needToRefresh = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && killBoss.isDead)
        {
            display.enabled = true;
            canAccesNextLevel = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            display.enabled = false;
            canAccesNextLevel = false;
        }
    }
}
