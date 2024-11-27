using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelSwitcher : MonoBehaviour
{
    public GameObject mainPanel; 
    public GameObject creditsPanel;
    public GameObject settingsPanel;

    public void ShowCredits()
    {
        mainPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void ShowMain()
    {
        creditsPanel.SetActive(false);
        settingsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void ShowSettings()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}