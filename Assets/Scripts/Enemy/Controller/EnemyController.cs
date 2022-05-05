using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public EnemyProperties enemyProperties;
    [HideInInspector] public NavMeshAgent Agent;

    public bool isPatrolling = false;

    private NoticeBar hBar;
    //FOV
    private float PlayerAngle = 200f;
    private float PlayerDistance = 200f;

    //Combat
    [HideInInspector] public EnemyCombat enemyCombat = new EnemyCombat();
    [Header("Combat Properties")]
    public float AttackRange = 3;
    public float AttackRate = 1;

    [Header("Noticing Properties")]
    public float noticeTimer = 0;
    public float resetNoticeTimer = 0;
    public bool EnemyHeard = false;

    [Header("Notify Properties")]
    public float NotifyEnemyRadius = 8;
    public LayerMask enemyMask;
    [HideInInspector] public NavMeshHit followHit;

    public EnemyStateManager[] enemies;

    private void Awake()
    {
        enemyProperties.Player = GameObject.FindGameObjectWithTag("Player").transform;
        thisStateManager = GetComponent<EnemyStateManager>();

        if (GetComponent<NavMeshAgent>() != null)
            Agent = GetComponent<NavMeshAgent>();

        hBar = GetComponentInChildren<NoticeBar>();

        enemies = FindObjectsOfType<EnemyStateManager>();

    }

    private void Update()
    {
        PlayerAngle = Vector3.Angle(transform.forward, (enemyProperties.Player.position - transform.position).normalized);
        PlayerDistance = Vector3.Distance(enemyProperties.Player.position, transform.position);

        EnemyHearing();

        if (thisStateManager.currentState == thisStateManager.AttackState || thisStateManager.currentState == thisStateManager.ChasingState)
        {
            if (hBar.gameObject.activeSelf)
            {
                noticeTimer = 0;
                hBar.gameObject.SetActive(false);
            }
        }
    }

    private bool Noticing()
    {
        resetNoticeTimer = 0;
        if (noticeTimer < enemyProperties.NoticeTime)
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

    private float visionRange;
    public bool EnemyFOV()
    {
        if (Physics.Raycast(transform.position, (enemyProperties.Player.position - transform.position).normalized, out RaycastHit hit, enemyProperties.SightRange))
        {
            if (hit.collider.CompareTag("Player"))
            {

                if (thisStateManager.currentState == thisStateManager.ChasingState)
                    visionRange = enemyProperties.SightRange * 2f;
                else
                    visionRange = enemyProperties.SightRange;


                if (PlayerAngle < (enemyProperties.VisionAngle) && PlayerDistance < visionRange)
                    return true;

                else if (PlayerAngle < (enemyProperties.VisionAngle * 0.8f) && PlayerDistance > enemyProperties.SightRange && PlayerDistance < enemyProperties.SightRange * 1.2f)
                    return Noticing();

                else if (PlayerAngle > (enemyProperties.VisionAngle) && PlayerAngle < (enemyProperties.NoticeAngle) && PlayerDistance < enemyProperties.SightRange * 0.4f)
                    return Noticing();
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

    public void CheckPlayer()
    {
        if (EnemyFOV())
        {
            thisStateManager.SwitchState(thisStateManager.ChasingState);
            return;
        }
        if (EnemyHeard)
        {
            FaceToHeardPoint();
        }

    }
    public void FaceToHeardPoint()
    {
        Vector3 lookRotation = (enemyProperties.Player.position - transform.position).normalized;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookRotation, Vector3.up), 0.05f);
        StartCoroutine(HeardPlayer());
    }
    public void NotifyPartners(EnemyStateManager enemy)
    {
        if (Vector3.Distance(transform.position, enemy.transform.position) < 50)
        {
            if (enemy.gameObject == gameObject)
                return;

            if (enemy.currentState == enemy.ChasingState || enemy.currentState == enemy.FollowingState || enemy.currentState == enemy.AttackState)
                return;

            if (thisStateManager.FollowingState.leader != null)
                return;


            enemy.FollowingState.leader = thisStateManager;
            enemy.currentState = enemy.FollowingState;
        }
    }

    #region Hearing
    private IEnumerator HeardPlayer()
    {
        yield return new WaitForSeconds(enemyProperties.SearchingTime);
        EnemyHeard = false;
    }
    public void EnemyHearing()
    {
        if (PlayerDistance > enemyProperties.HearRange || enemyProperties.Player.GetComponent<PlayerMovement>().isCrouching)
        {
            return;
        }

        if (!enemyProperties.Player.GetComponent<PlayerMovement>().isCrouching && Mathf.RoundToInt(enemyProperties.Player.GetComponent<CharacterController>().velocity.magnitude) == 0)
        {
            return;

        }

        if (Physics.Raycast(transform.position, (enemyProperties.Player.position - transform.position).normalized, out RaycastHit hit))
        {
            if (!hit.collider.CompareTag("Player"))
            {
                return;
            }
        }

        EnemyHeard = true;
    }

    #endregion

    private EnemyStateManager thisStateManager;
    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Quaternion leftRayRotation = Quaternion.AngleAxis(-enemyProperties.VisionAngle, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(enemyProperties.VisionAngle, Vector3.up);
        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;
        Gizmos.DrawRay(transform.position, leftRayDirection * enemyProperties.SightRange);
        Gizmos.DrawRay(transform.position, rightRayDirection * enemyProperties.SightRange);
        Gizmos.DrawRay(transform.position, transform.forward * enemyProperties.SightRange);
        Gizmos.DrawWireSphere(transform.position, enemyProperties.HearRange);

        Gizmos.color = Color.magenta;
        Quaternion leftRayRotation2 = Quaternion.AngleAxis(-enemyProperties.NoticeAngle, Vector3.up);
        Quaternion rightRayRotation2 = Quaternion.AngleAxis(enemyProperties.NoticeAngle, Vector3.up);
        Vector3 leftRayDirection2 = leftRayRotation2 * transform.forward;
        Vector3 rightRayDirection2 = rightRayRotation2 * transform.forward;
        Gizmos.DrawRay(transform.position, 0.4f * enemyProperties.SightRange * leftRayDirection2);
        Gizmos.DrawRay(transform.position, 0.4f * enemyProperties.SightRange * rightRayDirection2);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, NotifyEnemyRadius);


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
