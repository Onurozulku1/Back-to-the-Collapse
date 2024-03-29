﻿using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private Vector3 lookRotation; 

    public override void AwakeState(EnemyStateManager enemy)
    {
        enemyCombat = new EnemyCombat();
        Controller = enemy.Controller;
        Properties = Controller.enemyProperties;
    }

    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.SearchingState.target = Properties.Player;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        lookRotation = (Properties.Player.transform.position - enemy.transform.position).normalized;
        lookRotation.y = 0;
        enemy.transform.rotation = Quaternion.LookRotation(lookRotation);

        if (!Controller.EnemyFOV())
        {
            enemy.SwitchState(enemy.SearchingState);
            return;
        }

        if (!enemyCombat.PlayerInRange(enemy, Properties.Player))
        {
            enemy.SwitchState(enemy.ChasingState);
            return;
        }

        lookRotation = (Properties.Player.transform.position - enemy.transform.position).normalized;
        lookRotation.y = 0;
        enemy.transform.rotation = Quaternion.LookRotation(lookRotation);

        Controller.Agent.SetDestination(Properties.Player.position);

    }

}
