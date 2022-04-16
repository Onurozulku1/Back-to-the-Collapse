﻿using UnityEngine;
using System;

public class EnemyChasingState : EnemyBaseState
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
        Controller.Agent.speed = Properties.ChasingSpeed;
        Controller.Agent.stoppingDistance = 2;
        enemy.SearchingState.target = Properties.Player;
        Controller.NotifyPartners();
    }


    public override void UpdateState(EnemyStateManager enemy)
    {
        lookRotation = (Properties.Player.transform.position - enemy.transform.position).normalized;
        lookRotation.y = 0;
        enemy.transform.rotation = Quaternion.Lerp(enemy.transform.rotation, Quaternion.LookRotation(lookRotation, Vector3.up), 0.2f);

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
