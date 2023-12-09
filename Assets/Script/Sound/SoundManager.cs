using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    //배경음 관련 변수
    public AudioSource bgSound;
    public AudioClip bgClip;

    //효과음 관련 변수
    public AudioSource sfxSound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            BgSoundPlay(bgClip);
        }
        else if (instance == this)
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
        sfxSound.loop = true;
        sfxSound.volume = 1.0f;
        sfxSound.Play();
    }
}