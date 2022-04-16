﻿using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public EnemyProperties Properties;
    [HideInInspector] public NavMeshAgent Agent;

    public bool isPatrolling = false;

    private HealthBar hBar;
    //FOV
    private float PlayerAngle = 200f;
    private float PlayerDistance = 200f;

    //Combat
    [Header("Combat Properties")]
    [HideInInspector] public EnemyCombat enemyCombat = new EnemyCombat();
    public float AttackRange = 3;
    public float AttackRate = 1;


    private void Awake()
    {
        Properties.Player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyStateManager = GetComponent<EnemyStateManager>();

        if (GetComponent<NavMeshAgent>() != null)
            Agent = GetComponent<NavMeshAgent>();

        hBar = GetComponentInChildren<HealthBar>();
    }

    private void Update()
    {
        PlayerAngle = Vector3.Angle(transform.forward, (Properties.Player.position - transform.position).normalized);
        PlayerDistance = Vector3.Distance(Properties.Player.position, transform.position);

        EnemyHearing();

        if (enemyStateManager.currentState == enemyStateManager.AttackState || enemyStateManager.currentState == enemyStateManager.ChasingState)
        {
            if (hBar.gameObject.activeSelf)
            {
                noticeTimer = 0;
                hBar.gameObject.SetActive(false);
            }
        }
    }


    public float noticeTimer = 0;
    public float resetNoticeTimer = 0;
    private bool Noticing()
    {
        resetNoticeTimer = 0;
        if (noticeTimer < Properties.NoticeTime)
        {
            hBar.gameObject.SetActive(true);
            noticeTimer += Time.deltaTime;
            return false;
        }
        else
        {
            hBar.gameObject.SetActive(false);
            noticeTimer = 0;
            return true;
        }
    }

    public bool EnemyFOV()
    {
        if (Physics.Raycast(transform.position, (Properties.Player.position - transform.position).normalized, out RaycastHit hit, Properties.SightRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                if (PlayerAngle < (Properties.VisionAngle) && PlayerDistance < Properties.SightRange)
                {
                    return true;
                }
                else if (PlayerAngle < (Properties.VisionAngle * 0.8f) && PlayerDistance > Properties.SightRange && PlayerDistance < Properties.SightRange * 1.2f)
                {
                    return Noticing();
                }
                else if (PlayerAngle > (Properties.VisionAngle) && PlayerAngle < (Properties.NoticeAngle) && PlayerDistance < Properties.SightRange * 0.4f)
                {
                    return Noticing();
                }
            }
        }

        if (noticeTimer > 0)
        {
            resetNoticeTimer += Time.deltaTime;
            if (resetNoticeTimer >= 4)
            {
                resetNoticeTimer = 0;
                noticeTimer = 0;
                hBar.gameObject.SetActive(false);
            }
        }
        
        return false;
    }

    

    #region Hearing

    public bool EnemyHeard = false;
    private IEnumerator HeardPlayer()
    {
        yield return new WaitForSeconds(Properties.SearchingTime);
        EnemyHeard = false;
    }
    public void EnemyHearing()
    {
        if (PlayerDistance > Properties.HearRange || Properties.Player.GetComponent<PlayerMovement>().isCrouching) 
        {
            return;
        }

        if (!Properties.Player.GetComponent<PlayerMovement>().isCrouching && Mathf.RoundToInt(Properties.Player.GetComponent<CharacterController>().velocity.magnitude) == 0)
        {
            return;

        }

        if (Physics.Raycast(transform.position,(Properties.Player.position - transform.position).normalized, out RaycastHit hit))
        {
            if (!hit.collider.CompareTag("Player"))
            {
                return;
            }
        }

        EnemyHeard = true;
    }

    #endregion

    private EnemyStateManager enemyStateManager;
    public void CheckPlayer()
    {
        if (EnemyFOV())
        {
            enemyStateManager.SwitchState(enemyStateManager.ChasingState);
            return;
        }

        if (EnemyHeard)
        {
            FaceToHeardPoint();
        }
    }
    public void FaceToHeardPoint()
    {
        Vector3 lookRotation = (Properties.Player.position - transform.position).normalized;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookRotation, Vector3.up), 0.05f);
        StartCoroutine(HeardPlayer());
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Quaternion leftRayRotation = Quaternion.AngleAxis(-Properties.VisionAngle, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(Properties.VisionAngle, Vector3.up);
        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;
        Gizmos.DrawRay(transform.position, leftRayDirection * Properties.SightRange);
        Gizmos.DrawRay(transform.position, rightRayDirection * Properties.SightRange);
        Gizmos.DrawRay(transform.position, transform.forward * Properties.SightRange);
        Gizmos.DrawWireSphere(transform.position, Properties.HearRange);

        Gizmos.color = Color.magenta;
        Quaternion leftRayRotation2 = Quaternion.AngleAxis(-Properties.NoticeAngle, Vector3.up);
        Quaternion rightRayRotation2 = Quaternion.AngleAxis(Properties.NoticeAngle, Vector3.up);
        Vector3 leftRayDirection2 = leftRayRotation2 * transform.forward;
        Vector3 rightRayDirection2 = rightRayRotation2 * transform.forward;
        Gizmos.DrawRay(transform.position, 0.4f * Properties.SightRange * leftRayDirection2);
        Gizmos.DrawRay(transform.position, 0.4f * Properties.SightRange * rightRayDirection2);
    }


    private void OnEnable()
    {
        GameManager.PauseGameAction += (bool isPaused) => enabled = !isPaused;
        GameManager.PauseGameAction += (bool isPaused) => GetComponent<EnemyStateManager>().enabled = !isPaused;
        GameManager.PauseGameAction += (bool isPaused) => GetComponent<NavMeshAgent>().enabled = !isPaused;
    }
    private void OnDisable()
    {
        GameManager.PauseGameAction -= (bool isPaused) => enabled = !isPaused;
        GameManager.PauseGameAction -= (bool isPaused) => GetComponent<EnemyStateManager>().enabled = !isPaused;
        GameManager.PauseGameAction -= (bool isPaused) => GetComponent<NavMeshAgent>().enabled = !isPaused;

    }

}
