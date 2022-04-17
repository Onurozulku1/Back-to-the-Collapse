using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowingState : EnemyBaseState
{
    public EnemyStateManager leader;
    public override void AwakeState(EnemyStateManager enemy)
    {
        Controller = enemy.Controller;
        Properties = Controller.enemyProperties;
    }

    public override void EnterState(EnemyStateManager enemy)
    {
        Controller.Agent.ResetPath();
        Controller.Agent.speed = Properties.ChasingSpeed;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        Controller.Agent.stoppingDistance = 4;

        if (leader == null)
            return;

        Controller.CheckPlayer();
        Controller.Agent.SetDestination(leader.transform.position);

        if (leader.currentState == leader.IdleState || leader.currentState == leader.AttackState || leader.currentState == leader.SearchingState)
            enemy.SwitchState(leader.currentState);

        Debug.Log(Controller.Agent.stoppingDistance);
        
    }

    
}
