using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveTextManager : MonoBehaviour
{
    public GameObject MissionObj;
    public GameObject[] MissionObjects;

    [Range(0, 100)] public float spacing = 10;

    private Vector3 MissionObjPos;

    ObjectiveManager objMgr;
    private void Awake()
    {
        objMgr = ObjectiveManager.instance;
        MissionObjects = new GameObject[5];

        for (int i = 0; i < MissionObjects.Length; i++)
        {
            MissionObjects[i] = Instantiate(MissionObj);
            MissionObjects[i].transform.SetParent(transform);
            MissionObjects[i].name = "Mission" + i + " Object";
        }
        SetTexts();
    }


    public void SetTexts()
    {
        for (int k = 0; k < objMgr.ActiveMissions.Count; k++)
        {
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
    private void Update()
    {
        AdjustMissionPos();
    }
    public void AdjustMissionPos()
    {
        MissionObjPos = Vector3.zero;
        for (int k = 0; k < objMgr.ActiveMissions.Count; k++)
        {
            RectTransform rectTransform = MissionObjects[k].GetComponent<RectTransform>();
            rectTransform.localPosition = MissionObjPos;
            MissionObjPos += (rectTransform.rect.height + spacing - 40) * Vector3.down;
        }

    }



}
