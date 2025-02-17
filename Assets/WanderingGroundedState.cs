using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingGroundedState : EnemyState
{
    protected Enemy_Wandering enemy;

    protected Transform player;
    public WanderingGroundedState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName) : base(stateMachine, enemyBase, animBoolName)
    {
        enemy = enemyBase as Enemy_Wandering;
    }

    public override void Enter()
    {
        base.Enter();

        player = GameObject.Find("Blue").transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if ((enemy.IsPlayerrDetected() || Vector2.Distance(enemy.transform.position, player.transform.position) < 2)
            && CanAttack())
            stateMachine.ChangeState(enemy.attackState);
    }

    private bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            enemy.lastTimeAttacked = Time.time;
            return true;
        }

        return false;
    }
}
