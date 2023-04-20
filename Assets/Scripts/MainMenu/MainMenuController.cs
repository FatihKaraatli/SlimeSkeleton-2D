using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject settingsPanel;
    public GameObject soundOnButton;
    public GameObject soundOffButton;
    public GameObject musicOnButton;
    public GameObject musicOffButton;
    public AudioSource audioSourceMainMusic;
    public AudioSource audioSourceEffects;
    public AudioClip audioClipEffects;

    public GameObject enemieFloors;

    public void Awake()
    {
        PlayerPrefs.SetInt("music", 1);
        PlayerPrefs.SetInt("sound", 1);
    }

    public void PlayButton()
    {
        audioSourceEffects.PlayOneShot(audioClipEffects);
        SceneManager.LoadScene("SampleScene");
    }
    public void SettingsButton()
    {
        audioSourceEffects.PlayOneShot(audioClipEffects);
        enemieFloors.SetActive(false);
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
    public void SettingsPanelCloseButton()
    {
        audioSourceEffects.PlayOneShot(audioClipEffects);
        mainPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }
    public void SoundOn()
    {
        PlayerPrefs.SetInt("sound", 0);
        soundOnButton.SetActive(false);
        soundOffButton.SetActive(true);
    }
    public void SoundOff()
    {
        PlayerPrefs.SetInt("sound", 1);
        audioSourceEffects.PlayOneShot(audioClipEffects);
        soundOnButton.SetActive(true);
        soundOffButton.SetActive(false);
    }
    public void MusicOn()
    {
        PlayerPrefs.SetInt("music", 0);
        musicOnButton.SetActive(false);
        musicOffButton.SetActive(true);
    }
    public void MusicOff()
    {
        PlayerPrefs.SetInt("music", 1);
        audioSourceEffects.PlayOneShot(audioClipEffects);
        musicOnButton.SetActive(true);
        musicOffButton.SetActive(false);
    }
    public void ExitButton()
    {
        audioSourceEffects.PlayOneShot(audioClipEffects);
        Application.Quit();
    }

}
