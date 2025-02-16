using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingIdleState : EnemyState
{
    private Enemy_Wandering enemy;

    public WanderingIdleState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName) : base(stateMachine, enemyBase, animBoolName)
    {
        enemy = enemyBase as Enemy_Wandering;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.SetVelocity(0, 0);
        stateTimer = enemy.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.moveState);
    }
}
