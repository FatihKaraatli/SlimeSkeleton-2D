using System.Collections;
using System.Collections.Generic;
using CountDownTimer;
using UnityEngine;
using UnityEngine.UI;

public class AttackCanvasControl : MonoBehaviour
{
    public Slider punchSlider;
    public Slider smallFireBallSlider;
    public Slider ultiSlider;
    public Slider dashSlider;
    public Slider defenceSlider;

    public bool punchSliderControl = false;
    public bool fireBallSliderControl = false;
    public bool ultiSliderControl = false;
    public bool dashSliderControl = false;
    public bool defenceSliderControl = false;

    public PlayerAttack playerAttack;
    public GameObject leftWall;
    public GameObject player;
    public float leftWallDistanceControl;

    private bool isCloseTheWall = false;
    private bool isFarTheWall = false;
    private Image[] skillUseBoardImages;
    private GameObject[] skillUseBoardObjects;
    //private float[] skillUseBoardImagesAlphas;

    public void Start()
    {
        punchSlider.maxValue = playerAttack.punchTimer.Remaining();
        smallFireBallSlider.maxValue = playerAttack.fireBulletDestinationTimer.Remaining();
        ultiSlider.maxValue = playerAttack.ultiDestinationTimer.Remaining();
        dashSlider.maxValue = playerAttack.dashTimer.Remaining();
        defenceSlider.maxValue = playerAttack.defenceTimer.Remaining();

        skillUseBoardObjects = GameObject.FindGameObjectsWithTag("SkillsUseBoard");
        skillUseBoardImages = new Image[skillUseBoardObjects.Length];
        for (int i = 0; i < skillUseBoardObjects.Length; i++)
        {
            skillUseBoardImages[i] = skillUseBoardObjects[i].GetComponent<Image>();
        }
    }

    void Update()
    {
        if (!punchSliderControl)
        {
            punchSlider.value = playerAttack.punchTimer.PassedTime();
        }

        if (!fireBallSliderControl)
        {
            smallFireBallSlider.value = playerAttack.fireBulletDestinationTimer.PassedTime();
        }
        if (!ultiSliderControl)
        {
            ultiSlider.value = playerAttack.ultiDestinationTimer.PassedTime();
        }
        if (!dashSliderControl)
        {
            dashSlider.value = playerAttack.dashTimer.PassedTime();
        }
        if (!defenceSliderControl)
        {
            defenceSlider.value = playerAttack.defenceTimer.PassedTime();
        }


        if (playerAttack.punchTimer.Done())
        {
            punchSliderControl = true;
        }
        if (playerAttack.fireBulletDestinationTimer.Done())
        {
            fireBallSliderControl = true;
        }
        if (playerAttack.ultiDestinationTimer.Done())
        {
            ultiSliderControl = true;
        }
        if (playerAttack.dashTimer.Done())
        {
            dashSliderControl = true;
        }
        if (playerAttack.defenceTimer.Done())
        {
            defenceSliderControl = true;
        }

        float distance = Mathf.Abs(player.transform.position.x - leftWall.transform.position.x);
        if (distance < leftWallDistanceControl && !isCloseTheWall && player.transform.position.y < 0)
        {
            isCloseTheWall = true;
            SkillBoardImagesAlphaDownControl();
        }
        else if(distance > leftWallDistanceControl && isFarTheWall && player.transform.position.y < 0)
        {
            isFarTheWall = false;
            SkillBoardImagesAlphaUpControl();
        }
    }

    public void SkillBoardImagesAlphaDownControl()
    {
        for (int i = 0; i < skillUseBoardImages.Length; i++)
        {
            skillUseBoardImages[i].color = new Color(skillUseBoardImages[i].color.r, skillUseBoardImages[i].color.g, skillUseBoardImages[i].color.b, skillUseBoardImages[i].color.a/4);
        }
        isFarTheWall = true;
    }
    public void SkillBoardImagesAlphaUpControl()
    {
        for (int i = 0; i < skillUseBoardImages.Length; i++)
        {
            skillUseBoardImages[i].color = new Color(skillUseBoardImages[i].color.r, skillUseBoardImages[i].color.g, skillUseBoardImages[i].color.b, skillUseBoardImages[i].color.a * 4);
        }
        isCloseTheWall = false;
    }
}
