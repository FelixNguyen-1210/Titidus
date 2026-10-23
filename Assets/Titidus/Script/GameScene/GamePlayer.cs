
using Unity.Collections;
using UnityEngine;

namespace Titidus
{
    public class GamePlayer
    {
        public ulong Id { get; }
        public string Name { get; }

        public Card Card { get; private set; }


        public bool IsAlive { get; private set; }

        public GamePlayer(ulong id, string name)
        {
            Id = id;
            Name = name;
            IsAlive = true;
        }

        public void Kill()
        {
            IsAlive = false;
        }

        public void Revive()
        {
            IsAlive = true;
        }
        public void AssignCard(Card card)
        {
            Card = card;
        }

        public override string ToString()
        {
            return $"({Id}) {Name}: [Role: {Card.Profile.Id}] [Status: {IsAlive}]";
        }
    }
}