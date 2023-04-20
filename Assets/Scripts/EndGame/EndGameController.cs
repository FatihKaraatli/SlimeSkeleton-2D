using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CountDownTimer;
using UnityEngine.SceneManagement;

public class EndGameController : MonoBehaviour
{
    public void RestartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
