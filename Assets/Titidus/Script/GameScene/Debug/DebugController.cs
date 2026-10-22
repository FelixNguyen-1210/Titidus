
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
    }
}
