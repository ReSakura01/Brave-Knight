using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingIdleState : WanderingGroundedState
{
    public WanderingIdleState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName) : base(stateMachine, enemyBase, animBoolName)
    {
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
