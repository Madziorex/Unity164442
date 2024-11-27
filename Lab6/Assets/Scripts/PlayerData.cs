using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    public string playerName = "Player"; // Domyœlna nazwa gracza
    public int playerScore = 0;
    public int playerHealth = 100;

    // Klucz do przechowywania danych w PlayerPrefs w formacie JSON
    private const string PlayerDataKey = "PlayerData";

    // Funkcja do za³adowania danych z PlayerPrefs jako JSON
    public void LoadData()
    {
        if (PlayerPrefs.HasKey(PlayerDataKey))
        {
            string jsonData = PlayerPrefs.GetString(PlayerDataKey);  // Pobierz dane JSON z PlayerPrefs
            JsonUtility.FromJsonOverwrite(jsonData, this);  // Przekszta³æ JSON na dane obiektu
        }
        else
        {
            // Ustaw domyœlne wartoœci, jeœli dane nie istniej¹
            playerName = "Player";
            playerScore = 0;
            playerHealth = 100;
        }
    }

    // Funkcja do zapisania danych do PlayerPrefs jako JSON
    public void SaveData()
    {
        string jsonData = JsonUtility.ToJson(this);  // Serializowanie danych obiektu do formatu JSON
        PlayerPrefs.SetString(PlayerDataKey, jsonData);  // Zapisanie JSON w PlayerPrefs
        PlayerPrefs.Save();  // Zapisz wszystkie zmiany w PlayerPrefs
    }

    public void ClearData()
    {
        PlayerPrefs.DeleteKey(PlayerDataKey);  // Usuwanie danych gracza z PlayerPrefs
        Debug.Log("PlayerData zosta³y usuniête.");
    }

    public void ResetData()
    {
        playerName = "Player";
        playerScore = 0;
        playerHealth = 100;
        SaveData();  // Zapisz dane po resecie
        Debug.Log("Dane zosta³y zresetowane.");
    }
}