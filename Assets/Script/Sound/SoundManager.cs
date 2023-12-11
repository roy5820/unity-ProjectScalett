using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    //배경음 관련 변수
    public AudioSource bgSound;
    public AudioClip bgClip;

    //효과음 관련 변수
    public AudioSource sfxSound;

    //현재 오디오 믹서
    public AudioMixer myMixer;

    private void Start()
    {
        //기존의 음향값 반영
        float bgmValue = PlayerPrefs.GetFloat("BGM");
        myMixer.SetFloat("BGM", bgmValue);

        float sfxValue = PlayerPrefs.GetFloat("SFX");
        myMixer.SetFloat("SFX", sfxValue);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            BgSoundPlay(bgClip);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    
    public void BgSoundPlay(AudioClip clip)
    {
        bgSound.clip = clip;
        bgSound.loop = true;
        bgSound.volume = 1.0f;
        bgSound.Play();
    }

    public void SfxSoundPlay(AudioClip clip)
    {
        sfxSound.clip = clip;
        sfxSound.loop = false;
        sfxSound.volume = 1.0f;
        sfxSound.Play();
    }
}