
using NUnit.Framework.Internal.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Titidus
{
    public class DebugGameController : MonoBehaviour
    {
        [SerializeField] List<CardId> cards = new();
        private GameSimulation simulation;

        [ContextMenu("Start Simulation")]
        public void StartSimulation()
        {
            simulation = new GameSimulation();

            simulation.InitializePhases();

            for (int i = 0; i < cards.Count; i++)
            {
                var player = new GamePlayer(
                    (ulong)i,
                    $"Bot_{i + 1}"
                );

                simulation.AddPlayer(player);
            }

            Debug.Log(
                $"Simulation started: {simulation.Players.Count} players"
            );


        }

        [ContextMenu("Reset Simulation")]
        public void ResetSimulation()
        {
            simulation?.Reset();
            Debug.Log("Simulation reset");
        }

        [ContextMenu("Next Phase")]
        public void NextPhase()
        {
            if (simulation == null) return;

            simulation.NextPhase();

        }

        [ContextMenu("Force End")]
        public void ForceEnd()
        {
            simulation?.PhaseMachine?
                .ChangeState(GamePhase.End);
        }

        [ContextMenu("Create Deck And Assign Role")]
        public void CreatePlayerAndAssignRole()
        {
            Deck deck = new Deck(cards);

            deck.Shuffle();

            if (simulation == null)
            {
                Debug.LogError("Simulatoin has not been created/assgined");

                return;
            }

            if (deck.Cards.Count != simulation.Players.Count)
            {
                Debug.LogError("Number of players is not as much as number of cards");
                return;
            }

            int index = 0;
            foreach (GamePlayer player in simulation.Players.Values)
            {
                player.AssignCard(CardFactory.CreateCard(deck.Cards[index]));
                Debug.Log($"{player.Name} assigned {player.Card.ToString()}");
                index++;
            }
        }

        [ContextMenu("Night 1 Action Test")]
        public void TestNight1()
        {
            CreatePlayerAndAssignRole();

            simulation.PhaseMachine.ChangeState(GamePhase.Night);

            ulong firstVillagerId = 0;
            ulong firstWolfId = 0;
            ulong firstSeerId = 0;
            ulong firstGuard = 0;

            foreach (var player_pair in simulation.Players)
            {
                switch (player_pair.Value.Card.Profile.Id)
                {
                    case CardId.Villager:
                        firstVillagerId = player_pair.Key;
                        break;
                    case CardId.Werewolf:
                        firstWolfId = player_pair.Key;
                        break;
                    case CardId.Seer:
                        firstSeerId = player_pair.Key;
                        break;
                    case CardId.Guard:
                        firstGuard = player_pair.Key;
                        break;


                }
            }

            Debug.Log($"village: {firstVillagerId}, wolf: {firstWolfId}, seer: {firstSeerId}, guard: {firstGuard}");
            simulation.SubmitNightAction(firstWolfId, 0, new List<ulong> { firstVillagerId });
            simulation.SubmitNightAction(firstGuard, 0, new List<ulong> { firstVillagerId });
            simulation.SubmitNightAction(firstSeerId, 0, new List<ulong> { firstWolfId });



            Debug.Log("Actions amount: " + simulation.nightActionManager.Actions.Count<NightAction>());
            Debug.Log(simulation.ResolveNight());

        }

        [ContextMenu("TestVote")]

        public void TestVote()
        {
            CreatePlayerAndAssignRole();

            simulation.PhaseMachine.ChangeState(GamePhase.Voting);

            ulong firstPlayerId = simulation.Players.ElementAt(0).Key;
            ulong secondPlayerId = simulation.Players.ElementAt(1).Key;
            ulong thirdPlayerId = simulation.Players.ElementAt(2).Key;

            simulation.SubmitVote(firstPlayerId, true, secondPlayerId);

            simulation.SubmitVote(thirdPlayerId, false, secondPlayerId);


            Debug.Log(simulation.ResolveMorning());
        }

        [ContextMenu("TestRealGame")]

        public void TestARealGame()
        {
            StartSimulation();

            CreatePlayerAndAssignRole();

            simulation.NextPhase();

            ulong firstVillagerId = GetPlayerByRole(CardId.Villager);
            ulong firstWolfId = GetPlayerByRole(CardId.Werewolf);
            ulong firstSeerId = GetPlayerByRole(CardId.Seer);
            ulong firstGuard = GetPlayerByRole(CardId.Guard);


            Debug.Log($"village: {firstVillagerId}, wolf: {firstWolfId}, seer: {firstSeerId}, guard: {firstGuard}");

            simulation.SubmitNightAction(firstWolfId, 0, new List<ulong> { firstVillagerId });
            simulation.SubmitNightAction(firstGuard, 0, new List<ulong> { firstSeerId });
            simulation.SubmitNightAction(firstSeerId, 0, new List<ulong> { firstWolfId });

            simulation.NextPhase();
            simulation.NextPhase();
            simulation.NextPhase();


            simulation.SubmitVote(firstWolfId, true, firstSeerId);
            simulation.SubmitVote(firstGuard, true, firstSeerId);


            simulation.NextPhase();

        }
        ulong GetPlayerByRole(CardId role)
        {
            foreach (var pair in simulation.Players)
            {
                if (pair.Value.Card.Profile.Id == role)
                    return pair.Key;
            }

            throw new Exception($"No player with role {role}");
        }
    }
}