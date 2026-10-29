using Titidus;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

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
            
            MorningResult result = simulation.ResolveMorning();
            if (!CheckConditionToContinue())
            {
                simulation.PhaseMachine.ChangeState(GamePhase.End);
                return;
            }
            
            PrintDebug(result);



        }

        public void PrintDebug(MorningResult result)
        {

            Debug.Log($"{result.deadPlayerId} has been hanged");

            foreach(var player in simulation.Players.Values)
            {
                Debug.Log($"{player.Id} - Alive:{player.IsAlive}");
            }

        }
        public override void Exit()
        {
            base.Exit();
        }

        public bool CheckConditionToContinue() 
        {            
            int numVillages = simulation.CountPartyMembers(Party.Village);
            int numWolf = simulation.CountPartyMembers(Party.Wolf);

            if (numVillages <= numWolf)
            {
                Debug.Log("End game! wolf more than village");
                return false;
            }
            if (numWolf <= 0)
            {
                Debug.Log("End game! nums wolf is 0");
                return false;
            }

            return true;

        }
    }
}