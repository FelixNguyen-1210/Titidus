using NUnit.Framework;
using System.Collections.Generic;
using Titidus;
using Titudus;
using UnityEngine;

public class NightActionManager : MonoBehaviour
{
    private readonly List<NightAction> actions = new();

    public IReadOnlyList<NightAction> Actions => actions;

    public bool Submit(NightAction action)
    {
        actions.Add(action);
        return true;
    }

    public bool Cancel(
        ulong userId,
        int abilityIndex,
        List<ulong> targetIds)
    {
        for (int i = 0; i < actions.Count; i++)
        {
            NightAction action = actions[i];

            if (action.UserId == userId &&
                action.AbilityIndex == abilityIndex &&
                action.TargetIds == targetIds)
            {
                actions.RemoveAt(i);
                return true;
            }
        }

        return false;
    }

    public void Clear()
    {
        actions.Clear();
    }
}
