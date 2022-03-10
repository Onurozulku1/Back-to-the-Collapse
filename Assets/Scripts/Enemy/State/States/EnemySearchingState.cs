using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemySearchingState : EnemyBaseState
{
    private readonly float destFrequence = 0.75f;
    private Queue<Vector3> LastSeenPos;

    private float destTimer = 0;
    private float searchTimer = 0;

    public Transform target;

    bool RunningLastSeenPoint = false;
    Vector3 LastSeenPoint;

    public override void AwakeState(EnemyStateManager enemy)
    {
        Controller = enemy.Controller;
        Properties = Controller.Properties;
        target = Properties.Player;
    }

    public override void EnterState(EnemyStateManager enemy)
    {
        RunningLastSeenPoint = true;
        LastSeenPoint = Properties.Player.position;

        destTimer = destFrequence;
        searchTimer = 0;

        LastSeenPos = new Queue<Vector3>();
        LastSeenPos.Enqueue(target.position);
        Controller.Agent.SetDestination(LastSeenPos.Peek());
    }

    public override void UpdateState(EnemyStateManager enemy)
    {

        if (Controller.EnemyFOV())
        {
            enemy.SwitchState(enemy.ChasingState);
            return;
        }

        if (Controller.EnemyHear())
        {
            Controller.FaceToPlayer();
        }

        if (RunningLastSeenPoint)
        {
            Controller.Agent.SetDestination(LastSeenPoint);
            if (Vector3.Distance(LastSeenPos.Peek(), enemy.transform.position) < 2f)
            {
                RunningLastSeenPoint = false;
            }
        }
        else
        {

            Controller.Agent.speed = Properties.SearchingSpeed;
            Controller.Agent.stoppingDistance = 0.5f;

            searchTimer += Time.deltaTime;
            if (searchTimer > Properties.SearchingTime)
            {
                enemy.SwitchState(enemy.IdleState);
            }


            if (LastSeenPos.Count == 0)
                return;


            if (destTimer >= destFrequence)
            {
                LastSeenPos.Enqueue(target.position);
                destTimer = 0;
            }
            destTimer += Time.deltaTime;


            if (Vector3.Distance(LastSeenPos.Peek(), enemy.transform.position) < 1f)
            {
                LastSeenPos.Dequeue();

            }

            Controller.Agent.SetDestination(LastSeenPos.Peek());

        }

    }

}
