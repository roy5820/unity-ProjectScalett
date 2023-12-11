using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    //����� ���� ����
    public AudioSource bgSound;
    public AudioClip bgClip;

    //ȿ���� ���� ����
    public AudioSource sfxSound;

    //���� ����� �ͼ�
    public AudioMixer myMixer;

    private void Start()
    {
        //������ ���Ⱚ �ݿ�
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