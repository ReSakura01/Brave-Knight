using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
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

        if (xInput != 0)
            player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);

        if (player.IsWallDetected() && !player.fromWall)
            stateMachine.ChangeState(player.wallSlideState);

    }
}
