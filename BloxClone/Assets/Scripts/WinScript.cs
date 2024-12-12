using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        // Sprawd�, czy obiekt koliduj�cy to gracz
        if (other.CompareTag("Player"))
        {
            audioSource.Play();
            Debug.Log("Gratulacje! Gracz wygra�!");

            // Rozpocznij �adowanie kolejnej sceny po 5 sekundach
            StartCoroutine(LoadNextSceneAfterDelay());
        }
    }

    private IEnumerator LoadNextSceneAfterDelay()
    {
        // Odczekaj 5 sekund
        yield return new WaitForSeconds(5f);

        // Pobierz nazw� aktualnej sceny
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Decyduj, kt�ra scena ma si� za�adowa�
        switch (currentSceneName)
        {
            case "Level1":
                SceneManager.LoadScene("Level2");
                break;
            case "Level2":
                SceneManager.LoadScene("Level3");
                break;
            case "Level3":
                SceneManager.LoadScene("Level4");
                break;
            case "Level4":
                SceneManager.LoadScene("Level5");
                break;
            case "Level5":
                SceneManager.LoadScene("MainMenu");
                break;
            default:
                Debug.LogWarning("Nieznana scena: " + currentSceneName);
                break;
        }
    }
}