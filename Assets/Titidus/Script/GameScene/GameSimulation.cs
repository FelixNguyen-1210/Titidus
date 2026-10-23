using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Titidus
{
    public class GameSimulation
    {
        public readonly Dictionary<ulong, GamePlayer> Players = new Dictionary<ulong, GamePlayer>();

        public PhaseStateMachine PhaseMachine { get; private set; }
        public Deck GameDeck { get; set; }

        public void InitializePhases()
        {
            PhaseMachine = new PhaseStateMachine();

            PhaseMachine.RegisterState(new PreparationState(this));
            
            PhaseMachine.RegisterState(new NightState(this));
            
            PhaseMachine.RegisterState(new MorningState(this));
            
            PhaseMachine.RegisterState(new DiscussState(this));
            
            PhaseMachine.RegisterState(new EndState(this));
            
            PhaseMachine.RegisterState(new VoteState(this));

            PhaseMachine.ChangeState(GamePhase.Preparation);
        }

        public void NextPhase()
        {
            if (PhaseMachine == null || PhaseMachine.currentState == null)
                return;
            GamePhase next = PhaseMachine.currentState.Phase switch
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
    }
}