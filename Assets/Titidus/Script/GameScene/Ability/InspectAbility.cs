using NUnit.Framework;
using System.Collections.Generic;
using Titidus;
using UnityEngine;

public class InspectAbility : IAbility
{
    public string Name => "Inspect";

    public int MinTarger => 1;

    public int MaxTarger => 1;

    public bool CanExecute(GameSimulation simulation, GamePlayer user, List<GamePlayer> targets)
    {
        return true;
    }

    public void Execute(GameSimulation simulation, GamePlayer user, List<GamePlayer> targets)
    {
        if (!CanExecute(simulation, user, targets)) return;

        Party result = targets[0].Card.Profile.Party;

        Debug.Log(
            $"[INSPECT] {user.Name} inspected " +
            $"{targets[0].Name}: {result}"
        );
    }
}
