using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{

    EnemyBaseState currentState;

    public EnemyChasingState ChasingState = new();
    public EnemyAttackState AttackState = new();
    public EnemySearchingState SearchingState = new();
    public EnemyIdleState IdleState = new();

    public EnemyController Controller;
    

    private void Awake()
    {
        ChasingState.AwakeState(this);
        SearchingState.AwakeState(this);
        IdleState.AwakeState(this);
        AttackState.AwakeState(this);
    }

    private void OnValidate()
    {
        if (GetComponent<EnemyController>() != null)
        {
            Controller = GetComponent<EnemyController>();
        }
    }

    void Start()
    {
        currentState = IdleState;
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


