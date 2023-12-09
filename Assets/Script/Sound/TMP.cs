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

        Debug.Log(bgmValue + ", " + sfxValue);
    }

    private void OnEnable()
    {
        float bgmValue;
        myMixer.GetFloat("BGM", out bgmValue);
        bgmAudioSlider.value = bgmValue;

        float sfxValue;
        myMixer.GetFloat("SFX", out sfxValue);
        sfxAudioSlider.value = sfxValue;

        Debug.Log(bgmValue + ", " + sfxValue);
    }

    public void BgmAudioControl()
    {
        float sound = bgmAudioSlider.value;

        if (sound == -40f) myMixer.SetFloat("BGM", -80);
        else myMixer.SetFloat("BGM", sound);
    }

    public void SfxAudioControl()
    {
        float sound = bgmAudioSlider.value;

        if (sound == -40f) myMixer.SetFloat("SFX", -80);
        else myMixer.SetFloat("SFX", sound);
    }

    public void ToggleAudioVolume()
    {
        AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
    }
}
