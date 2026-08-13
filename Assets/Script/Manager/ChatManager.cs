using UnityEngine;
using Unity.Netcode;
using TMPro;
public class ChatManager : NetworkBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private GameObject messagePrefab;
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
