using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetBgm : MonoBehaviour
{
    public AudioClip bgm;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.BgSoundPlay(bgm);
    }
}
