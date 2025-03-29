using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Crawlid : Enemy
{

    #region States
    public CrawlidMoveState moveState {  get; private set; }
    public CrawlidTrunState trunState { get; private set; }

    public CrawlidDeadState deadState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        moveState = new CrawlidMoveState(stateMachine, this, "Move", this);
        trunState = new CrawlidTrunState(stateMachine, this, "Trun", this);
        deadState = new CrawlidDeadState(stateMachine, this, "Move");
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(moveState);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deadState);
    }
}
