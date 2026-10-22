using Titidus;
using UnityEngine;

namespace Titidus
{
    public class NightState : GameState
    {
        public NightState(GameSimulation simulation) : base(simulation)
        {

        }

        public override GamePhase Phase => GamePhase.Night;

        public override void Enter()
        {
            base.Enter();
        }

    }
}
