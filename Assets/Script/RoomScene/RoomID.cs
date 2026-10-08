using Titidus;
using TMPro;
using UnityEngine;

namespace Titidus
{
    public class RoomID : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI idTxt;
        void Start()
        {
            if (RelayManager.Instance)
            {
                if (idTxt)
                {
                    idTxt.text = RelayManager.Instance.roomCode.ToUpperInvariant();
                }
            }
            else
            {
                Debug.LogWarning("RelayManager not found");
            }
        }

    }
}