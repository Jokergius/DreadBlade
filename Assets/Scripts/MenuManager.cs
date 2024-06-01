using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject levelPanel;

    public void SettingPanel(){
        settingsPanel.SetActive(true);
    }
    public void LevelPanel(){
        levelPanel.SetActive(true);
    }
    public void setExit(){
        settingsPanel.SetActive(false);
    }
    public void levExit(){
        levelPanel.SetActive(false);
    }

    public void ExitGame(){
        Application.Quit();
    }
}
