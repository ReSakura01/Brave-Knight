using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlidDeadState : EnemyState
{
    protected Enemy_Crawlid enemy;
    public CrawlidDeadState(EnemyStateMachine stateMachine, Enemy enemyBase, string animBoolName) : base(stateMachine, enemyBase, animBoolName)
    {
        enemy = enemyBase as Enemy_Crawlid;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        
    }

    public override void Enter()
    {
        base.Enter();
        enemy.anim.SetBool(enemy.lastAnimBoolName, true);
        enemy.anim.speed = 0;
        enemy.cd.enabled = false;

        stateTimer = .1f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer > 0)
            rb.velocity = new Vector2(0, 10);
    }
}
