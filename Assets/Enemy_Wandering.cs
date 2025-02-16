using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Wandering : Enemy
{
    #region States
    public WanderingIdleState idleState {  get; private set; }
    public WanderingMoveState moveState { get; private set; }

    public WanderingBattleState battleState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();

        idleState = new WanderingIdleState(stateMachine, this, "Idle");
        moveState = new WanderingMoveState(stateMachine, this, "Move");
        battleState = new WanderingBattleState(stateMachine, this, "Battle");
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
}
