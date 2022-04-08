using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableNotification : MonoBehaviour
{
    public float EffectRadius = 10f;
    public bool isLaunched = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6 && isLaunched)
        {
            NoticeEnemies();
            isLaunched = false;
        }
    }

    private Collider[] enemies;
    private void NoticeEnemies()
    {
        enemies = Physics.OverlapSphere(transform.position, EffectRadius);
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].gameObject.CompareTag("Enemy"))
            {
                EnemyStateManager esm = enemies[i].GetComponent<EnemyStateManager>();
                if (esm.currentState == esm.AttackState || esm.currentState == esm.ChasingState)
                    return;
                esm.SearchingState.target = transform;

                esm.SwitchState(esm.SearchingState);
            }
        }
    }
}
