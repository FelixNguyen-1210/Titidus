using System.Collections.Generic;
using Titidus;
using UnityEngine;

public class VoteManager
{
    public readonly List<Vote> votes = new();

    public bool Submit(Vote vote)
    {

        Debug.Log($"{vote.sourceId} vote {vote.targetId}: {vote.status}");
        votes.Add(vote);
        return true;
    }

    public void Clear()
    {
        votes.Clear();
    }
    public int Count()
    {
        return votes.Count;
    }

    public VoteResult Resolve()
    {
        VoteResult result = new();
        Dictionary<ulong, int> agree = new();
        Dictionary<ulong, int> disagree = new();

        // filter 

        foreach (Vote vote in votes)
        {
            
            if (vote.status) agree[vote.targetId] = agree.TryGetValue(vote.targetId, out var count) ? count + 1 : 1;

            else disagree[vote.targetId] = disagree.TryGetValue(vote.targetId, out var count) ? count + 1 : 1;

        }

        result.Agree = agree;
        result.Disagree = disagree;


        return result;
    }
}
