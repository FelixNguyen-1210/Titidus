using Titidus;
using UnityEngine;

public class InspectAbility : IAbility
{
    public string Name => "Inspect";


    public bool CanExecute(GameSimulation simulation, GamePlayer user, GamePlayer target)
    {
        return true;
    }

    public void Execute(GameSimulation simulation, GamePlayer user, GamePlayer target)
    {
        if (!CanExecute(simulation, user, target)) return;
        Party result = target.Card.Profile.Party;

        Debug.Log(
            $"[INSPECT] {user.Name} inspected " +
            $"{target.Name}: {result}"
        );
    }
}
