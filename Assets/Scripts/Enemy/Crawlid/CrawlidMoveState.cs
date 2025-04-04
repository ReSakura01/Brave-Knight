using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlidMoveState : EnemyState
{
    private Enemy_Crawlid enemy;

    public CrawlidMoveState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, Enemy_Crawlid enemy) : base(stateMachine, enemyBase, animBoolName)
    {
        this.enemy = enemy;
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

        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, rb.velocity.y);

        if (enemy.IsWallDetected() || !enemy.IsGroundDetected())
        {
            stateMachine.ChangeState(enemy.trunState);
        }
    }
}
