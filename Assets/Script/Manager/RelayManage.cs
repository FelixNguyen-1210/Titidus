using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.UI;

public class RelayManager : MonoBehaviour
{
    [SerializeField] Button hostBtn;
    [SerializeField] Button joinBtn;

    [SerializeField] TMP_InputField joinInput;



    async void Start()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        hostBtn.onClick.AddListener(CreateRelay);
        joinBtn.onClick.AddListener(() => JoinRelay(joinInput.text));
    }

    public async void CreateRelay()
    {
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(3);
        string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
        Debug.Log(joinCode);
        var relayServerData = AllocationUtils.ToRelayServerData(allocation, "dtls");

        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

        NetworkManager.Singleton.StartHost();

    }
    public async void JoinRelay(string joinCode)
    {

        var joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
        var relayServerData = AllocationUtils.ToRelayServerData(joinAllocation, "dtls");
        Debug.Log("[JOIN] Relay allocation SUCCESS");
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
        NetworkManager.Singleton.StartClient();

    }
}
