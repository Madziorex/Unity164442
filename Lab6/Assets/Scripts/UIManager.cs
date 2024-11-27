using TMPro;
using UnityEngine;
using UnityEngine.UI; // Do obs³ugi UI

public class UIManager : MonoBehaviour
{
    public PlayerData playerData; // Przypisz PlayerData przez inspektora w Unity
    public TextMeshProUGUI scoreText; // Przypisz obiekt UI Text dla wyniku
    public TextMeshProUGUI healthText; // Przypisz obiekt UI Text dla zdrowia
    public TextMeshProUGUI playerNameText; // Przypisz obiekt UI Text dla wyœwietlania nazwy gracza
    public TMP_InputField nameInputField; // Pole wejœciowe do ustawiania nazwy gracza

    private void Start()
    {
        // £aduj dane gracza z PlayerPrefs na pocz¹tku
        playerData.LoadData();
        UpdateUI();
        nameInputField.text = playerData.playerName;  // Ustaw pocz¹tkow¹ nazwê w polu tekstowym
    }

    private void Update()
    {
        // Zaktualizuj teksty wyniku i zdrowia w czasie rzeczywistym
        UpdateUI();

        // Zmieniaj dane przy pomocy klawiszy (do testowania)
        if (Input.GetKeyDown(KeyCode.Space)) // Zwiêksz wynik
        {
            playerData.playerScore += 10;
            UpdateUI();  // Aktualizuj UI po zmianach
        }

        if (Input.GetKeyDown(KeyCode.H)) // Zmniejsz zdrowie
        {
            playerData.playerHealth -= 10;
            UpdateUI();  // Aktualizuj UI po zmianach
        }
    }

    // Funkcja do aktualizowania UI
    private void UpdateUI()
    {
        // Aktualizuj tekst UI
        scoreText.text = "Score: " + playerData.playerScore.ToString();
        healthText.text = "Health: " + playerData.playerHealth.ToString();
        playerNameText.text = "Player: " + playerData.playerName;
    }

    // Funkcja do zmiany nazwy gracza
    public void SetPlayerName()
    {
        playerData.playerName = nameInputField.text;  // Ustaw nazwê gracza z InputField
        playerData.SaveData();  // Zapisz dane po zmianie nazwy
        UpdateUI();  // Zaktualizuj UI
    }
}