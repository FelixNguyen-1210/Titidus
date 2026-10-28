using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using static Titidus.NightResult;

namespace Titidus
{
    public class NightActionResolver
    {

        public NightResult Resolve(GameSimulation simulation, IEnumerable<NightAction> actions)
        {

            List<AbilityEffect> effects = CollectEffects(simulation, actions);

            NightResult result = ResolveEffects(simulation, effects);

            return result;

        }

        private NightResult ResolveEffects(GameSimulation simulation, List<AbilityEffect> effects)
        {
            NightResult result = new();

            HashSet<ulong> attacked = new();
            HashSet<ulong> protectedPlayers = new();

            List<InspectEffect> inspections = new();

            foreach (AbilityEffect effect in effects)
            {
                switch (effect)
                {
                    case AttackEffect attack:
                        attacked.Add(attack.TargetId);
                        break;

                    case ProtectEffect protect:
                        protectedPlayers.Add(protect.TargetId);
                        break;

                    case InspectEffect inspect:
                        inspections.Add(inspect);
                        break;
                }
            }

            foreach (ulong targetId in attacked)
            {
                if (!protectedPlayers.Contains(targetId))
                {
                    result.DeadPlayers.Add(targetId);
                }
            }

            foreach (InspectEffect inspect in inspections)
            {
                GamePlayer target =
                    simulation.GetPlayer(inspect.TargetId);

                if (target?.Card == null)
                    continue;

                result.Inspections.Add(new InspectionResult(inspect.SourceId, inspect.TargetId, target.Card.Profile.Party));
            }

            return result;
        }

        List<AbilityEffect> CollectEffects(GameSimulation simulation, IEnumerable<NightAction> actions)
        {
            List<AbilityEffect> effects = new();
            foreach (var action in actions)
            {
                GamePlayer user = simulation.GetPlayer(action.UserId);

                if (user == null || !user.IsAlive || user.Card == null) continue;

                if (action.AbilityIndex < 0 || action.AbilityIndex >= user.Card.Abilities.Count) continue;


                IAbility ability = user.Card.Abilities[action.AbilityIndex];


                // GET TARGETS
                List<GamePlayer> targets = new();

                bool valid = action.TargetIds != null;

                if (!valid) continue;

                HashSet<ulong> uniqueIds = new();


                foreach (ulong targetId in action.TargetIds)
                {
                    if (!uniqueIds.Add(targetId))
                    {
                        valid = false;

                        break;
                    }

                    GamePlayer target = simulation.GetPlayer(targetId);

                    if (target == null)
                    {
                        valid = false;

                        break;
                    }

                    targets.Add(target);
                }

                if (!valid)
                    continue;

                if (!ability.CanExecute(simulation, user, targets)) continue;

                effects.AddRange(ability.Execute(simulation, user, targets));

            }


            return effects;

        }

        
    }
}