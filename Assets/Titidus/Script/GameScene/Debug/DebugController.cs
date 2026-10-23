
using UnityEngine;

namespace Titidus
{
    public class DebugGameController : MonoBehaviour
    {
        [SerializeField, Range(4, 12)]
        private int playerCount = 10;

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

        [ContextMenu("Test Kill Player")]
        public void TestKillPlayer()
        {
            if (simulation == null)
                return;

            var player = simulation.GetPlayer(0);

            player?.Kill();

            Debug.Log(
                $"Player 0 alive: {player?.IsAlive}"
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
            if(simulation == null) return;

            simulation.NextPhase();

        }

        [ContextMenu("Force End")]
        public void ForceEnd()
        {
            simulation?.PhaseMachine?
                .ChangeState(GamePhase.End);
        }
        [ContextMenu("Test Card Factory")]
        public void TestCardFactory()
        {
            Card card = CardFactory.CreateCard(CardId.Seer);

            Debug.Log($"Card: {card.Profile.Name}");
            Debug.Log($"Party: {card.Profile.Party}");
            Debug.Log($"Point: {card.Profile.Point}");
        }

        [ContextMenu("Test Ability")]

        public void TestAbility()
        {

            GamePlayer werewolf = new GamePlayer(0, "werewolf");
            GamePlayer village = new GamePlayer(1, "village");
            GamePlayer seer = new GamePlayer(2, "seer");



            werewolf.AssignCard(CardFactory.CreateCard(CardId.Werewolf));
            village.AssignCard(CardFactory.CreateCard(CardId.Villager));
            seer.AssignCard(CardFactory.CreateCard(CardId.Seer));

            IAbility seerAbility = seer.Card.Abilities[0];
            IAbility werewolfAbility = werewolf.Card.Abilities[0];

            Debug.Log("Before");
            
            seerAbility.Execute(simulation, seer, werewolf);
            werewolfAbility.Execute(simulation, werewolf, village);

            Debug.Log(village.ToString());

        }
    }
}
