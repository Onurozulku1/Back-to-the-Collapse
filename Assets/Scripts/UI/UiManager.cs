using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{

    public TMP_Text NotificationText;

    public GameObject staticTabs;

    [Header("Hud Elements")]
    public GameObject inventoryPanel;
    public HudTab[] tabs;
    public TMP_Text[] TabTexts;
    
    [Header("PauseMenu Elements")]
    public GameObject pauseMenu;
    

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


        //CREATE TAB QUEUE
        TabQueue = new Queue<int>();
        for (int i = 0; i < hudTabCount; i++)
        {
            TabQueue.Enqueue(i);
        }

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

    private Queue<int> TabQueue;
    public int hudTabCount = 3;
    public void ChangeHudTab(int point)
    {

        if (point > 0)
        {
            TabQueue.Enqueue(TabQueue.Dequeue());
        }
        else
        {
            for (int j = 0; j < TabQueue.Count - 1; j++)
            {
                TabQueue.Enqueue(TabQueue.Dequeue());
            }
        }

        for (int i = 0; i < TabQueue.Count; i++)
        {
            tabs[i].HudPanel.SetActive(false);
            TabQueue.Enqueue(TabQueue.Dequeue());
            TabTexts[i].text = tabs[TabQueue.Peek()].Title;

        }
       
        tabs[TabQueue.Peek()].HudPanel.SetActive(true);


    }

   


}

[System.Serializable]
public struct HudTab
{
    public string Title;
    public GameObject HudPanel;
}
