using System;
using System.Collections.Generic;
using Titidus;
using UnityEngine;

public class PhaseStateMachine
{
    public IState CurrentState;
    public event Action<GamePhase> OnPhaseChanged;

    public readonly Dictionary<GamePhase, IState> states = new();


    public void RegisterState(IState state)
    {

        GamePhase phase = state.Phase;

        if (!states.TryAdd(phase, state))
        {
            throw new InvalidOperationException(
                $"State {phase} has been registered");
        }
    }
    public void ChangeState(GamePhase phrase)
    {
        if (!states.TryGetValue(phrase, out var nextState))
        {
            throw new InvalidOperationException(
                $"State {phrase} not registered");
        }

        CurrentState?.Exit();
        CurrentState = nextState;
        CurrentState.Enter();
        OnPhaseChanged?.Invoke(phrase);
    }

    public void UpdateState()
    {

    }


}
