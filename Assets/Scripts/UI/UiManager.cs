using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{

    public TMP_Text NotificationText;

    public GameObject staticTabs;

    [Header("Hud Elements")]
    public HudTab[] tabs;
    public TMP_Text[] TabTexts;
    
    [Header("PauseMenu Elements")]
    public GameObject pauseMenu;
    
    [Header("Note Elements")]
    public GameObject NotePanel;
    public TMP_Text NoteTitle;
    public TMP_Text NoteContext;


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


    public Transform UiPanel;
    private GameObject[] UiElements;
    public bool DisplayingWindows()
    {
        foreach (GameObject element in UiElements)
        {
            if (element.activeSelf)
                return true;
        }

        return false;
    }


    public void CloseWindows()
    {
        foreach (GameObject element in UiElements)
        {
            element.SetActive(false);
        }
    }


    public void DisplayNotificationText(string context)
    {
        StopCoroutine(HideNotificationText());
        NotificationText.gameObject.SetActive(true);
        NotificationText.text = context;
        StartCoroutine(HideNotificationText());
    }
    private IEnumerator HideNotificationText()
    {
        yield return new WaitForSeconds(3);
        NotificationText.gameObject.SetActive(false);

    }


    private int[] TabIndex = new int[] { 2, 0, 1 };
    public void ChangeHudTab(int point)
    {
        for (int i = 0; i < TabIndex.Length; i++)
        {
            tabs[i].HudPanel.SetActive(false);
            TabIndex[i] -= point;
            if (TabIndex[i] < 0)
            {
                TabIndex[i] = 2;
            }
            else if (TabIndex[i] > 2)
            {
                TabIndex[i] = 0;
            }

            TabTexts[i].text = tabs[TabIndex[i]].Title;

        }
            tabs[TabIndex[1]].HudPanel.SetActive(true);

    }


}

[System.Serializable]
public struct HudTab
{
    public string Title;
    public GameObject HudPanel;
}
