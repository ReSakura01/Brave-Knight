using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAirState
{
    public PlayerWallJumpState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.StartCoroutine("BusyFor", .2f);
        player.SetVelocity(player.moveSpeed * -player.facingDir, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();

        player.fromWall = false;
    }

    public override void Update()
    {
        base.Update();

        if (rb.velocity.y < 0)
            stateMachine.ChangeState(player.fallState);
    }
}
