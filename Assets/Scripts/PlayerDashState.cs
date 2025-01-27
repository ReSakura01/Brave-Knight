using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.isDashing = true;
        stateTimer = player.dashDuration;
    }

    public override void Exit()
    {
        base.Exit();
        player.fromWall = false;
        player.isDashing = false;
        player.SetVelocity(0, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        player.FlipController(rb.velocity.x);

        player.SetVelocity(player.dashSpeed * player.dashDir, 0);

        if (player.IsWallDetected() && !player.fromWall)
            stateMachine.ChangeState(player.wallSlideState);

        if (stateTimer < 0)
            stateMachine.ChangeState(player.idleState);
    }
}
