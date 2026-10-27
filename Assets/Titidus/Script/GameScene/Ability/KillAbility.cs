using System.Collections.Generic;
using Titidus;
using UnityEngine;

public class KillAbility : IAbility
{
    public string Name => "Kill";

    public int MinTarger => 0;

    public int MaxTarger => 1;

    public bool CanExecute(GameSimulation simulation, GamePlayer user, List<GamePlayer> targets)
    {
        return true;
    }

    public void Execute(GameSimulation simulation, GamePlayer user, List<GamePlayer> targets)
    {
        if (!CanExecute(simulation, user, targets)) return;

        targets[0].Kill();
    }

}
