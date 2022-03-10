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

    private void OnValidate()
    {
        if (UiPanel != null)
        {
            UiElements = new GameObject[UiPanel.childCount];
            for (int i = 0; i < UiPanel.childCount; i++)
            {
                UiElements[i] = UiPanel.GetChild(i).gameObject;
            }
        }
    }

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

    public Transform UiPanel;
    private GameObject[] UiElements;
    public bool DisplayingTabs()
    {
        foreach (GameObject element in UiElements)
        {
            if (element.activeSelf)
                return true;
        }

        return false;
    }

    public void CloseTabs()
    {
        foreach (GameObject element in UiElements)
        {
            element.SetActive(false);
        }
    }
}
