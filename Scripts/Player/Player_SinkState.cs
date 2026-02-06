using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_SinkState : PlayerState
{
    public Player_SinkState(PlayerStateMachine _stateMachine, Player _player, string _stateName) : base(_stateMachine, _player, _stateName)
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
    }
}
