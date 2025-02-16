using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingGroundedState : EnemyState
{
    protected Enemy_Wandering enemy;
    public WanderingGroundedState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName) : base(stateMachine, enemyBase, animBoolName)
    {
        enemy = enemyBase as Enemy_Wandering;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (enemy.IsPlayerrDetected())
            stateMachine.ChangeState(enemy.battleState);
    }
}
