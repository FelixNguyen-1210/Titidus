using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

namespace Titidus
{
    public class NightResult
    {
        public readonly struct InspectionResult
        {
            public ulong SourceId { get; }
            public ulong TargetId { get; }
            public Party Party { get; }

            public InspectionResult(
                ulong sourceId,
                ulong targetId,
                Party party)
            {
                SourceId = sourceId;
                TargetId = targetId;
                Party = party;
            }
            public override string ToString()
            {
                return $"(Source:{SourceId.ToString()}, Target:{TargetId.ToString()}, Party:{Party.ToString()})";
            }
        }
        public HashSet<ulong> DeadPlayers { get; } = new();

        public List<InspectionResult> Inspections { get; } = new();

        public override string ToString()
        {
            return $"\nDead: [{string.Join("," ,DeadPlayers)}] - Inspection: [{string.Join(",", Inspections)}]";
        }

    }
}