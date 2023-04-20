using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CountDownTimer;

public class DuckCode : MonoBehaviour
{
    public GameObject slimeBullet;
    public GameObject duckSlime;
    public PlayerAttack playerAttack;
    public Timer duckTimer;

    private int counter = 0;
    private int tmp = 0;
    private bool isDuck = false;

    KeyCode[] cheatKod = null;
    void Start()
    {
        cheatKod = new KeyCode[10];

        cheatKod[0] = KeyCode.UpArrow;
        cheatKod[1] = KeyCode.UpArrow;
        cheatKod[2] = KeyCode.DownArrow;
        cheatKod[3] = KeyCode.DownArrow;
        cheatKod[4] = KeyCode.LeftArrow;
        cheatKod[5] = KeyCode.RightArrow;
        cheatKod[6] = KeyCode.LeftArrow;
        cheatKod[7] = KeyCode.RightArrow;
        cheatKod[8] = KeyCode.B;
        cheatKod[9] = KeyCode.A;
    }

    void Update()
    {
        if (isDuck)
        {
            duckTimer.UpdateTimer();
            if (duckTimer.Done())
            {
                duckTimer.ResetTimer();
                playerAttack.fireBallPrefab = slimeBullet;
                isDuck = false;
            }
        }       

        if (counter == cheatKod.Length)
        {
            counter = 0;
            isDuck = true;
            playerAttack.fireBallPrefab = duckSlime;
        }
    }

    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey && Event.current.type == EventType.KeyDown && tmp == 0)
        {
            tmp = 1;
        }
        if (e.isKey && counter < cheatKod.Length && cheatKod[counter] == e.keyCode && Event.current.type == EventType.KeyUp && tmp == 1)
        {
            tmp = 0;
            counter++;
        } 
        if (e.isKey && counter < cheatKod.Length && cheatKod[counter] != e.keyCode && Event.current.type == EventType.KeyUp && tmp == 1)
        {
            tmp = 0;
            counter = 0;
        }
    }
}
