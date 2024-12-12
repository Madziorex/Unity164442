using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]  public GameObject UI_Menu; // Referencja do menu

    private bool isPaused = false; // Czy gra jest wstrzymana?

    void Update()
    {
        // SprawdŸ, czy naciœniêto ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume2();
            }
            else
            {
                Pause();
            }
        }
    }

    // Funkcja wznawiaj¹ca grê
    public void Resume()
    {
        Debug.Log("Resuming Game");
        Time.timeScale = 1f;          // Wznów czas gry
        UI_Menu.SetActive(false);
        isPaused = false;
    }

    public void Resume2()
    {
        UI_Menu.SetActive(false); // Ukryj menu
        Time.timeScale = 1f;          // Wznów czas gry
        isPaused = false;
    }

    // Funkcja zatrzymuj¹ca grê
    public void Pause()
    {
        UI_Menu.SetActive(true); // Poka¿ menu
        Time.timeScale = 0f;         // Zatrzymaj czas gry
        isPaused = true;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Level1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Level2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void Level3()
    {
        SceneManager.LoadScene("Level3");
    }

    public void Level4()
    {
        SceneManager.LoadScene("Level4");
    }

    public void Level5()
    {
        SceneManager.LoadScene("Level5");
    }
}
