using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Image BlackScreen;
    public float fadeSpeed = 2f;
    public bool fadeToBlack,fadeFromBlack;

    public Text healthText;
    public Image healthImage;

    //添加金币文本
    public Text coinText;

    //暂停按钮,选择按钮
    public GameObject pauseScreen, optionsScreen;
    //声音滑块控制
    public Slider musicVolSlider,sfxVolSlider;

    public string levelSelect,mainMenu;

    private void Awake(){
        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeToBlack){
            BlackScreen.color = new Color(BlackScreen.color.r,BlackScreen.color.g,BlackScreen.color.b, Mathf.MoveTowards(BlackScreen.color.a,1f,fadeSpeed*Time.deltaTime));

            if(BlackScreen.color.a == 1f){
                fadeToBlack = false;
            }
        }

        if(fadeFromBlack){
            BlackScreen.color = new Color(BlackScreen.color.r,BlackScreen.color.g,BlackScreen.color.b, Mathf.MoveTowards(BlackScreen.color.a,0f,fadeSpeed*Time.deltaTime));

            if(BlackScreen.color.a == 0f){
                fadeFromBlack = false;
            }
        }
    }

    //各种按钮效果
    public void Resume(){
        GameManager.instance.PauseUnpause();
    }

    public void OpenOptions(){
        optionsScreen.SetActive(true);

    }

    public void CloseOptions(){
        optionsScreen.SetActive(false);

    }
    public void LevelSelect(){
        SceneManager.LoadScene(levelSelect);
        Time.timeScale = 1f;

    }
    public void MainMenu(){
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }

    public void SetMusicLevel(){
        AudioManager.instance.SetMusicLevel();

    }
    public void SetSFXLevel(){
        AudioManager.instance.SetSFXLevel();

    }
}
