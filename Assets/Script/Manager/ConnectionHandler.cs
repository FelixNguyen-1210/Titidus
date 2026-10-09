using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Titidus
{
    public class ConnectionHandler : MonoBehaviour
    {

        private NetworkManager manager;
        private bool isLeaving;
        public static ConnectionHandler Instance;
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

        void Start()
        {
            manager = NetworkManager.Singleton;
            manager.OnClientDisconnectCallback += OnClientDisconnected;
        }
        void OnDestroy()
        {
            if (manager != null)
            {
                manager.OnClientDisconnectCallback -= OnClientDisconnected;
            }
        }
        void OnClientDisconnected(ulong clientId)
        {
            if (manager.IsServer) return;
            if (clientId != manager.LocalClientId) return;
            if (isLeaving) return;

            _ = HandleHostDisconnected();
        }

        private async Task HandleHostDisconnected()
        {
            isLeaving = true;

            Debug.Log("Connection to host lost!");

            await ShutdownAndReturn();

            // Hiển thị thông báo ở Main Menu
            Debug.Log("Host has left the room.");
        }
        public async void LeaveRoom()
        {
            if (isLeaving) return;

            isLeaving = true;

            await ShutdownAndReturn();
        }

        private async Task ShutdownAndReturn()
        {
            if (manager.IsListening && !manager.ShutdownInProgress)
            {
                manager.Shutdown();
            }

            while (manager.ShutdownInProgress)
            {
                await Task.Yield();
            }
            SceneManager.LoadScene("MainScene");
            isLeaving = false;
        }
    }
}