using Unity.Netcode;
using UnityEngine;

namespace Titidus
{
    public class NetworkPlayer : NetworkBehaviour
    {
        public NetworkVariable<bool> IsReady =
                new NetworkVariable<bool>(false);

        [Rpc(SendTo.Server)]
        public void SetReadyRpc()
        {
            IsReady.Value = true;

            GameManager.Instance.CheckAllPlayersReady();
        }
    }
}