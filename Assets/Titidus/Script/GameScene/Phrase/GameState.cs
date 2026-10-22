using Titidus;
using UnityEngine;

public abstract class GameState : IState
{
    protected readonly GameSimulation simulation;
    public abstract GamePhase Phase { get; }

    protected GameState(GameSimulation simulation)
    {
        this.simulation = simulation;
    }

    public virtual void Enter()
    {
        Debug.Log($"[STATE] Enter {Phase}"); 
    }

    public virtual void Exit()
    {
        Debug.Log($"[STATE] Exit {Phase}");
    }
}
