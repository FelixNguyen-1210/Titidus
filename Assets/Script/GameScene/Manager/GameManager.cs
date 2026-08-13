using Unity.Netcode;
using UnityEngine;

namespace Titidus
{
    public class GameManager : NetworkBehaviour
    {

        public static GameManager Instance;
        public NetworkVariable<GamePhase> CurrentPhase =
            new NetworkVariable<GamePhase>(GamePhase.Prepare);

        void Awake()
        {
            Instance = this;
        }
        public override void OnNetworkSpawn()
        {
            CurrentPhase.OnValueChanged += OnPhaseChanged;

            // Client vừa vào cũng update UI theo phase hiện tại
            OnPhaseChanged(CurrentPhase.Value, CurrentPhase.Value);

            if (IsServer)
            {
                StartPreparePhase();
            }
        }
        private void StartPreparePhase()
        {
            CurrentPhase.Value = GamePhase.Prepare;

            AssignRoles();
        }

        private void AssignRoles()
        {
            // Sau này viết phần random role ở đây
            Debug.Log("Assign roles");
        }

        private void OnPhaseChanged(GamePhase oldPhase, GamePhase newPhase)
        {
            Debug.Log($"Phase: {oldPhase} -> {newPhase}");

            switch (newPhase)
            {
                case GamePhase.Prepare:
                    break;

                case GamePhase.Morning:
                    break;

                case GamePhase.Discussion:
                    break;

                case GamePhase.Voting:
                    break;

                case GamePhase.Night:
                    break;

                case GamePhase.End:
                    break;
            }
        }
        public void CheckAllPlayersReady()
        {
            if (!IsServer)
                return;

            foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
            {
                NetworkObject playerObject = client.PlayerObject;

                if (playerObject == null)
                    return;

                NetworkPlayer player =
                    playerObject.GetComponent<NetworkPlayer>();

                if (player == null || !player.IsReady.Value)
                    return;
            }

            // Nếu chạy tới đây nghĩa là tất cả đều Ready
            StartMorningPhase();
        }

        private void StartMorningPhase()
        {
            CurrentPhase.Value = GamePhase.Morning;
        }
    }

}