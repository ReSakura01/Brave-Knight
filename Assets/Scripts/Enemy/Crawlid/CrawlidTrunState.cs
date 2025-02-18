using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlidTrunState : EnemyState
{
    private Enemy_Crawlid enemy;
    public CrawlidTrunState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName, Enemy_Crawlid enemy) : base(stateMachine, enemyBase, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.SetVelocity(0, 0);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.Flip(); 
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }
}
