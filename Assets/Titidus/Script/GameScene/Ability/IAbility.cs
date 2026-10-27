using NUnit.Framework;
using System.Collections.Generic;
using Titidus;
using UnityEngine;

public interface IAbility
{
    string Name { get;}
    int MinTarger { get; }
    int MaxTarger { get; }
    bool CanExecute(
        GameSimulation simulation,
        GamePlayer user,
        List<GamePlayer> targets
    );

    void Execute(
        GameSimulation simulation,
        GamePlayer user,
        List<GamePlayer> targets
    );
}
