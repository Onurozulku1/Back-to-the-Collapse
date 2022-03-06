﻿using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public EnemyProperties Properties;
    [HideInInspector] public NavMeshAgent Agent;
    

    //FOV
    private float PlayerAngle = 200f;
    private float PlayerDistance = 200f;

    //Combat
    [HideInInspector] public EnemyCombat enemyCombat = new EnemyCombat();
    public float AttackRange = 5;
    public float AttackRate = 1;

    private void OnValidate()
    {
        if (GetComponent<NavMeshAgent>() != null)
            Agent = GetComponent<NavMeshAgent>();

    }

    private void Awake()
    {
        Properties.Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        PlayerAngle = Vector3.Angle(transform.forward, (Properties.Player.position - transform.position).normalized);
        PlayerDistance = Vector3.Distance(Properties.Player.position, transform.position);
    }


    private float noticeTimer = 0;
    public bool EnemyFOV()
    {
        if (Physics.Raycast(transform.position, (Properties.Player.position - transform.position).normalized, out RaycastHit hit, Properties.SightRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                if (PlayerAngle < (Properties.VisionAngle * 0.5f) && PlayerDistance < Properties.SightRange)
                {
                    return true;
                }
                else if (PlayerAngle > (Properties.VisionAngle * 0.5f) && PlayerAngle < (Properties.NoticeAngle * 0.5f) && PlayerDistance < Properties.SightRange * 0.4f)
                {
                    if (noticeTimer < Properties.NoticeTime)
                    {
                        noticeTimer += Time.deltaTime;
                        return false;
                    }
                    else
                    {
                        noticeTimer = 0;
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public bool EnemyHear()
    {
        if (PlayerDistance > Properties.HearRange || Properties.Player.GetComponent<PlayerMovement>().isCrouching) 
        {
            return false;
        }

        if (!Properties.Player.GetComponent<PlayerMovement>().isCrouching && Mathf.RoundToInt(Properties.Player.GetComponent<Rigidbody>().velocity.magnitude) == 0)
        {
            return false;

        }

        if (Physics.Raycast(transform.position,(Properties.Player.position - transform.position).normalized, out RaycastHit hit))
        {
            if (!hit.collider.CompareTag("Player"))
            {
                return false;
            }
        }

        return true;
    }
    
    public void FaceToPlayer()
    {
        Vector3 lookRotation = (Properties.Player.position - transform.position).normalized;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookRotation, Vector3.up), 0.1f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Quaternion leftRayRotation = Quaternion.AngleAxis(-Properties.VisionAngle * 0.5f, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(Properties.VisionAngle * 0.5f, Vector3.up);
        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;
        Gizmos.DrawRay(transform.position, leftRayDirection * Properties.SightRange);
        Gizmos.DrawRay(transform.position, rightRayDirection * Properties.SightRange);
        Gizmos.DrawRay(transform.position, transform.forward * Properties.SightRange);
        Gizmos.DrawWireSphere(transform.position, Properties.HearRange);

        Gizmos.color = Color.magenta;
        Quaternion leftRayRotation2 = Quaternion.AngleAxis(-Properties.NoticeAngle * 0.5f, Vector3.up);
        Quaternion rightRayRotation2 = Quaternion.AngleAxis(Properties.NoticeAngle * 0.5f, Vector3.up);
        Vector3 leftRayDirection2 = leftRayRotation2 * transform.forward;
        Vector3 rightRayDirection2 = rightRayRotation2 * transform.forward;
        Gizmos.DrawRay(transform.position, 0.4f * Properties.SightRange * leftRayDirection2);
        Gizmos.DrawRay(transform.position, 0.4f * Properties.SightRange * rightRayDirection2);
    }

}
