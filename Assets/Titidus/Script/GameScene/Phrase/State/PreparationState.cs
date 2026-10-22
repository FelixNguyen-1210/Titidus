using Titidus;
using UnityEngine;

namespace Titidus
{
    public class PreparationState : GameState
    {
        public PreparationState(GameSimulation simulation) : base(simulation)
        {

        }

        public override GamePhase Phase => GamePhase.Preparation;

        public override void Enter()
        {
            base.Enter();
        }

    }
}