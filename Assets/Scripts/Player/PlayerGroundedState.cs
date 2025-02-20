using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
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

        player.FlipController(rb.velocity.x);

        if (Input.GetKeyDown(KeyCode.J))
            stateMachine.ChangeState(player.primaryAttackState);

        if (!player.IsGroundDetected() && !player.IsSpikeDetected())
            stateMachine.ChangeState(player.fallState);

        if (Input.GetKeyDown(KeyCode.K) && (player.IsGroundDetected() || player.IsSpikeDetected()) && !player.isDashing)
            stateMachine.ChangeState(player.jumpState);
    }
}
