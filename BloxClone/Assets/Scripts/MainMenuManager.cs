using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] public GameObject Menu;
    [SerializeField] public GameObject Chapters;
    private bool isChapter = false;

    // Update is called once per frame
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

    public void Exit()
    {
        Application.Quit();
    }
}
