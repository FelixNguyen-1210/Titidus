using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Titidus
{
    public class SlotManager : MonoBehaviour
    {
        public static SlotManager Instance { get; private set; }

        private List<SlotUI> slots = new List<SlotUI>();
        private readonly Dictionary<ulong, (string name, int avatarIndex)> players = new();
        [SerializeField] private GameObject PlayerSlots;
        PlayerDataManager dataManager;
        private void Awake()
        {
            Instance = this;

            GetSlotsFromGameObject();

            foreach (var slot in slots)
                slot.Clear();
        }
        public void SetPlayer(
            ulong clientId,
            string playerName,
            int avatarIndex)
        {
            players[clientId] = (playerName, avatarIndex);
            RefreshSlots();
        }

        public void RemovePlayer(ulong clientId)
        {
            players.Remove(clientId);
            RefreshSlots();
        }

        private void Start()
        {

            dataManager = PlayerDataManager.Instance;

            // This script prevents the host's information from being
            // missing in the UI when the Profile calls RefreshUI()
            // before this script is initialized.

            var profiles = FindObjectsByType<NPlayer>();

            foreach (var profile in profiles)
                profile.RefreshUI();
        }

        private void GetSlotsFromGameObject()
        {
            if (!PlayerSlots)
            {

                Debug.LogError("PlayerSlots Transform is not found");
                return;
            }
            foreach (Transform child in PlayerSlots.transform)
            {
                if (child.TryGetComponent<SlotUI>(out var slot))
                {
                    slots.Add(slot);
                }
            }

        }
        private void RefreshSlots()
        {
            foreach (var slot in slots)
                slot.Clear();

            int slotIndex = 0;

            if (!dataManager) return;
            // ClientId nhỏ hơn được hiển thị trước.
            foreach (var player in players.OrderBy(p => p.Key))
            {
                if (slotIndex >= slots.Count)
                    break;

                var data = player.Value;

                Sprite avatar = dataManager.GetAvatar(data.avatarIndex);

                slots[slotIndex].SetPlayer(data.name, avatar);
                slotIndex++;
            }
        }
        private void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }
    }
}