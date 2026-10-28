
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Titidus
{
    public class DebugGameController : MonoBehaviour
    {
        [SerializeField, UnityEngine.Range(4, 12)]
        private int playerCount = 5;
        [SerializeField] List<CardId> cards = new();
        private GameSimulation simulation;

        [ContextMenu("Start Simulation")]
        public void StartSimulation()
        {
            simulation = new GameSimulation();

            simulation.InitializePhases();



            for (int i = 0; i < playerCount; i++)
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
            StartSimulation();

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
                Debug.Log($"{player.Name}: {player.Card.ToString()}");
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
    }
}
