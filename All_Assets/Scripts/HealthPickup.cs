using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount;
    public bool isFullHeal;

     //死亡的爱心粒子效果
    public GameObject HeartDeathEffect;

    public int soundToPlay;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            Destroy(gameObject);

            if(isFullHeal){
                HealthMananger.instance.ResetHealth();
            }else
            {
                HealthMananger.instance.AddHealth(healAmount);
            }

            Instantiate(HeartDeathEffect,transform.position,transform.rotation);

            //添加SFX到物体上
            AudioManager.instance.PlaySFX(soundToPlay);
        }
    }
}
