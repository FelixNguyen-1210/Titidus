using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Titidus
{
    public struct VoteResult
    {
        public Dictionary<ulong, int> Agree;
        public Dictionary<ulong, int> Disagree;


        public override string ToString()
        {
            return $"[Agree: {string.Join(",", Agree.Keys)}] [Disagree: {string.Join(",", Disagree.Keys)}]";
        }

    }
}