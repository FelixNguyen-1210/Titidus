using System.Collections.Generic;
using System.Linq;
using UnityEditor.SpeedTree.Importer;

namespace Titidus
{
    public class MorningResult
    {
        public readonly ulong? deadPlayerId;
        public VoteResult Result;

        public MorningResult(VoteResult result)
        {
            this.Result = result;
            deadPlayerId = Resolve();

        }
        ulong? Resolve()
        {
            Dictionary<ulong, int> voted = new();

            foreach (var (targetId, agreeVote) in Result.Agree)
            {
                int disagreeVote = Result.Disagree.GetValueOrDefault(targetId);

                int different = agreeVote - disagreeVote;

                if (different > 0) voted.Add(targetId, different);
            }

            int count = voted.Count;

            if (count <= 0) return null;
            if (voted.Count == 1) return voted.First().Key;

            int maxVotes = voted.Values.Max();
            var topPlayers = voted.Where(pair => pair.Value == maxVotes).ToList();

            if (topPlayers.Count != 1) return null;

            return topPlayers[0].Key;
        }
        public override string ToString()
        {
            return $"Dead player's id: {string.Join(",", deadPlayerId)}";
        }
    }
}