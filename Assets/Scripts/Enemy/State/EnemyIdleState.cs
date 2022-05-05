using UnityEngine;
using System.Collections;
public class EnemyIdleState : EnemyBaseState
{
    public override void AwakeState(EnemyStateManager enemy)
    {
        Controller = enemy.Controller;
        Properties = Controller.enemyProperties;
    }

    public override void EnterState(EnemyStateManager enemy)
    {
        Controller.Agent.stoppingDistance = 0;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        
    }

    

}
