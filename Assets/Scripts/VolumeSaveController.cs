using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class VolumeSaveController : MonoBehaviour
{
    [SerializeField] private Slider musicSlider = null;
    [SerializeField] private Slider sfxSlider = null;

    [SerializeField] private TextMeshProUGUI musicText = null;
    [SerializeField] private TextMeshProUGUI sfxText = null;


    private void Start()
    {

        LoadMusicValue();
        LoadSfxValue();
    }

    public void MusicSlider(float volume)
    {
        musicText.SetText(volume.ToString());
        AudioManager.Instance.musicSource.volume = volume * 0.1f;

    }
    public void SfxSlider(float volume)
    {
        sfxText.SetText(volume.ToString());
        AudioManager.Instance.sfxSource.volume = volume * 0.1f;
        
    }
    public void SaveMusicVolume()
    {
        float volumeValue = musicSlider.value;
        PlayerPrefs.SetFloat("MusicValue", volumeValue);
        LoadMusicValue();
    }
    public void SaveSfxValue()
    {
        float volumeValue = sfxSlider.value;
        PlayerPrefs.SetFloat("SfxValue", volumeValue);
        LoadSfxValue();
    }

    private void LoadMusicValue()
    {
        float volumeValue = PlayerPrefs.GetFloat("MusicValue");
        musicSlider.value = volumeValue;
        AudioManager.Instance.musicSource.volume = volumeValue * 0.1f;
    }
    private void LoadSfxValue()
    {
        float volumeValue = PlayerPrefs.GetFloat("SfxValue");
        sfxSlider.value = volumeValue;
        AudioManager.Instance.sfxSource.volume = volumeValue * 0.1f;
    }

 
    
}
