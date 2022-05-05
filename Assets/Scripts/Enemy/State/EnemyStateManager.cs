using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{

    public EnemyBaseState currentState;

    public EnemyIdleState IdleState;
    public EnemyChasingState ChasingState;

    public EnemyAttackState AttackState = new();
    public EnemySearchingState SearchingState = new();
    public EnemyPatrollingState PatrollingState = new();
    public EnemyFollowingState FollowingState = new();

    public EnemyController Controller;
    

    private void Awake()
    {
        if (GetComponent<EnemyController>() == null)
            Controller = gameObject.AddComponent(typeof(EnemyController)) as EnemyController;  
        else
            Controller = GetComponent<EnemyController>();


        if (Controller.enemyProperties.enemyType == EnemyProperties.EnemyType.standart)
        {
            IdleState = new StandartIdleState();
            ChasingState = new StandartChasingState();
        }
        else if (Controller.enemyProperties.enemyType == EnemyProperties.EnemyType.blind)
        {
            Controller = GetComponent<EnemyController>();

            IdleState = new BlindIdleState();
            ChasingState = new BlindChasingState();
        }


        ChasingState.AwakeState(this);
        SearchingState.AwakeState(this);
        IdleState.AwakeState(this);
        AttackState.AwakeState(this);
        PatrollingState.AwakeState(this);
        FollowingState.AwakeState(this);
    }

    void Start()
    {
        currentState = Controller.isPatrolling ? PatrollingState : IdleState;
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        state.EnterState(this);

    }

}


