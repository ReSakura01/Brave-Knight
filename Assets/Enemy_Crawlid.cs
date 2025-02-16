using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Crawlid : Enemy
{

    #region States
    public CrawlidMoveState moveState {  get; private set; }
    public CrawlidTrunState trunState { get; private set; }
    #endregion


    protected override void Awake()
    {
        base.Awake();

        moveState = new CrawlidMoveState(stateMachine, this, "Move", this);
        trunState = new CrawlidTrunState(stateMachine, this, "Trun", this);
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

    public virtual void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
}
