using Titidus;
using UnityEngine;

public interface IState
{
    public abstract GamePhase Phase { get; }

    void Enter();
    void Exit();
}
