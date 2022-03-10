using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrollingState : EnemyBaseState
{
    public override void AwakeState(EnemyStateManager enemy)
    {
        enemyCombat = new EnemyCombat();
        Controller = enemy.Controller;
        Properties = Controller.Properties;
    }

    public override void EnterState(EnemyStateManager enemy)
    {
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        Controller.CheckPlayer();
    }
}
