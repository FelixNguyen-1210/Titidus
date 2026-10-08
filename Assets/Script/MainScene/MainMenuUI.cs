using UnityEngine;
using UnityEngine.UI;
namespace Titidus
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] GameObject loadingPopup;
        [SerializeField] GameObject joinRoomDialog;


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