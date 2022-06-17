using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstLevel,levelSelect;
    public GameObject continueButton;

    public string[] levelNames;

    void Start()
    {
        //继续按钮
        if(PlayerPrefs.HasKey("Continue")){
            continueButton.SetActive(true);
        }else{
            RestProgress();
        }
        
    }

    void Update()
    {
        
    }

    public void NewGame(){
        SceneManager.LoadScene(firstLevel);
        //继续按钮
        PlayerPrefs.SetInt("Continue",0);
        //start current level points
        PlayerPrefs.SetString("CurrentLevel",firstLevel);

        RestProgress();
    }

    public void Continue(){
        SceneManager.LoadScene(levelSelect);

    }

    public void QuitGame(){
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void RestProgress(){
        for(int i = 0;i < levelNames.Length; i++){
            PlayerPrefs.SetInt(levelNames[i]+ "_unlocked",0);
        }
    }
}
