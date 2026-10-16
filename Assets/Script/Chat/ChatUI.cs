using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Titidus
{
    public class ChatUI : MonoBehaviour
    {
        [SerializeField] private TMP_InputField input;
        [SerializeField] private NChatManager chatManager;
        [SerializeField] private Button sendBtn;

        private void OnEnable()
        {
            sendBtn.onClick.AddListener(Send);
        }
        private void OnDisable()
        {
            sendBtn.onClick.RemoveAllListeners();
        }
        void Send()
        {
            Debug.Log("Send");

            if (string.IsNullOrWhiteSpace(input.text))
                return;
            chatManager.SendMessageRpc(input.text);
            input.text = "";
            input.ActivateInputField();
        }
    }
}