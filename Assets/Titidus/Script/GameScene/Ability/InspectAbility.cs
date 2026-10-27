using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Titidus;
using UnityEngine;

public class InspectAbility : IAbility
{
    public string Name => "Inspect";

    public int MinTarger => 1;

    public int MaxTarger => 1;

    public bool CanExecute(GameSimulation simulation, GamePlayer user, List<GamePlayer> targets)
    {
        if (targets.Count != 1) { return false; }
        return true;
    }

    public IEnumerable<AbilityEffect> Execute(GameSimulation simulation, GamePlayer user, List<GamePlayer> targets)
    {
        if (!CanExecute(simulation, user, targets)) yield return null;
        else  yield return new InspectEffect(user.Id, targets[0].Id);
    }
}
