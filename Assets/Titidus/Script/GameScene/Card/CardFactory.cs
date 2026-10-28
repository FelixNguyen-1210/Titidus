using System;
using UnityEngine;

namespace Titidus
{
    public static class CardFactory
    {
        public static Card CreateCard(CardId id)
        {

            CardProfile profile = id switch
            {

                CardId.Villager => new CardProfile(id, "Villager", "Find the werewolves.", Party.Village, 1),
                CardId.Werewolf => new CardProfile(id, "Werewolf", "Kill village.", Party.Wolf, -1),
                CardId.Seer => new CardProfile(id,"Seer","Inspect another player's party.",Party.Village,2),
                CardId.Guard => new CardProfile(id, "Guard", "Protect another.", Party.Village, 2),

                _ => throw new ArgumentOutOfRangeException(nameof(id),id,"Unknown Card ID")
            };

            Card card = new Card(profile);

            switch (id)
            {
                case CardId.Werewolf:
                    card.AddAbility(new KillAbility() );
                    break;
                case CardId.Seer:
                    card.AddAbility(new InspectAbility() );
                    break;
                case CardId.Guard:
                    card.AddAbility(new ProtectAbility());
                    break;

                default:
                    Debug.Log($"Ability for card {card.Profile.Name} is Unknown");
                    break;
            }


            return card;
        }



    }
}
