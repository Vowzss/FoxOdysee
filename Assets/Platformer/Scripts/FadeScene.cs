using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeScene : MonoBehaviour
{
    // this script is only made for scene that don't have levelSelector ref such as Menu, Introduction and GameOver
    [SerializeField] private Animator fadeAnimation;
    [SerializeField] private GameObject fadeObject;

    private void Awake()
    {
        fadeObject = GameObject.FindGameObjectWithTag("FadeAnimation");
        // gives errors mainly because fadeAnimation is in DontDestroyOnLoad which makes it I belive difficult for the code to get the reference ...
        fadeObject.GetComponent<Image>().enabled = false;

        fadeAnimation = GameObject.FindGameObjectWithTag("FadeAnimation").GetComponent<Animator>();
    }

    public IEnumerator GoToNextScene(string sceneToLoad)
    {
        fadeObject.GetComponent<Image>().enabled = true;
        fadeAnimation.SetTrigger("in");
        //Data.instance.SaveData();
        yield return new WaitForSeconds(0.75f);
        SceneManager.LoadScene(sceneToLoad);
        yield return new WaitForSeconds(0.25f);
    }
}
