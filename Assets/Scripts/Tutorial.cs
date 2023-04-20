using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorialScreen;
    public GameObject firstFloorEnemies;
    public void CloseButton()
    {
        tutorialScreen.SetActive(false);
        firstFloorEnemies.SetActive(true);
    }
}
