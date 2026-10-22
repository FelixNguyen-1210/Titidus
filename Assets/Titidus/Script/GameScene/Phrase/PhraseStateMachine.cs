using System;
using System.Collections.Generic;
using Titidus;
using UnityEngine;

public class PhaseStateMachine
{
    public IState currentState;
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

        currentState?.Exit();
        currentState = nextState;
        currentState.Enter();
        OnPhaseChanged?.Invoke(phrase);
    }

    public void UpdateState()
    {

    }


}
