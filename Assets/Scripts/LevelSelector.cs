using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
     public Button[] levels;
     public int levelReached;

    public void PlayGame(){
        SceneManager.LoadScene(levelReached);
    }

    public void ResetGame(){
        PlayerPrefs.SetInt("level", 1);
        PlayerPrefs.Save();
        levelReached = PlayerPrefs.GetInt("level");
        Debug.Log("Game succesfully reset to lvl 1");

        for (int i = 0; i < levels.Length; i++){
            if (i+1>levelReached){
                levels[i].interactable = false;
            }
        }
    }

    public void UnlockAll(){
        int sceneNumber = SceneManager.sceneCountInBuildSettings;
        levelReached = sceneNumber - 1;
        PlayerPrefs.SetInt("level", levelReached);
        PlayerPrefs.Save();
        Debug.Log("All levels unlocked");


        for (int i = 0; i < levels.Length; i++){
            if (i+1>levelReached){
                levels[i].interactable = false;
            }
            else{
                levels[i].interactable = true;
            }
        }
    }

    private void Start(){
        if(!PlayerPrefs.HasKey("level")) {
            PlayerPrefs.SetInt("level", 1);
            PlayerPrefs.Save();
            Debug.Log("Created new Pref called level");
        }
        else levelReached = PlayerPrefs.GetInt("level");

        for (int i = 0; i < levels.Length; i++){
            if (i+1>levelReached){
                levels[i].interactable = false;
            }
        }
    }

    public void Select(int numberInBuild){
        SceneManager.LoadScene(numberInBuild);
    }
}
