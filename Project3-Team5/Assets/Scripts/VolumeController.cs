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
    [SerializeField] bool isPlayer = false;
    AudioManager audioMan;

    private void Awake()
    {
        audioMan = GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
           
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        ChangeVolume();
    }

    public void ChangeSFXVolume() //changes volume to slider value
    {
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
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
            //s.volume = s.volume * PlayerPrefs.GetFloat("SFXVolume") * 10;
            s.volume = PlayerPrefs.GetFloat("SFXVolume");           
        }
        if(isPlayer)
        {
            audioMan.ChangeVolume("Acceleration", PlayerPrefs.GetFloat("SFXVolume"));
            audioMan.ChangeVolume("Ambiance", PlayerPrefs.GetFloat("SFXVolume") / 2);
        }
        //print("Volume: " + PlayerPrefs.GetFloat("SFXVolume"));
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
