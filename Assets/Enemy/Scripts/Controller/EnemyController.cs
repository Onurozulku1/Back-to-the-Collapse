using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public EnemyMainFeatures Properties;
    [HideInInspector] public NavMeshAgent Agent;
    

    //FOV
    private float angleBetweenPlayer = 200f;
    private float pDistance = 200f;

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
        angleBetweenPlayer = Vector3.Angle(transform.forward, (Properties.Player.position - transform.position).normalized);
        pDistance = Vector3.Distance(Properties.Player.position, transform.position);
    }


    public bool EnemyFOV()
    {
        if (Physics.Raycast(transform.position, (Properties.Player.position - transform.position).normalized, out RaycastHit hit, Properties.SightRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                if (angleBetweenPlayer < (Properties.VisionAngle * 0.5f) || angleBetweenPlayer < (Properties.NoticeAngle * 0.5f) && pDistance < Properties.SightRange * 0.3f)
                {
                    return true;
                }
            }
        }
         
        return false;
    }

    public bool EnemyHear()
    {
        if (pDistance > Properties.HearRange || Properties.Player.GetComponent<PlayerMovement>().isCrouching || 
            !Properties.Player.GetComponent<PlayerMovement>().isCrouching && Mathf.RoundToInt(Properties.Player.GetComponent<Rigidbody>().velocity.magnitude) == 0)
        {
            return false;
        }
        return true;
    }
    
}
