using Titidus;
using UnityEngine;

namespace Titidus
{
    public class DiscussState : GameState
    {
        public DiscussState(GameSimulation simulation) : base(simulation)
        {

        }

        public override GamePhase Phase => GamePhase.Discussion;

        public override void Enter()
        {
            base.Enter();
        }

    }
}