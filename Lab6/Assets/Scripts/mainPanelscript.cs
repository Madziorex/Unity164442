using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainPanelscript : MonoBehaviour
{
    [SerializeField] private GameObject MainPanel;
    [SerializeField] private GameObject settingsPanel;
    public void SettingsPanelActivate()
    {
        MainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
}
