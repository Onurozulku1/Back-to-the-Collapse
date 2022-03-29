using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveTextManager : MonoBehaviour
{
    public GameObject MissionObj;
    public GameObject[] MissionObjects;

    [Range(0, 100)] public float spacing = 10;
    [Range(0, 100)] public float PaddingX = 10;
    [Range(0, 100)] public float PaddingY = 10;

    private Vector3 MissionObjPos;

    ObjectiveManager objMgr;
    public static ObjectiveTextManager instance;
    private void Awake()
    {
        instance = this;

        objMgr = ObjectiveManager.instance;
        MissionObjects = new GameObject[5];

        for (int i = 0; i < MissionObjects.Length; i++)
        {
            MissionObjects[i] = Instantiate(MissionObj);
            MissionObjects[i].transform.SetParent(transform);
            MissionObjects[i].name = "Mission" + i + " Object";
        }
    }


    public void SetTexts()
    {
        for (int k = 0; k < objMgr.ActiveMissions.Count; k++)
        {
            MissionObjects[k].SetActive(true);
            TMP_Text[] textObjects = MissionObjects[k].GetComponentsInChildren<TMP_Text>();

            textObjects[0].text = objMgr.ActiveMissions[k].Title;

            for (int i = 0; i < objMgr.ActiveMissions[k].objectives.Length; i++)
            {
                textObjects[i + 1].text = objMgr.ActiveMissions[k].objectives[i].Description;
                textObjects[i + 1].gameObject.SetActive(true);
            }
            for (int j = objMgr.ActiveMissions[k].objectives.Length + 1; j < 6; j++)
            {
                textObjects[j].gameObject.SetActive(false);
            }
            
        }
        
    }

    public void AdjustMissionPos()
    {
        SetTexts();
        MissionObjPos = new Vector3(PaddingX, -PaddingY, 0); 
        for (int k = 0; k < objMgr.ActiveMissions.Count; k++)
        {
            RectTransform rectTransform = MissionObjects[k].GetComponent<RectTransform>();
            rectTransform.localPosition = MissionObjPos;
            MissionObjPos += (rectTransform.rect.height + spacing - 40) * Vector3.down;
        }

    }



}
