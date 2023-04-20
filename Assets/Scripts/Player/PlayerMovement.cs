using System;
using System.Collections;
using System.Collections.Generic;
using CountDownTimer;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40f;

    [HideInInspector] public float horizontalMove = 0f;
    [HideInInspector] public bool jump = false;

    public PlayerAttack playerAttack;

    public Light2D spriteLight;
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        spriteLight.lightCookieSprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;
        if (Input.GetButtonDown("JumpEnes"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }
    }
    
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime * playerAttack.moveStop, false, jump);
        jump = false;
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }
}
