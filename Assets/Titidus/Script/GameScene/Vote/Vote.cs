
using UnityEngine;


namespace Titidus
{
    public struct Vote
    {
        public ulong sourceId;
        public ulong targetId;
        public bool status;

        public Vote(ulong sourceId, bool status, ulong targetId)
        {
            this.sourceId = sourceId; 
            this.targetId = targetId;
            this.status = status;
        }

    }
}