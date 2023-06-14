using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogueText;
    public Button[] responseButtons;
    private Queue<string> sentences;
    private Dialogue currentDialogue;
    private int currentSentenceIndex;

    void Awake()
    {
        sentences = new Queue<string>();
        ClearResponseButtons();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        currentSentenceIndex = 0;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;

        ClearResponseButtons();

        if (currentSentenceIndex < currentDialogue.responses.Length && currentDialogue.responses != null)
        {
            ShowResponseButtons();
        }

        currentSentenceIndex++;
    }

    public void OnResponseSelected(int responseIndex)
    {
        if (responseIndex < currentDialogue.responses.Length)
        {
            Dialogue nextDialogue = currentDialogue.responses[responseIndex].nextDialogue;
            StartDialogue(nextDialogue);
        }
    }

    void EndDialogue()
    {
        ClearResponseButtons();
    }

    void ShowResponseButtons()
    {
        for (int i = 0; i < currentDialogue.responses.Length; i++)
        {
            if (i < responseButtons.Length)
            {
                responseButtons[i].gameObject.SetActive(true);
                responseButtons[i].GetComponentInChildren<TMP_Text>().text = currentDialogue.responses[i].text;
                int responseIndex = i; // Capture the index in a local variable to avoid closure issues
                responseButtons[i].onClick.AddListener(() => OnResponseSelected(responseIndex));
            }
        }
    }

    void ClearResponseButtons()
    {
        foreach (Button button in responseButtons)
        {
            button.gameObject.SetActive(false);
            button.onClick.RemoveAllListeners();
        }
    }
}