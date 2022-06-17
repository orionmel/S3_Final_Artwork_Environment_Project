using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //创建GM例子
    public static GameManager instance;
    
    //设置重生位置
    private Vector3 respawnPosition;

    //死亡的粒子效果
    public GameObject deathEffect;

    //目前的金币数目
    public int currentCoins;

    public int levelEndMusic = 8;

    public string levelToLoad;

    //敌人respawing
    public bool isRespawning;

    //一个私有的函数
    private void Awake(){
        instance = this;
    }
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        respawnPosition = PlayerController.instance.transform.position;

        AddCoins(0);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //按下暂停
        if(Input.GetKeyDown(KeyCode.Escape)){
            PauseUnpause();

        }
        
    }

    //重生
    public void Respawn(){
        StartCoroutine(RespawnCo());
        //生命条再现
        HealthMananger.instance.PlayerKilled();
    }

    //产生时间差的重生
    public IEnumerator RespawnCo(){
        //让人物消失
        PlayerController.instance.gameObject.SetActive(false);

        //摄像机消失
        CameraController.instance.theCMBrain.enabled = false;

        //黑屏
        UIManager.instance.fadeToBlack = true;

        //实例化死亡粒子系统
        Instantiate(deathEffect,PlayerController.instance.transform.position + new Vector3(0f,1f,0f),PlayerController.instance.transform.rotation);

        yield return new WaitForSeconds(2f);

        //敌人respawing
        isRespawning = true;

        //重新获得生命数值
        HealthMananger.instance.ResetHealth();

        //不黑屏
        UIManager.instance.fadeFromBlack = true;

        //让人物重生
        PlayerController.instance.transform.position = respawnPosition;

        //摄像机重生
        CameraController.instance.theCMBrain.enabled = true;

        PlayerController.instance.gameObject.SetActive(true);

    }

    
    //从checkpoint重生
    public void SetSpawnPoint(Vector3 newSpawnPoint){
        respawnPosition = newSpawnPoint;
        Debug.Log("Spawn Point Set");
    }
    
    //添加金币
    public void AddCoins(int coinsToAdd){
        currentCoins += coinsToAdd;
        UIManager.instance.coinText.text = ""+currentCoins;
    }

    //按下暂停按钮
    public void PauseUnpause(){
        if(UIManager.instance.pauseScreen.activeInHierarchy){
            UIManager.instance.pauseScreen.SetActive(false);
            Time.timeScale = 1f;

            //鼠标消失
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

        }else{
            UIManager.instance.pauseScreen.SetActive(true);
            //关选择弹窗当再次按下esc键
            UIManager.instance.CloseOptions();
            Time.timeScale = 0f;

            //鼠标显示
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    //下一关
    public IEnumerator LevelEndCo(){
        AudioManager.instance.PlayMusic(levelEndMusic);
        PlayerController.instance.stopMove = true;
        UIManager.instance.fadeToBlack = true;

        yield return new WaitForSeconds(2f);
        Debug.Log("Level Ended");

        //unlocking levels
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked",1);

         if(PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_coins")){
             if(currentCoins > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_coins")){
                 PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_coins",currentCoins);
             }

         }else{
             PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_coins",currentCoins);
         }

        SceneManager.LoadScene(levelToLoad);
    }

}
