using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindIdleState : EnemyIdleState
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
        if (Vector3.Distance(Properties.Player.position, enemy.transform.position) <= Properties.HearRange)
        {
            enemy.SwitchState(enemy.ChasingState);
        }
    }
}
