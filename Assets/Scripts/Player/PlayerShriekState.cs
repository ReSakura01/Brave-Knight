using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShriekState : PlayerState
{
    public PlayerShriekState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
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

        player.SetVelocity(0, 0);

        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}
