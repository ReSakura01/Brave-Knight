using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Enemy_Wandering : Enemy
{
    #region States
    public WanderingIdleState idleState {  get; private set; }
    public WanderingMoveState moveState { get; private set; }
    public WanderingAttackState attackState { get; private set; }
    public WanderingDeadState deadState { get; private set; }

    #endregion
    protected override void Awake()
    {
        base.Awake();

        idleState = new WanderingIdleState(stateMachine, this, "Idle");
        moveState = new WanderingMoveState(stateMachine, this, "Move");
        attackState = new WanderingAttackState(stateMachine, this, "Attack");
        deadState = new WanderingDeadState(stateMachine, this, "Move");
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
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
