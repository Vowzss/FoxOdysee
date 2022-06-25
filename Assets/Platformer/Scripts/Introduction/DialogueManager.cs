using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour 
{
	private FadeScene fadeScene;
	public Text nameText;
	public Text dialogueText;

	public bool dialogEnded = false;
	public bool dialogStarted = false;

	public Animator animator;

	private Queue<string> sentences;

	public Button playButton;
	public Button restartButton;
	public GameObject dialogBox;

	private void Start () 
	{
		fadeScene = GameObject.FindObjectOfType<FadeScene>();
		sentences = new Queue<string>();
	}

	public void StartDialogue (Dialogue dialogue)
	{
		dialogStarted = true;
		animator.SetBool("IsOpen", true);
		nameText.text = dialogue.textName;
		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
			sentences.Enqueue(sentence);

		DisplayNextSentence();
	}

	public void DisplayNextSentence ()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	private IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return new WaitForSeconds(0.02f);
		}
	}

	private void EndDialogue()
	{
		dialogEnded = true;
	}

	public void Update()
	{
		if (dialogEnded)
		{
			playButton.gameObject.SetActive(true);
			restartButton.gameObject.SetActive(true);
		}
		else
		{
			if(dialogStarted)
				dialogBox.gameObject.SetActive(true);
			else
				dialogBox.gameObject.SetActive(false);

			playButton.gameObject.SetActive(false);
			restartButton.gameObject.SetActive(false);
		}
	}

	public void PlayGame()
    {
		StartCoroutine(fadeScene.GoToNextScene("LevelSelector"));
	}
}