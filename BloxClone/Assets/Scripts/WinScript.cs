using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField] OneController OneController;

    private void OnTriggerEnter(Collider other)
    {
        OneController oneController = FindObjectOfType<OneController>();
        if (other.CompareTag("Player") && oneController.isDead == false)
        {
            audioSource.Play();
            Debug.Log("Gratulacje! Gracz wygra³!");

            StartCoroutine(LoadNextSceneAfterDelay());
        }
    }

    private IEnumerator LoadNextSceneAfterDelay()
    {
        yield return new WaitForSeconds(3f);

        string currentSceneName = SceneManager.GetActiveScene().name;

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