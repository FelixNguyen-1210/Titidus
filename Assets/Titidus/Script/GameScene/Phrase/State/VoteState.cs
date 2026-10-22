using Titidus;
using UnityEngine;

namespace Titidus
{
    public class VoteState : GameState
    {
        public VoteState(GameSimulation simulation) : base(simulation)
        {

        }

        public override GamePhase Phase => GamePhase.Voting;

        public override void Enter()
        {
            base.Enter();
        }

    }
}