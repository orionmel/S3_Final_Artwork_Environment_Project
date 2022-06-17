using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int value;

    public GameObject pickEffect;

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
            GameManager.instance.AddCoins(value);
            Destroy(gameObject);

            //金币效果
            Instantiate(pickEffect,transform.position,transform.rotation);
            //添加SFX到物体上
            AudioManager.instance.PlaySFX(soundToPlay);
        }
    }
}
