using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartIdleState : EnemyIdleState
{
    private float waitTimer = 0;
    private float StopTime = 5;

    private Vector3 FirstPosition;
    private Quaternion FirstRotation;

    private bool isIdling = true;

    public override void AwakeState(EnemyStateManager enemy)
    {
        base.AwakeState(enemy);
        FirstPosition = enemy.transform.position;
        FirstRotation = enemy.transform.rotation;
    }

    public override void EnterState(EnemyStateManager enemy)
    {
        base.EnterState(enemy);
        waitTimer = 0;

        isIdling = OnLocation(enemy);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        base.UpdateState(enemy);
        Controller.CheckPlayer();

        if (OnLocation(enemy))
        {
            Controller.Agent.ResetPath();
            if (!isIdling)
            {
                enemy.transform.rotation = FirstRotation;
                isIdling = true;
            }

            return;
        }

        waitTimer += Time.deltaTime;
        if (waitTimer >= StopTime)
        {
            Controller.Agent.SetDestination(FirstPosition);
            Controller.Agent.stoppingDistance = 0;
        }
    }

    private bool OnLocation(EnemyStateManager enemy)
    {
        return (Vector3.Distance(FirstPosition, enemy.transform.position) < 1);
    }
}
