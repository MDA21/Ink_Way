using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState currentState { get; private set; }

    public void Initialize(PlayerState _startingState)
    {
        currentState = _startingState;
        currentState.Enter();
    }

    public void ChangeState(PlayerState _newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = _newState;
        if (currentState != null)
        {
            currentState.Enter();
        }
    }

    public void activateCurrentState()
    {
        if (currentState != null)
        {
            currentState.Update();
        }
    }

}
