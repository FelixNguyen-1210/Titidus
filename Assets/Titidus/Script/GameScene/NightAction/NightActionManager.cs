using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Titidus;
using UnityEngine;

public class NightActionManager
{
    private readonly Dictionary<(ulong, int), NightAction> actions = new();

    public IEnumerable<NightAction> Actions => actions.Values;

    public bool Submit(NightAction action)
    {
        var key = (action.UserId, action.AbilityIndex);

        actions[key] = action;
        return true;
    }

    public bool Cancel(ulong userId, int abilityIndex)
    {
        return actions.Remove((userId, abilityIndex));
    }

    public void Clear()
    {
        actions.Clear();
    }

}
