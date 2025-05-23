using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStunedState : PlayerState
{
    public PlayerStunedState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
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

        if (!player.isKnocked)
            stateMachine.ChangeState(player.idleState);
    }
}
