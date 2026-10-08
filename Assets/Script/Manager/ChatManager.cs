using UnityEngine;
using Unity.Netcode;
using TMPro;

namespace Titidus
{
    public class ChatManager : NetworkBehaviour
    {
        [SerializeField] private Transform content;
        [SerializeField] private GameObject messagePrefab;

        public static ChatManager Instance;

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
        [Rpc(SendTo.Server)]
        public void SendMessageRpc(string message)
        {
            Debug.Log($"SERVER RECEIVED: {message}");
            ReceiveMessageRpc(message);
        }

        [Rpc(SendTo.Everyone)]
        private void ReceiveMessageRpc(string message)
        {
            Debug.Log($"CLIENT RECEIVED: {message}");
            GameObject obj = Instantiate(messagePrefab, content);

            obj.GetComponent<TMP_Text>().text = message;
        }
    }
}