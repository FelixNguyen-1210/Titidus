using System;
using System.Threading.Tasks;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

namespace Titidus
{

    public class RelayManager: MonoBehaviour
    {

        [SerializeField] TMP_InputField joinInput;
        public static RelayManager Instance;
        private string m_roomCode = null;
        private bool isBusy = false;
        public string roomCode { get => m_roomCode; }

        async void Start()
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

        }


        void Awake()
        {
            MakeSingleton();
        }

        void MakeSingleton()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public async void CreateRelay()
        {
            if (isBusy) return;

            try
            {
                isBusy = true;

                Allocation allocation = await RelayService.Instance.CreateAllocationAsync(3);
                string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
                m_roomCode = joinCode;
                Debug.Log(m_roomCode);
                var relayServerData = AllocationUtils.ToRelayServerData(allocation, "dtls");

                NetworkManager network = NetworkManager.Singleton;
                network.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

                if (network.StartHost())
                {
                    network.SceneManager.LoadScene(
                        "RoomScene",
                        UnityEngine.SceneManagement.LoadSceneMode.Single
                    );
                }

                isBusy = false;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Create Relay failed: {e}");
            }
            finally
            {
                isBusy = false;
            }

        }
        public void ShutdownRelay()
        {
            if (NetworkManager.Singleton)
            {
                m_roomCode = null;
                NetworkManager.Singleton.Shutdown();
            }
        }



        public async Task JoinRelay(string joinCode)
        {
            if (isBusy) return;
            isBusy = true;

            var joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
            var relayServerData = AllocationUtils.ToRelayServerData(joinAllocation, "dtls");

            NetworkManager manager = NetworkManager.Singleton;
            manager.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);


            
            manager.OnClientConnectedCallback -= OnClientConnected;
            manager.OnClientConnectedCallback += OnClientConnected;

            if (!NetworkManager.Singleton.StartClient())
            {
                throw new System.Exception("Failed to start client.");
            }
            isBusy = false;
            m_roomCode = joinCode.Trim().ToUpperInvariant();
        }

        private void OnClientConnected(ulong clientId)
        {
            if (clientId != NetworkManager.Singleton.LocalClientId)
                return;
            
            NChatManager.Instance.PlayerJoinedRpc();

            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
        }
    }
}