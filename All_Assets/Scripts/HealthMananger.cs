using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMananger : MonoBehaviour
{
    public static HealthMananger instance;
    public int currentHealth,maxHealth;

    public float invincibleLength = 2f;
    private float invincCounter;
    public Sprite[]healthBarImages;


    private void Awake() {
        instance = this;
    }
    void Start()
    {
        ResetHealth();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(invincCounter >0 ){
            invincCounter -= Time.deltaTime;

            for(int i = 0;i<PlayerController.instance.playerPieces.Length;i++)
                {
                    if(Mathf.Floor(invincCounter * 5f)%2 == 0){
                        PlayerController.instance.playerPieces[i].SetActive(true);
                    }
                    else
                    {
                        PlayerController.instance.playerPieces[i].SetActive(false);
                    }
                    
                    if(invincCounter <= 0){
                        PlayerController.instance.playerPieces[i].SetActive(true);
                    }
                }
        }
            
        
    }

    //触发重生
    public void Hurt(){
        if(invincCounter <= 0){
            currentHealth--;
            if(currentHealth <= 0){
                currentHealth = 0;
                GameManager.instance.Respawn();
            }else{
                PlayerController.instance.knockback();
                invincCounter = invincibleLength;

                // for(int i = 0;i<PlayerController.instance.playerPieces.Length;i++){
                //     PlayerController.instance.playerPieces[i].SetActive(false);
                // }
             }
             UpdatedUI();
        }
        
    }

    //重新获得生命数值
    public void ResetHealth(){
        currentHealth = maxHealth;
        UIManager.instance.healthImage.enabled = true;
        UpdatedUI();
    }

    public void AddHealth(int amountToHeal){
        currentHealth += amountToHeal;
        if(currentHealth > maxHealth){
            currentHealth = maxHealth;
        }
        UpdatedUI();
    }

    //生命条变化
    public void UpdatedUI(){
        UIManager.instance.healthText.text = currentHealth.ToString();
        //生命条图片交替变化
        switch(currentHealth){
            case 5:
                UIManager.instance.healthImage.sprite = healthBarImages[4];
                break;
            case 4:
                UIManager.instance.healthImage.sprite = healthBarImages[3];
                break;
            case 3:
                UIManager.instance.healthImage.sprite = healthBarImages[2];
                break;
            case 2:
                UIManager.instance.healthImage.sprite = healthBarImages[1];
                break;
            case 1:
                UIManager.instance.healthImage.sprite = healthBarImages[0];
                break;
            case 0:
                UIManager.instance.healthImage.enabled = false;
                break;
        }
    }

    //重新生成生命条
    public void PlayerKilled(){
        currentHealth = 0;
        UpdatedUI();
    }
}
