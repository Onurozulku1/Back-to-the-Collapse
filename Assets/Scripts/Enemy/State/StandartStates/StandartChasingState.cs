using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartChasingState : EnemyChasingState
{
    public override void AwakeState(EnemyStateManager enemy)
    {
        base.AwakeState(enemy);
    }

    public override void EnterState(EnemyStateManager enemy)
    {
        base.EnterState(enemy);
        foreach (var _enemy in Controller.enemies)
        {
            Controller.NotifyPartners(_enemy);
        }
        enemy.SearchingState.target = Properties.Player;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        base.UpdateState(enemy);
        if (!Controller.EnemyFOV())
        {
            enemy.SwitchState(enemy.SearchingState);
            return;
        }

        if (enemyCombat.PlayerInRange(enemy, Properties.Player))
        {
            enemy.SwitchState(enemy.AttackState);
            return;
        }
    }
}
