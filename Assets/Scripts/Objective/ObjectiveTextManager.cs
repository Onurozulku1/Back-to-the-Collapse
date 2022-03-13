using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveTextManager : MonoBehaviour
{
    public TMP_Text denemeText;

    ObjectiveManager objectiveManager;
    private float missionPosY = 170;

    public GameObject[] missionObjects;
    public GameObject missionObject;

    public float spacing = 10;

    private void Awake()
    {
        objectiveManager = ObjectiveManager.instance;
        missionObjects = new GameObject[5];

        for (int i = 0; i < 5; i++)
        {
            missionObjects[i] = Instantiate(missionObject, transform);
            missionObjects[i].name = "MissionDisplayItem";
            missionObjects[i].SetActive(false);

        }
        AdjustObjectiveHUD();
    }
    private void Update()
    {
            Debug.Log(missionObjects[0].GetComponent<RectTransform>().rect.height);

    }
    private TMP_Text[] MissionTexts;
    private RectTransform rectTransform;
    public void AdjustObjectiveHUD()
    {
        for (int i = 0; i < objectiveManager.ActiveMissions.Count; i++)
        {
            
            missionObjects[i].SetActive(true);
            MissionTexts = missionObjects[i].GetComponentsInChildren<TMP_Text>();
            MissionTexts[0].text = objectiveManager.ActiveMissions[i].Title;
            MissionTexts[0].gameObject.SetActive(true);

            for (int j = 0; j < objectiveManager.ActiveMissions[i].objectives.Length; j++)
            {
                MissionTexts[j+1].gameObject.SetActive(true);
                MissionTexts[j+1].text = objectiveManager.ActiveMissions[i].objectives[j].Description;
            }
            for (int k = objectiveManager.ActiveMissions[i].objectives.Length + 1; k < 6; k++)
            {
                MissionTexts[k].gameObject.SetActive(false);
                MissionTexts[k].text = " ";

            }

            rectTransform = missionObjects[i].GetComponent<RectTransform>();
            rectTransform.localPosition = Vector2.up * missionPosY;
            missionPosY -= Mathf.Ceil(rectTransform.rect.height + spacing);

        }

    }
}
