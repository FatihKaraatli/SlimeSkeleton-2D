using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DnaCount : MonoBehaviour
{
    public int count = 0;
    public TMP_Text dnaCountText;



    public void DnaCountIncrease()
    {
        PlayerPrefs.SetInt("DnaCount", ++count);
    }
    public void Update()
    {
        dnaCountText.text = PlayerPrefs.GetInt("DnaCount").ToString();
    }
}
