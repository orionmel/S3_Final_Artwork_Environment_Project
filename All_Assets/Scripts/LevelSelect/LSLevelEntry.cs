using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LSLevelEntry : MonoBehaviour
{
    public string levelName,levelToCheck,displayName;
    private bool canLoadLevel,levelUnlocked;

    public GameObject mapPointActive,mapPointInactive;

    private bool levelLoading;

    void Start()
    {
        //unlocking levels
        if(PlayerPrefs.GetInt(levelToCheck + "_unlocked") == 1 || levelToCheck == ""){
            mapPointActive.SetActive(true);
            mapPointInactive.SetActive(false);
            levelUnlocked = true;
        }else{
            mapPointActive.SetActive(false);
            mapPointInactive.SetActive(true);
            levelUnlocked = false;
        }

        if(PlayerPrefs.GetString("CurrentLevel") == levelName){
            PlayerController.instance.transform.position = transform.position;
            //重新回到目标点
            LSResetPosition.instance.respawnPosition = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump") && canLoadLevel && levelUnlocked && !levelLoading){
            StartCoroutine(LevelLoadCo());
            levelLoading = true;

            //SceneManager.LoadScene(levelName);
        }
        
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            canLoadLevel = true;

            LSUIManager.instance.INamePanel.SetActive(true);
            LSUIManager.instance.INameText.text = displayName;

            if(PlayerPrefs.HasKey(levelName + "_coins")){
                LSUIManager.instance.coinsText.text = PlayerPrefs.GetInt(levelName + "_coins").ToString();
            }else{
                LSUIManager.instance.coinsText.text = "???";
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player"){
            canLoadLevel = false;

            LSUIManager.instance.INamePanel.SetActive(false);
        }
    }

    public IEnumerator LevelLoadCo(){
        PlayerController.instance.stopMove = true;
        UIManager.instance.fadeToBlack = true;

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(levelName);
        PlayerPrefs.SetString("CurrentLevel",levelName);
    }
}
