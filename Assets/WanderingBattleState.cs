using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingBattleState : EnemyState
{
    private Transform player;
    private Enemy_Wandering enemy;
    private int moveDir;

    public WanderingBattleState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName) : base(stateMachine, enemyBase, animBoolName)
    {
        enemy = enemyBase as Enemy_Wandering;
    }

    public override void Enter()
    {
        base.Enter();

        player = GameObject.Find("Blue").transform;
    }
    public override void Update()
    {
        base.Update();

        if (player.position.x > enemy.transform.position.x)
            moveDir = 1;
        else
            moveDir = -1;
        
        if (moveDir != enemy.facingDir)
        {
            enemy.Flip();
        }

        enemy.SetVelocity(3.5f * moveDir, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

}
