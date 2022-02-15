using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    [Header("Note Elements")]
    public GameObject NotePanel;
    public TMP_Text NoteTitle;
    public TMP_Text NoteContext;

    public static UiManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void DisplayNote(Note note)
    {
        NotePanel.SetActive(true);
        NoteTitle.text = note.Title;
        NoteContext.text = note.Context + "\n \n  -" + note.Author;
    }
}
