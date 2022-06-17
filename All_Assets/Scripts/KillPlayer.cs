using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    //引用GM
    public GameManager theGM;

    public int soundToPlay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            //引用GM里面的私有函数
            GameManager.instance.Respawn();

            //添加SFX到物体上
            AudioManager.instance.PlaySFX(soundToPlay);

        }
        
    }
}
