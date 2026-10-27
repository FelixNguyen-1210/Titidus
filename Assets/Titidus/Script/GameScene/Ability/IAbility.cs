using System.Collections;
using System.Collections.Generic;
using Titidus;

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

    public IEnumerable<AbilityEffect> Execute(
        GameSimulation simulation,
        GamePlayer user,
        List<GamePlayer> targets
    );
}
