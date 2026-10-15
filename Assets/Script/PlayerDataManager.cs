using System.Xml.Linq;
using Titidus;
using UnityEngine;
using WebSocketSharp;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance;
    public string PlayerName;
    
    private void Awake()
    {
        MakeSingleton();
        PlayerName = PlayerPrefs.GetString(PlayerData.Name.ToString(), "");
        if (PlayerName.IsNullOrEmpty()) SetPlayerName(GenerateRandomName(5));
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

    public bool HasPlayerName() {
        return PlayerName == "";
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

        PlayerName = pName;

        PlayerPrefs.SetString(PlayerData.Name.ToString(), pName);
        PlayerPrefs.Save();
    }

    [ContextMenu("Clear PlayerPrefs")]
    private void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
