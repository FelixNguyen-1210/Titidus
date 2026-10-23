using UnityEngine;

namespace Titidus
{
    public readonly struct CardProfile
    {

        public CardId Id { get; }
        public string Name { get; }
        public string Description { get; }
        public Party Party { get; }
        public int Point { get; }

        public CardProfile(
            CardId id,
            string name,
            string description,
            Party party,
            int point)
        {
            Id = id;
            Name = name;
            Description = description;
            Party = party;
            Point = point;
        }
    }
}