using Titidus;
using UnityEngine;

public interface IAbility
{
    string Name { get;}
    bool CanExecute(
        GameSimulation simulation,
        GamePlayer user,
        GamePlayer target
    );

    void Execute(
        GameSimulation simulation,
        GamePlayer user,
        GamePlayer target
    );
}
