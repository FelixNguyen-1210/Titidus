using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;


namespace Titidus
{
    public class PlayerDataManager : MonoBehaviour
    {
        public static PlayerDataManager Instance;
        [SerializeField] string m_playerName;
        [SerializeField] int m_playerAvatarIndex;
        public string PlayerName { get => m_playerName; }
        public int PlayerAvatarIndex { get => m_playerAvatarIndex; }

        [SerializeField] List<Sprite> avatar = new List<Sprite>();

        private void Awake()
        {
            MakeSingleton();

            m_playerName = PlayerPrefs.GetString(PlayerData.Name.ToString(), "");
            m_playerAvatarIndex = PlayerPrefs.GetInt(PlayerData.AvatarIndex.ToString(), 0);

            if (m_playerName.IsNullOrEmpty()) SetPlayerName(GenerateRandomName(5));
        }
        void MakeSingleton()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        public Sprite GetAvatar(int index)
        {
            return avatar[index];
        }
        public bool HasPlayerName()
        {
            return m_playerName == "";
        }
        private string GenerateRandomName(int length = 10)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] result = new char[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = chars[Random.Range(0, chars.Length)];
            }

            return new string(result);
        }
        public void SetPlayerName(string pName)
        {
            pName = pName.Trim();

            if (string.IsNullOrEmpty(pName))
                return;

            m_playerName = pName;

            PlayerPrefs.SetString(PlayerData.Name.ToString(), pName);
            PlayerPrefs.Save();
        }


        public void SetAvatarIndex(int index)
        {
            if (index < 0) return;

            m_playerAvatarIndex = index;

            PlayerPrefs.SetInt(PlayerData.AvatarIndex.ToString(), index);
            PlayerPrefs.Save();
        }

        [ContextMenu("Clear PlayerPrefs")]
        private void ClearPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}