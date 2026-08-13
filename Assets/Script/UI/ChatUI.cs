using TMPro;
using UnityEngine;

public class ChatUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField input;
    [SerializeField] private ChatManager chatManager;

    public void Send()
    {
        if (string.IsNullOrWhiteSpace(input.text))
            return;
        chatManager.SendMessageRpc(input.text);

        input.text = "";
        input.ActivateInputField();
    }
}
