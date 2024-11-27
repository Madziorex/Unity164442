using TMPro;
using UnityEngine;
using UnityEngine.UI; // Do obs�ugi UI

public class UIManager : MonoBehaviour
{
    public PlayerData playerData; // Przypisz PlayerData przez inspektora w Unity
    public TextMeshProUGUI scoreText; // Przypisz obiekt UI Text dla wyniku
    public TextMeshProUGUI healthText; // Przypisz obiekt UI Text dla zdrowia
    public TextMeshProUGUI playerNameText; // Przypisz obiekt UI Text dla wy�wietlania nazwy gracza
    public TMP_InputField nameInputField; // Pole wej�ciowe do ustawiania nazwy gracza

    private void Start()
    {
        // �aduj dane gracza z PlayerPrefs na pocz�tku
        playerData.LoadData();
        UpdateUI();
        nameInputField.text = playerData.playerName;  // Ustaw pocz�tkow� nazw� w polu tekstowym
    }

    private void Update()
    {
        // Zaktualizuj teksty wyniku i zdrowia w czasie rzeczywistym
        UpdateUI();

        // Zmieniaj dane przy pomocy klawiszy (do testowania)
        if (Input.GetKeyDown(KeyCode.Space)) // Zwi�ksz wynik
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
        playerData.playerName = nameInputField.text;  // Ustaw nazw� gracza z InputField
        playerData.SaveData();  // Zapisz dane po zmianie nazwy
        UpdateUI();  // Zaktualizuj UI
    }
}