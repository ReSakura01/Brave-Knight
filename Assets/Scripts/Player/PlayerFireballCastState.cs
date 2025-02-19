using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireballCastState : PlayerState
{
    public PlayerFireballCastState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();

        player.StartCoroutine(player.BusyFor(.4f));
        player.SetVelocity(player.fireballCastSpeed.x * -player.facingDir, player.fireballCastSpeed.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}
