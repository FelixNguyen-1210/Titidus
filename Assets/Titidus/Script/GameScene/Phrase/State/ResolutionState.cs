using Titidus;
using UnityEngine;

namespace Titidus
{
    public class ResolutionState: GameState
    {
        public ResolutionState(GameSimulation simulation) : base(simulation)
        {

        }

        public override GamePhase Phase => GamePhase.Resolution;

        public override void Enter()
        {

            base.Enter();
        }

        public override void Exit()
        {
            simulation.voteManager.Clear();
            base.Exit();
        }
            
        
    }
}