using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyFollowingState : EnemyBaseState
{
    public EnemyStateManager leader;
    private float searchRadius = 10;
    private bool goSearching = true;
    public override void AwakeState(EnemyStateManager enemy)
    {
        Controller = enemy.Controller;
        Properties = Controller.enemyProperties;
    }

    public override void EnterState(EnemyStateManager enemy)
    {
        Controller.Agent.ResetPath();
        Controller.Agent.speed = Properties.ChasingSpeed;
        goSearching = true;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {

        if (leader == null)
            return;

        Controller.CheckPlayer();

        if (leader.currentState == leader.IdleState || leader.currentState == leader.AttackState)
            enemy.SwitchState(leader.currentState);
        
        if (leader.currentState == leader.SearchingState && leader.SearchingState.RunningLastSeenPoint)
        {
            Controller.Agent.stoppingDistance = 4;
            Controller.Agent.SetDestination(leader.transform.position);

        }
        else
        {
            if (goSearching)
            {
                finalPosition = PointAroundLeader();
                goSearching = false;
            }
            Controller.Agent.stoppingDistance = 0;
            Controller.Agent.SetDestination(finalPosition);
                

            if (Vector3.Distance(enemy.transform.position, finalPosition) < 3)
            {
                observeTimer += Time.deltaTime;
                if (observeTimer >= Properties.SearchingTime)
                {
                    observeTimer = 0;
                    goSearching = true;
                    leader = null;
                    enemy.SwitchState(enemy.IdleState);
                }
            }

        }


    }

    private float observeTimer;
    private Vector3 searchArea;
    private Vector3 finalPosition;
    private Vector3 PointAroundLeader()
    {
        Vector3 randomDirection = Random.onUnitSphere * searchRadius;
        searchArea = leader.SearchingState.LastSeenPoint + (Properties.Player.position - leader.transform.position).normalized * searchRadius;
        randomDirection += searchArea;
        UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out UnityEngine.AI.NavMeshHit hit, searchRadius, 1);
        return hit.position;

    }

}
