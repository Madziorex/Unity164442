using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] public GameObject Menu;
    [SerializeField] public GameObject Chapters;
    [SerializeField] public GameObject Credits;
    [SerializeField] public GameObject Settings;
    private bool isChapter = false;
    private bool isCredits = false;
    private bool isSettings = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isChapter)
            {
                Chapters.SetActive(false);
                Menu.SetActive(true);
                isChapter = false;
            }

            if (isCredits)
            {
                Credits.SetActive(false);
                Menu.SetActive(true);
                isCredits = false;
            }

            if (isSettings)
            {
                Settings.SetActive(false);
                Menu.SetActive(true);
                isSettings = false;
            }
        }
    }

    public void NewGame()
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

    public void ChapterSelect()
    {
        Chapters.SetActive(true);
        Menu.SetActive(false);
        isChapter = true;
    }

    public void CreditsSelect()
    {
        Credits.SetActive(true);
        Menu.SetActive(false);
        isCredits = true;
    }

    public void SettingsSelect()
    {
        Settings.SetActive(true);
        Menu.SetActive(false);
        isSettings = true;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
