using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindChasingState : EnemyChasingState
{
    public override void AwakeState(EnemyStateManager enemy)
    {
        base.AwakeState(enemy);
    }

    public override void EnterState(EnemyStateManager enemy)
    {
        base.EnterState(enemy);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        base.UpdateState(enemy);

        if (Vector3.Distance(enemy.transform.position, Properties.Player.position) <= Controller.Agent.stoppingDistance)
        {
            ExplodeEnemy();
        }
    }

    private void ExplodeEnemy()
    {
        
    }
}
