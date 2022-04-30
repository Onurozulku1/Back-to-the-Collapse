using UnityEngine;
using System.Collections;
public class EnemyIdleState : EnemyBaseState
{
    private float waitTimer = 0;
    private float StopTime = 5;

    private Vector3 FirstPosition;
    private Quaternion FirstRotation;

    private bool isIdling = true;

    public override void AwakeState(EnemyStateManager enemy)
    {
        Controller = enemy.Controller;
        Properties = Controller.enemyProperties;
        FirstPosition = enemy.transform.position;
        FirstRotation = enemy.transform.rotation;
    }

    public override void EnterState(EnemyStateManager enemy)
    {
        Controller.Agent.stoppingDistance = 0;
        waitTimer = 0;

        isIdling = OnLocation(enemy);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
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
