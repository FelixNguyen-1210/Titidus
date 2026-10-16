using System.Linq;
using TMPro;
using Unity.Netcode;
using UnityEngine;

namespace Titidus
{
    public class NChatManager : NetworkBehaviour
    {


        [SerializeField] private Transform content;
        [SerializeField] private GameObject messagePrefab;

        public static NChatManager Instance;
        void Awake()
        {
            Instance = this;
        }
        [Rpc(SendTo.Server, InvokePermission = RpcInvokePermission.Everyone)]
        public void SendMessageRpc(string message, RpcParams rpcParams = default)
        {

            //Now scan all profile each time chat to decide player but
            //the maximum player only 12 so it'll workwell, otherwise we
            //can use dictionary

            string playerName = GetPlayerName(rpcParams.Receive.SenderClientId);

            message = $"{playerName}: <color=black>{message}</color>" ;
            ReceiveMessageRpc(message);
        }
        string GetPlayerName(ulong senderId)
        {

            var profile = FindObjectsByType<NPlayerProfile>().FirstOrDefault(p =>
                p.IsSpawned &&
                p.OwnerClientId == senderId
            );

            return profile == null
                ? ""
                : profile.PlayerName.Value.ToString();
        }

        [Rpc(SendTo.Everyone)]
        private void ReceiveMessageRpc(string message)
        {
            GameObject obj = Instantiate(messagePrefab, content);

            obj.GetComponent<TMP_Text>().text = message;
        }

        [Rpc(SendTo.Server)]
        public void PlayerJoinedRpc(RpcParams rpcParams = default)
        {
            string message = $"<color=green>{GetPlayerName(rpcParams.Receive.SenderClientId)} đã tham gia phòng!</color>";

            ReceiveMessageRpc(message);
        }
    }
}