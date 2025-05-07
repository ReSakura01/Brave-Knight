using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAttackState : EnemyState
{
    private Transform player;
    private Enemy_Wandering enemy;
    private int moveDir;

    public WanderingAttackState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName) : base(stateMachine, enemyBase, animBoolName)
    {
        enemy = enemyBase as Enemy_Wandering;
    }

    public override void Enter()
    {
        base.Enter();

        player = PlayerManager.instance.player.transform;

        if (player.position.x > enemy.transform.position.x)
            moveDir = 1;
        else
            moveDir = -1;

        if (moveDir != enemy.facingDir)
        {
            enemy.Flip();
        }
    }
    public override void Update()
    {
        base.Update();

        enemy.SetVelocity(3.5f * moveDir, rb.velocity.y);

        if (triggerCalled)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        enemy.lastTimeAttacked = Time.time;
    }

}
