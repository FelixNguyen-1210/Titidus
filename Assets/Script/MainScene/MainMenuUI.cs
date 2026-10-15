using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
namespace Titidus
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] GameObject loadingPopup;
        [SerializeField] GameObject joinRoomDialog;
        [SerializeField] TMP_InputField playerNameInput;

        private void OnEnable()
        {
            if (playerNameInput && PlayerDataManager.Instance)
                playerNameInput.onEndEdit.AddListener(PlayerDataManager.Instance.SetPlayerName);
            else
            {
                Debug.LogWarning("Component missing");
            }
        }
        void Start()
        {
            if (playerNameInput && PlayerDataManager.Instance)
                playerNameInput.text = PlayerDataManager.Instance.PlayerName;
            else
            {
                Debug.LogWarning("Component missing");
            }
        }
        void CheckComponentsValid()
        {
        }
        public void CreateRoom()
        {

            RelayManager relayManager = RelayManager.Instance;
            if (!relayManager)
            {
                Debug.LogWarning("RelayManager not found");
                return;
            }

            if (relayManager.isBusy)
            {
                Debug.LogWarning("RelayManager is busy");
                return;
            }

            if (loadingPopup) loadingPopup.gameObject.SetActive(true);
            else Debug.LogWarning("Loading Icon Not Found");
            relayManager.CreateRelay();


        }
        public void JoinRoom()
        {
            joinRoomDialog.SetActive(true);
        }
    }
}