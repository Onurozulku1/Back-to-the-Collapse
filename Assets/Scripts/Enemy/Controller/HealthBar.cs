using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image noticeBar;
    private EnemyController ec;
    private void Awake()
    {
        ec = GetComponentInParent<EnemyController>();
        noticeBar = GetComponentInChildren<Image>();
    }

    public void SetBar()
    {
        transform.LookAt(Camera.main.transform);
        noticeBar.fillAmount = ec.noticeTimer / ec.Properties.NoticeTime;
    }

    private void Update()
    {
        SetBar();
    }
}
