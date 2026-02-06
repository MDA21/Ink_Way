using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_NormalState : PlayerState
{
    public Player_NormalState(PlayerStateMachine _stateMachine, Player _player, string _stateName) : base(_stateMachine, _player, _stateName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("进入正常状态");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        player.movement.Update3DMovement();
    }
}
