using Titidus;
using UnityEngine;
using UnityEngine.UI;

public class RoomSceneUI : MonoBehaviour
{
    [SerializeField] Button LeaveBtn;

    void OnEnable()
    {
        LeaveBtn.onClick.AddListener(() => Leave());
    }

    private void Leave()
    {
        ConnectionHandler.Instance.LeaveRoom();
        Debug.Log("Leave");

    }
}
