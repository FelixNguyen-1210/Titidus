using Titidus;
using UnityEngine;

namespace Titidus
{
    public class EndState : GameState
    {
        public EndState(GameSimulation simulation) : base(simulation)
        {

        }

        public override GamePhase Phase => GamePhase.End;

        public override void Enter()
        {
            base.Enter();
        }

    }
}