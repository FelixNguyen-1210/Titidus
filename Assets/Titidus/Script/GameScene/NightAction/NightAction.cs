using System.Collections.Generic;
using UnityEngine;
namespace Titudus
{
    public readonly struct NightAction
    {
        public ulong UserId { get; }
        public int AbilityIndex { get; }
        public List<ulong> TargetIds { get; }

        public NightAction(
            ulong userId,
            int abilityIndex,
            List<ulong> targetIds)
        {
            UserId = userId;
            AbilityIndex = abilityIndex;
            TargetIds = targetIds;
        }
    }
}
