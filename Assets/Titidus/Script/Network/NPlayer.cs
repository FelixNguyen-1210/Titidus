
namespace Titidus
{

    using UnityEngine;
    using Unity.Netcode;
    using Unity.Collections;

    public class NPlayer : NetworkBehaviour
    {

        
        public NetworkVariable<PlayerProfile> playerProfile = new();

        public override void OnNetworkSpawn()
        {
            playerProfile.OnValueChanged += OnProfileChanged;

            RefreshUI();

            // Chỉ chủ sở hữu PlayerObject gửi dữ liệu cá nhân.
            if (IsOwner)
                UploadLocalProfile();
        }

        public void UploadLocalProfile()
        {
            if (!IsSpawned || !IsOwner)
                return;

            var data = PlayerDataManager.Instance;

            if (data == null)
            {
                Debug.LogError("PlayerDataManager not found!");
                return;
            }

            SubmitProfileRpc(data.PlayerName, data.PlayerAvatarIndex);
        }

        [Rpc(SendTo.Server)]
        private void SubmitProfileRpc(string playerName, int avatarIndex)
        {
            playerName = (playerName ?? "").Trim();

            PlayerProfile profile = playerProfile.Value;

            profile.name = new FixedString64Bytes(playerName);
            profile.avt_index = avatarIndex;

            playerProfile.Value = profile;
        }

        private void OnProfileChanged(PlayerProfile previous, PlayerProfile current)
        {
            RefreshUI();

        }
        public void RefreshUI()
        {
            if (!IsSpawned || SlotManager.Instance == null)
                return;

            string playerName = playerProfile.Value.name.ToString();
            int avtIndex = playerProfile.Value.avt_index;
            if (string.IsNullOrEmpty(playerName))
                return;

            SlotManager.Instance.SetPlayer(
                OwnerClientId,
                playerName,
                avtIndex
            );
        }

        public override void OnNetworkDespawn()
        {
            playerProfile.OnValueChanged -= OnProfileChanged;


            if (SlotManager.Instance != null)
                SlotManager.Instance.RemovePlayer(OwnerClientId);
        }
    }

}