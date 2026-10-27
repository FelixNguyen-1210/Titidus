using System.Collections.Generic;
using Titidus;
using UnityEngine;

public class KillAbility : IAbility
{
    public string Name => "Kill";

    public int MinTarger => 0;

    public int MaxTarger => 2;

    public bool CanExecute(GameSimulation simulation, GamePlayer user, List<GamePlayer> targets)
    {
        if (targets.Count <= 0) { return false; }

        return true;
    }

    public IEnumerable<AbilityEffect> Execute(GameSimulation simulation, GamePlayer user, List<GamePlayer> targets)
    {
        if (!CanExecute(simulation, user, targets)) yield return null;

        else
        {
            foreach (var target in targets)
            {
                yield return new AttackEffect(user.Id, target.Id);
            }
        }
    }

}
