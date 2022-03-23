using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel;
    private TMP_Text tutorialText;


    [TextArea] public string[] TutorialTexts;

    public bool TutorialDisplaying = false;

    public static TutorialManager instance;
    private void Awake()
    {
        instance = this;
        tutorialPanel.SetActive(false);
    }

    private void OnValidate()
    {
        tutorialText = tutorialPanel.GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        if (TutorialTexts == null)
            return;

        StartCoroutine(DisplayTutorial());

    }

    private IEnumerator DisplayTutorial()
    {
        yield return new WaitForSeconds(3);
        
        tutorialText.text = TutorialTexts[0];
        tutorialPanel.SetActive(true);
        TutorialDisplaying = true;

    }

    int tutorialIndex = 1;
    public void NextTutorial()
    {
        if (TutorialTexts == null)
            return;
        
        if (tutorialIndex < TutorialTexts.Length)
        {
            tutorialText.text = TutorialTexts[tutorialIndex];
            tutorialIndex++;
        }
        else
        {
            tutorialPanel.SetActive(false);
            TutorialDisplaying = false;
        }


    }
}
