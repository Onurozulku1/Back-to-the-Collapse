using UnityEngine;

public class EnemyCombat 
{
    private readonly float attackRate;
    private readonly float attackRange;
    public Vector3 availableArea;

    private Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public bool PlayerInRange(EnemyStateManager enemy, Transform player)
    {
        if (Vector3.Distance(enemy.transform.position, player.position) < enemy.Controller.AttackRange && 
            Physics.Raycast(enemy.transform.position,(player.position - enemy.transform.position).normalized,out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Player"))
                return true;

        }
        return false;
    }

}
