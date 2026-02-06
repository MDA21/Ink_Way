using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : EntityState
{
    protected PlayerStateMachine playerStateMachine;
    protected Player player;
    protected string stateName;

    public PlayerState(PlayerStateMachine _stateMachine, Player _player, string _stateName)
    {
        this.playerStateMachine = _stateMachine;
        this.player = _player;

        this.stateName = _stateName;
    }

}
