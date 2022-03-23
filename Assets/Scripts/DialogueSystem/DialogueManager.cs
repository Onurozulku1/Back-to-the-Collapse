using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    [SerializeField] GameObject DialoguePanel;
    [SerializeField] TMP_Text NameText;
    [SerializeField] TMP_Text SentenceText;

    private Queue<string> sentences;

    public static DialogueManager instance;

    private void Awake()
    {
        DialoguePanel.SetActive(false);
    
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        sentences = new Queue<string>();
    }

    private Dialogue _dialogue;
    public void StartDialogue(Dialogue dialogue)
    {
        _dialogue = dialogue;
        sentences.Clear();

        foreach (string sentence in dialogue.Sentences)
        {
            sentences.Enqueue(sentence);
        }
        DialoguePanel.SetActive(true);
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (!DialoguePanel.activeSelf)
            return;

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        SentenceText.text = sentence;

        if (_dialogue.Name == null)
        {
            NameText.text = " ";
            return;

        }

        NameText.text = _dialogue.Name;
        
    }

    void EndDialogue()
    {
        DialoguePanel.SetActive(false);
        SentenceText.text = "";
        NameText.text = "";

    }
}
