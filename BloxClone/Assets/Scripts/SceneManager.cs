using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MySceneManager : MonoBehaviour
{
    [SerializeField] public GameObject UI_Hints;

    private bool isHints = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
        else if(Input.GetKeyDown(KeyCode.H))
        {
            if (isHints)
            {
                CloseHint();
            }
            else
            {
                ShowHint();
            }
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowHint()
    {
        UI_Hints.SetActive(true);
        Time.timeScale = 0f;
        isHints = true;
    }

    public void CloseHint()
    {
        UI_Hints.SetActive(false);
        Time.timeScale = 1f;
        isHints = false;
    }
}
