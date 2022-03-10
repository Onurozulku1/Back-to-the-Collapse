using UnityEngine;
using System;

public class EnemyChasingState : EnemyBaseState
{

    public override void AwakeState(EnemyStateManager enemy)
    {
        enemyCombat = new EnemyCombat();
        Controller = enemy.Controller;
        Properties = Controller.Properties;
    }

    public override void EnterState(EnemyStateManager enemy)
    {
        Controller.Agent.speed = Properties.ChasingSpeed;
        Controller.Agent.stoppingDistance = 2;
        enemy.SearchingState.target = Properties.Player;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
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

        Controller.Agent.SetDestination(Properties.Player.position);

        

        

    }
   
    
}
