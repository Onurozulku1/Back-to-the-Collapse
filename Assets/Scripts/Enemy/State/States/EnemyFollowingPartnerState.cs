using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowingPartnerState : EnemyBaseState
{
    public EnemyStateManager followingPartner;
    public override void AwakeState(EnemyStateManager enemy)
    {
        Controller = enemy.Controller;
        Properties = Controller.enemyProperties;
    }

    public override void EnterState(EnemyStateManager enemy)
    {
        Controller.Agent.ResetPath();
        Controller.Agent.speed = Properties.ChasingSpeed;
        Controller.Agent.stoppingDistance = 4;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if (followingPartner == null)
            return;

        Controller.CheckPlayer();
        Controller.Agent.SetDestination(followingPartner.transform.position);

        if (followingPartner.currentState != followingPartner.ChasingState)
            enemy.SwitchState(followingPartner.currentState);
    }
}
