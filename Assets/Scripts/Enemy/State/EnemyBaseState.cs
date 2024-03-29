﻿using UnityEngine;

public abstract class EnemyBaseState 
{
    protected EnemyController Controller;
    protected EnemyProperties Properties;
    protected EnemyCombat enemyCombat;

    
    public abstract void AwakeState(EnemyStateManager enemy);
    public abstract void EnterState(EnemyStateManager enemy);
    public abstract void UpdateState(EnemyStateManager enemy);

}
