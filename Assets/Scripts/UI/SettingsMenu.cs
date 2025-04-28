using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    #region Variables
    public TMP_Dropdown graphicsDropdown, windowDropdown;
    public Slider MasterVolumeSlider, MusicVolumeSlider, SFXVolumeSlider, AmbienceVolumeSlider;
    public AudioMixer audioMixer;
    #endregion

    #region Graphics
    public void SetGraphicsQuality()
    {
        QualitySettings.SetQualityLevel(graphicsDropdown.value);
    }
    #endregion

    #region Window
    public void SetWindowMode()
    {
        switch (windowDropdown.value)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
        }
    }
    #endregion


    #region Volume
    public void ChangeMasterVolume()
    {
        audioMixer.SetFloat("MasterVolume", MasterVolumeSlider.value);
    }

    public void ChangeSFXVolume()
    {
        audioMixer.SetFloat("SFXVolume", SFXVolumeSlider.value);
    }

    public void ChangeMusicVolume()
    {
        audioMixer.SetFloat("MusicVolume", MusicVolumeSlider.value);
    }

    public void ChangeAmbienceVolume()
    {
        audioMixer.SetFloat("AmbienceVolume", AmbienceVolumeSlider.value);
    }
    #endregion
}
