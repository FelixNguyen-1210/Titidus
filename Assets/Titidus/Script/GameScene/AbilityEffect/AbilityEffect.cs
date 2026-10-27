namespace Titidus
{
    public abstract class AbilityEffect
    {
        public ulong SourceId { get; }
        public ulong TargetId { get; }

        protected AbilityEffect(ulong sourceId, ulong targetId)
        {
            SourceId = sourceId;
            TargetId = targetId;
        }
    }
}