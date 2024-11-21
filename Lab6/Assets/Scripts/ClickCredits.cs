using UnityEngine;

public class PanelSwitcher : MonoBehaviour
{
    public GameObject mainPanel; 
    public GameObject creditsPanel;
    public GameObject settingsPanel;
    public GameObject RightPanel;
    public GameObject TopPanel;
    public GameObject BotPanel;
    public GameObject LeftPanel;
    public GameObject WinPanel;

    public void ShowCredits()
    {
        mainPanel.SetActive(false);
        LeftPanel.SetActive(false);
        RightPanel.SetActive(false);
        TopPanel.SetActive(false);
        BotPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void ShowMain()
    {
        creditsPanel.SetActive(false);
        settingsPanel.SetActive(false);
        WinPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void ShowSettings()
    {
        mainPanel.SetActive(false);
        LeftPanel.SetActive(false);
        RightPanel.SetActive(false);
        TopPanel.SetActive(false);
        BotPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void ShowLvl1()
    {
        mainPanel.SetActive(false);
        TopPanel.SetActive(true);
    }

    public void ShowLvl2()
    {
        TopPanel.SetActive(false);
        RightPanel.SetActive(true);
    }

    public void ShowLvl3()
    {
        RightPanel.SetActive(false);
        BotPanel.SetActive(true);
    }

    public void ShowLvl4()
    {
        BotPanel.SetActive(false);
        LeftPanel.SetActive(true);
    }

    public void ShowWin()
    {
        LeftPanel.SetActive(false);
        WinPanel.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }


}