using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    [SerializeField] GameObject DialoguePanel;
    [SerializeField] TMP_Text NameText;
    [SerializeField] TMP_Text SentenceText;

    private Queue<string> sentences;
    private bool DisplayingDialogue = false;

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
        DisplayingDialogue = true;
        _dialogue = dialogue;
        sentences.Clear();

        foreach (string sentence in dialogue.Sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (!DisplayingDialogue)
            return;

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        SentenceText.text = sentence;
        NameText.text = _dialogue.Name;
        
    }

    void EndDialogue()
    {
        DialoguePanel.SetActive(false);
        SentenceText.text = "";
        NameText.text = "";

    }
}
