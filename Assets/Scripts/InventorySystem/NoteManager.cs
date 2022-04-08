using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoteManager : MonoBehaviour
{
    public List<Note> Notes = new List<Note>();
    public GameObject JournalsParent;

    public GameObject NotePanel;

    public TMP_Text NoteTitle;
    public TMP_Text NoteContext;

    public TMP_Text JournalTitle;
    public TMP_Text JournalContext;

    public static NoteManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;

        if (JournalsParent != null)
            journalTexts = JournalsParent.GetComponentsInChildren<TMP_Text>();


        if (journalTexts != null)
        {
            foreach (var item in journalTexts)
            {
                item.gameObject.SetActive(false);
            }
        }
    }

    private TMP_Text[] journalTexts;
    

    public void AddNote(Note note)
    {
        Notes.Add(note);
        foreach (var item in note.ItemsWithNote)
        {
            if (item != null)
            {
                InventoryManager.instance.AddItem(item);
            }
        }
        NotePanel.SetActive(true);
        DisplayNote(note, NoteTitle, NoteContext);
        SetJournals();
    }

    public void SetJournals()
    {
        for (int i = 0; i < Notes.Count; i++)
        {
            journalTexts[i].text = Notes[i].Title;
            journalTexts[i].gameObject.SetActive(true);
        }
        DisplayNote(Notes[0], JournalTitle, JournalContext);

    }

    public void DisplayNote(Note note, TMP_Text titleText, TMP_Text contextText)
    {
        titleText.text = note.Title;
        contextText.text = note.Context + "\n \n  -" + note.Author;

    }

}
