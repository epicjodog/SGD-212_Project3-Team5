using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Changes volumes of SFX and Music using volume sliders (requires music and SFX to be in the same AudioManager)
/// </summary>
public class VolumeController : MonoBehaviour
{
    [Header("Volume Sliders")]
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider musicSlider;
    AudioManager audioMan;

    // Start is called before the first frame update
    void Start()
    {
        audioMan = GetComponent<AudioManager>();
        ChangeVolume();
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        
    }

    public void ChangeSFXVolume() //changes volume to slider value
    {
        //AudioListener.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);

        /*if (volumeSlider.value == 0)
        {
            volumeIcon.sprite = speakerIcons[1];
        }
        else
        {
            volumeIcon.sprite = speakerIcons[0];
        }*/
        ChangeVolume();
    }
    public void ChangeMusicVolume()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        ChangeVolume();
    }
    void ChangeVolume()
    {
        foreach (Sound s in audioMan.sounds)
        {
            s.volume = PlayerPrefs.GetFloat("SFXVolume");
        }
        audioMan.ChangeVolume("Music", PlayerPrefs.GetFloat("MusicVolume"));
    }

    public void OnMuteSFXButtonClick()
    {
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        ChangeVolume();
        sfxSlider.value = 0;
    }
    public void OnMuteMusicButtonClick()
    {
        PlayerPrefs.SetFloat("MusicVolume", 0);
        ChangeVolume();
        musicSlider.value = 0;
    }
}
