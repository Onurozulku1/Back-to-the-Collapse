using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{

    public EnemyBaseState currentState;

    public EnemyChasingState ChasingState = new();
    public EnemyAttackState AttackState = new();
    public EnemySearchingState SearchingState = new();
    public EnemyIdleState IdleState = new();
    public EnemyPatrollingState PatrollingState = new();
    public EnemyFollowingPartnerState FollowingPartnerState = new();

    public EnemyController Controller;
    

    private void Awake()
    {
        if (GetComponent<EnemyController>() != null)
        {
            Controller = GetComponent<EnemyController>();
        }

        ChasingState.AwakeState(this);
        SearchingState.AwakeState(this);
        IdleState.AwakeState(this);
        AttackState.AwakeState(this);
        PatrollingState.AwakeState(this);
        FollowingPartnerState.AwakeState(this);
    }

    void Start()
    {
        currentState = Controller.isPatrolling ? PatrollingState : IdleState;
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
        Debug.Log(currentState);
    }

    public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        state.EnterState(this);

    }

}


