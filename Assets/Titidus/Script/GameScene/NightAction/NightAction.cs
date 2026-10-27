using System.Collections.Generic;
namespace Titidus
{
    public readonly struct NightAction
    {
        public ulong UserId { get; }
        public int AbilityIndex { get; }
        public IReadOnlyList<ulong> TargetIds { get; }

        public NightAction(
            ulong userId,
            int abilityIndex,
            IEnumerable<ulong> targetIds)
        {
            UserId = userId;
            AbilityIndex = abilityIndex;
            TargetIds = new List<ulong>(targetIds).AsReadOnly();
        }
    }
}
