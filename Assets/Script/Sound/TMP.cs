using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Audio;
using UnityEngine.UI;
public class TMP : MonoBehaviour
{
    public AudioMixer myMixer;
    public Slider bgmAudioSlider;
    public Slider sfxAudioSlider;

    private void Awake()
    {
        float bgmValue;
        myMixer.GetFloat("BGM", out bgmValue);
        bgmAudioSlider.value = bgmValue;

        float sfxValue;
        myMixer.GetFloat("SFX", out sfxValue);
        sfxAudioSlider.value = sfxValue;
    }

    public void BgmAudioControl()
    {
        float sound = bgmAudioSlider.value;

        if (sound == -40f)
            sound = -80f;

        PlayerPrefs.SetFloat("BGM", sound);
        myMixer.SetFloat("BGM", sound);
    }

    public void SfxAudioControl()
    {
        float sound = sfxAudioSlider.value;

        if (sound == -40f)
            sound = -80f;

        PlayerPrefs.SetFloat("SFX", sound);
        myMixer.SetFloat("SFX", sound);
    }

    public void ToggleAudioVolume()
    {
        AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
    }
}
