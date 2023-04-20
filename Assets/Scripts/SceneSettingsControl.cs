using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSettingsControl : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject soundOnButton;
    public GameObject soundOffButton;
    public GameObject musicOnButton;
    public GameObject musicOffButton;

    public void Awake()
    {
        if (PlayerPrefs.GetInt("sound") == 0) SoundOn();
        else SoundOff();
        if (PlayerPrefs.GetInt("music") == 0) MusicOn();
        else MusicOff();
    }
    public void SettingsButton()
    {
        settingsPanel.SetActive(true);
    }
    public void SettingsPanelCloseButton()
    {
        settingsPanel.SetActive(false);
    }
    public void SoundOn()
    {
        soundOnButton.SetActive(false);
        soundOffButton.SetActive(true);
        PlayerPrefs.SetInt("sound", 0);
    }
    public void SoundOff()
    {
        soundOnButton.SetActive(true);
        soundOffButton.SetActive(false);
        PlayerPrefs.SetInt("sound", 1);
    }
    public void MusicOn()
    {
        musicOnButton.SetActive(false);
        musicOffButton.SetActive(true);
        PlayerPrefs.SetInt("music", 0);
    }
    public void MusicOff()
    {
        musicOnButton.SetActive(true);
        musicOffButton.SetActive(false);
        PlayerPrefs.SetInt("music", 1);
    }
    public void RestartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
