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
    public override void AwakeState(EnemyStateManager enemy)
    {
        Controller = enemy.Controller;
        Properties = Controller.Properties;
        target = Properties.Player;
    }

    public override void EnterState(EnemyStateManager enemy)
    {
        destTimer = 0;
        searchTimer = 0;

        Controller.Agent.speed = Properties.SearchingSpeed;
        Controller.Agent.stoppingDistance = 0.5f;

        LastSeenPos = new Queue<Vector3>();
        LastSeenPos.Enqueue(target.position);
        Controller.Agent.SetDestination(LastSeenPos.Peek());
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if (enemy.Controller.EnemyFOV())
        {
            enemy.SwitchState(enemy.ChasingState);
            return;
        }


        searchTimer += Time.deltaTime;
        if (searchTimer > Properties.MemoryTime)
        {
            enemy.SwitchState(enemy.IdleState);
        }


        if (LastSeenPos.Count == 0)
            return;


        Controller.Agent.SetDestination(LastSeenPos.Peek());

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
    }

}
