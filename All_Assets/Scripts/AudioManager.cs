using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource[] music;
    public AudioSource[] sfx;

    //选择第几个音乐
    public int levelMusicToPlay;

    //做music滑块UI
    public AudioMixerGroup musicMixer,sfxMixer;
    

    //private int currentTrack;

    private void Awake() {
        instance = this;
    }

    void Start()
    {
        PlayMusic(levelMusicToPlay);
    }

    // Update is called once per frame
    void Update()
    {
        //用按键控制声音
        // if(Input.GetKeyDown(KeyCode.M)){
        //     currentTrack++;
        //     PlayMusic(currentTrack);

        //     PlaySFX(6);
        // }
        
    }
    
    public void PlayMusic(int musicToPlay){
        //创造循环播放背景乐
        for(int i =0;i<music.Length;i++){
            music[i].Stop();
        }
        music[musicToPlay].Play();
    }
    public void PlaySFX(int sfxToPlay){
        sfx[sfxToPlay].Play();
    }

    //设置UI滑块控制音乐声音大小
    public void SetMusicLevel(){
        musicMixer.audioMixer.SetFloat("MusicVol",UIManager.instance.musicVolSlider.value);

    }
    public void SetSFXLevel(){
        sfxMixer.audioMixer.SetFloat("SfxVol",UIManager.instance.sfxVolSlider.value);

    }

}
