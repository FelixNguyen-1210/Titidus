using Titidus;
using UnityEngine;

namespace Titidus
{
    public class MorningState : GameState
    {
        public MorningState(GameSimulation simulation) : base(simulation)
        {

        }

        public override GamePhase Phase => GamePhase.Morning;

        public override void Enter()
        {
            base.Enter();
        }

    }
}