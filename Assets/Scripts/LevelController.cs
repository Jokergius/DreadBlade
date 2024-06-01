using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private int enemiesOnScene;
    private int levelReached;

    [SerializeField] GameObject NextButton;

    public static LevelController Instance{get; set;}

    private void Awake(){
        Instance = this;
        
    }

    public void NextLevel(){
        SceneManager.LoadScene(levelReached);
        Time.timeScale = 1;
    }

    public void EnemiesCount(){
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        enemiesOnScene = enemies.Length;
        Debug.Log("Осталось мобов: " + enemiesOnScene);

        if(enemiesOnScene == 0){
            Hero.Instance.Invoke("SetWinPanel", 1.1f);

            levelReached = PlayerPrefs.GetInt("level");
            if(levelReached == SceneManager.GetActiveScene().buildIndex){
                levelReached++;
            }
            int sceneNumber = SceneManager.sceneCountInBuildSettings;
            if(levelReached>sceneNumber-1){
                levelReached--;
                PlayerPrefs.SetInt("level", levelReached);
                PlayerPrefs.Save();
                NextButton.SetActive(false);
            }
            else{
                PlayerPrefs.SetInt("level", levelReached);
                PlayerPrefs.Save();
            }
            Debug.Log("Кол-во " + sceneNumber);
        }
    }
}
