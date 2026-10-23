using System;
using System.Collections.Generic;
using UnityEngine;

namespace Titidus
{
    public class Card
    {

        public CardProfile Profile;

        private readonly List<IAbility> abilities = new();

        public IReadOnlyList<IAbility> Abilities => abilities;

        public Card(CardProfile profile)
        {
            this.Profile = profile;
        }

        public void AddAbility(IAbility ability)
        {
            if (ability == null)
                throw new ArgumentNullException(nameof(ability));

            abilities.Add(ability);
        }
    }
}