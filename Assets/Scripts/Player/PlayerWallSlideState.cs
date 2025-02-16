using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(0, rb.velocity.y * 0.7f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(0, player.wallSlideForce);

        if (xInput != 0 && player.facingDir != xInput || player.IsGroundDetected() || !player.IsWallDetected())
            stateMachine.ChangeState(player.idleState);

        if (Input.GetKeyDown(KeyCode.K))
        {
            player.fromWall = true;
            stateMachine.ChangeState(player.wallJumpState);
        }
    }
}
