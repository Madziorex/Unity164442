using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerData playerData;  // Przypisz `PlayerDataInstance` przez Inspektor w Unity

    private void Start()
    {
        // £adowanie danych na starcie
        playerData.LoadData();
        Debug.Log("Player Name: " + playerData.playerName);
        Debug.Log("Player Score: " + playerData.playerScore);
        Debug.Log("Player Health: " + playerData.playerHealth);
    }

    private void OnApplicationQuit()
    {
        // Zapisz dane przy zamykaniu aplikacji
        playerData.SaveData();
    }

    private void OnDestroy()
    {
        // Zapisz dane przy niszczeniu obiektu (np. zmiana sceny)
        playerData.SaveData();
    }

    // Przyk³adowa funkcja modyfikuj¹ca dane
    public void IncreaseScore(int amount)
    {
        playerData.playerScore += amount;
        Debug.Log("Nowy wynik gracza: " + playerData.playerScore);
    }

    public void ResetPlayer()
    {
        playerData.ResetData();
        Debug.Log("Dane gracza zosta³y zresetowane.");
    }
}
