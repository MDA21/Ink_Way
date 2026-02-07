using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_SinkState : PlayerState
{
    public Player_SinkState(PlayerStateMachine _playerStateMachine, Player _player, string _stateName) : base(_playerStateMachine, _player, _stateName)
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
        Debug.Log("玩家进入下沉状态");

        if (player.movement.sinkOn)
        {
            player.movement.UpdateSinkMovement();
            Debug.Log("触发动作");
        }
        else
        {
            player.playerStateMachine.ChangeState(player.normalState);
        }
    }
}
