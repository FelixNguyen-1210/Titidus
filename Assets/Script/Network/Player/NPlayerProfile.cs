
namespace Titidus
{

    using UnityEngine;
    using Unity.Netcode;
    using Unity.Collections;

    public class NPlayerProfile : NetworkBehaviour
    {

        
        public NetworkVariable<FixedString64Bytes> PlayerName = new();
        public NetworkVariable<int> AvatarIndex = new();

        public override void OnNetworkSpawn()
        {
            PlayerName.OnValueChanged += OnNameChanged;
            AvatarIndex.OnValueChanged += OnAvatarChanged;

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


            // Host cập nhật dữ liệu để NGO tự đồng bộ.
            PlayerName.Value = new FixedString64Bytes(playerName);
            AvatarIndex.Value = avatarIndex;
        }

        private void OnNameChanged(
            FixedString64Bytes previous,
            FixedString64Bytes current)
        {
            RefreshUI();
        }

        private void OnAvatarChanged(int previous, int current)
        {
            RefreshUI();
        }

        public void RefreshUI()
        {
            if (!IsSpawned || SlotManager.Instance == null)
                return;

            string playerName = PlayerName.Value.ToString();

            if (string.IsNullOrEmpty(playerName))
                return;

            SlotManager.Instance.SetPlayer(
                OwnerClientId,
                playerName,
                AvatarIndex.Value
            );
        }

        public override void OnNetworkDespawn()
        {
            PlayerName.OnValueChanged -= OnNameChanged;
            AvatarIndex.OnValueChanged -= OnAvatarChanged;

            if (SlotManager.Instance != null)
                SlotManager.Instance.RemovePlayer(OwnerClientId);
        }
    }

}