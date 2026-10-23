using Titidus;
using UnityEngine;

public class KillAbility : IAbility
{
    public string Name => "Kill";

    public bool CanExecute(GameSimulation simulation, GamePlayer user, GamePlayer target)
    {
        return true;
    }

    public void Execute(GameSimulation simulation, GamePlayer user, GamePlayer target)
    {
        if (!CanExecute(simulation, user, target)) return;
        target.Kill();
    }

}
