using System.Collections.Generic;
using UnityEngine;


namespace Titidus
{
    public class Deck
    {

        public readonly List<CardId> Cards;

        readonly System.Random m_random = new();

        public Deck(List<CardId> cards)
        {
            Cards = cards;
        }
        public void Shuffle()
        {
            for (int i = Cards.Count - 1; i >= 0; i--)
            {
                int j = m_random.Next(i + 1);

                (Cards[i], Cards[j]) = (Cards[j], Cards[i]);
            }

        }

        

    }
}
