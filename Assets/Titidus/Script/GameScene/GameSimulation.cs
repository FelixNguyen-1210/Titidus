using System;
using System.Collections.Generic;
using UnityEngine;


namespace Titidus
{
    public class GameSimulation
    {
        public readonly Dictionary<ulong, GamePlayer> Players = new Dictionary<ulong, GamePlayer>();

        public PhaseStateMachine PhaseMachine { get; private set; }
        public Deck GameDeck { get; set; }

        public readonly NightActionManager nightActionManager = new();
        private readonly NightActionResolver nightResolver = new();
        public void InitializePhases()
        {
            PhaseMachine = new PhaseStateMachine();

            PhaseMachine.RegisterState(new PreparationState(this));

            PhaseMachine.RegisterState(new NightState(this));

            PhaseMachine.RegisterState(new MorningState(this));

            PhaseMachine.RegisterState(new DiscussState(this));

            PhaseMachine.RegisterState(new EndState(this));

            PhaseMachine.RegisterState(new VoteState(this));
            PhaseMachine.RegisterState(new ResolutionState(this));


            PhaseMachine.ChangeState(GamePhase.Preparation);
        }

        public void NextPhase()
        {
            if (PhaseMachine == null || PhaseMachine.CurrentState == null)
                return;
            GamePhase next = PhaseMachine.CurrentState.Phase switch
            {
                GamePhase.Preparation => GamePhase.Night,
                GamePhase.Night => GamePhase.Morning,
                GamePhase.Morning => GamePhase.Discussion,
                GamePhase.Discussion => GamePhase.Voting,
                GamePhase.Voting => GamePhase.Resolution,
                GamePhase.Resolution => GamePhase.Night,
                _ => GamePhase.End
            };

            PhaseMachine.ChangeState(next);
        }

        public GamePlayer GetPlayer(ulong playerId)
        {
            return Players.TryGetValue(playerId, out var player)
                ? player
                : null;
        }

        public void AddPlayer(GamePlayer player)
        {
            if (!Players.TryAdd(player.Id, player))
                throw new InvalidOperationException(
                    $"Player {player.Id} already exists.");
        }
        public void CreateDeck(List<CardId> cards)
        {
            Deck GameDeck = new Deck(cards);
        }
        public void Reset()
        {
            Players.Clear();
        }

        //public bool ExcuteAbility(int abilityIndex, ulong userId, List<ulong> targetIds)
        //{
        //    GamePlayer user = GetPlayer(userId);



        //    if (user == null || targetIds == null)
        //        return false;

        //    if (!user.IsAlive || user.Card == null)
        //        return false;

        //    if (abilityIndex < 0 ||
        //        abilityIndex >= user.Card.Abilities.Count)
        //        return false;


        //    List<GamePlayer> targets = new();
        //    foreach (var id in targetIds)
        //    {
        //        GamePlayer target = GetPlayer(id);
        //        targets.Add(target);

        //    }



        //    IAbility ability = user.Card.Abilities[abilityIndex];

        //    if (!ability.CanExecute(this, user, targets))
        //        return false;

        //    ability.Execute(this, user, targets);

        //    return true;
        //}

        public bool SubmitNightAction(ulong userId, int abilityIndex, List<ulong> targetIds)
        {
            if (PhaseMachine?.CurrentState?.Phase != GamePhase.Night)
                return false;

            GamePlayer user = GetPlayer(userId);




            if (user == null || targetIds == null)
                return false;

            if (!user.IsAlive || user.Card == null)
                return false;

            if (abilityIndex < 0 ||
                abilityIndex >= user.Card.Abilities.Count)
                return false;


            List<GamePlayer> targets = new();

            foreach (var id in targetIds)
            {
                GamePlayer target = GetPlayer(id);
                targets.Add(target);
            }



            IAbility ability = user.Card.Abilities[abilityIndex];

            if (!ability.CanExecute(this, user, targets)) return false;

            NightAction action = new(userId, abilityIndex, targetIds);

            return nightActionManager.Submit(action);
        }

        public NightResult ResolveNight()
        {
            if (PhaseMachine?.CurrentState?.Phase != GamePhase.Night)
                throw new InvalidOperationException("Cannot resolve outside Night phase");

            NightResult result = nightResolver.Resolve(this, nightActionManager.Actions);

            foreach (ulong playerId in result.DeadPlayers)
            {
                GetPlayer(playerId)?.Kill();
            }

            nightActionManager.Clear();

            PhaseMachine.ChangeState(GamePhase.Morning);

            return result;
        }
    }
}