using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;

    public int deathSound;

    //敌人死亡粒子效果
    public GameObject deathEffect,itemToDrop;

    void Start()
    {
        currentHealth = maxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //杀敌人
    public void TakeDamage(){
        currentHealth--;
        if(currentHealth <= 0){
            AudioManager.instance.PlaySFX(deathSound);
            Destroy(gameObject);

            //修改杀敌人的效果
            PlayerController.instance.Bounce();

            //敌人死亡粒子效果
            Instantiate(deathEffect,transform.position + new Vector3(0,1.2f,0f), transform.rotation);
            Instantiate(itemToDrop,transform.position + new Vector3(0,.5f,0f), transform.rotation);
        }
    }
}
