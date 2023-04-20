using System;
using System.Collections;
using System.Collections.Generic;
using CountDownTimer;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;

    public float playerFireBulletAttackPower;
    public float playerUltiAttackPower;
    public float playerPunchAttackPower;

    public LayerMask layer;

    public GameObject fireBallPrefab;
    public GameObject ultiPrefab;
    public Transform throwableObjectsStartPos;
    public Timer fireBulletDestinationTimer;
    public Timer ultiDestinationTimer;
    public Timer punchTimer;

    private GameObject fireBullet;
    private GameObject ultiBullet;

    private Vector2 dashStartPos;
    private bool dashControl = false;
    private float dashDirection;
    private float dashDestination;
    [HideInInspector]public int moveStop = 1;
    public LayerMask dashLayer;
    public Timer defenceTimer;
    public Timer dashTimer;
    public Timer dashTrailCrateTimer;

    public GameObject dashTrailSprite;
    private List<GameObject> dashTrailList;

    public AttackCanvasControl attackCanvasControl;
    public PlayerMovement playerMovement;

    public bool isDefencing = false;

    public AudioSource audioSourceEffects;
    public AudioClip audioClipPunchEffects;
    public AudioClip audioClipWhooshEffects;
    public AudioClip audioClipDashEffect;

    public AudioSource audioSourceShieldEffects;
    public AudioClip audioClipShieldEffects;

    public AudioSource audioSourceDashEffects;
    public void Start()
    {
        dashTrailList = new List<GameObject>();
    }
    void Update()
    {
        AnimatorClipInfo[] a = animator.GetCurrentAnimatorClipInfo(0);
        if (a[0].clip.name == "Player_DefenceAnim")
        {
            isDefencing = true;
        }
        else
        {
            isDefencing = false;
        }

        Debug.DrawRay(transform.position, transform.right * 25f * transform.localScale.x, Color.red);
        
        fireBulletDestinationTimer.UpdateTimer();
        ultiDestinationTimer.UpdateTimer();
        punchTimer.UpdateTimer();
        defenceTimer.UpdateTimer();
        dashTimer.UpdateTimer();

        if (Input.GetKeyDown(KeyCode.C) && fireBulletDestinationTimer.Done())
        {
            FireBall();
        }
        if (Input.GetKeyDown(KeyCode.X) && ultiDestinationTimer.Done())
        {
            Ulti();
        }
        if (Input.GetKeyDown(KeyCode.E) && punchTimer.Done() && !playerMovement.jump && playerMovement.horizontalMove == 0)
        {
            Punch();
        }
        if (Input.GetKeyDown(KeyCode.Q) && defenceTimer.Done() && !playerMovement.jump && playerMovement.horizontalMove == 0)
        {
            Defence();
        }
        if (Input.GetKeyDown(KeyCode.W) && !dashControl && playerMovement.horizontalMove != 0)
        {
            RaycastHit2D[] hit2 = Physics2D.RaycastAll(transform.position, transform.right * transform.localScale.x, 25f, layer);
            {
                Dash(hit2);
            }
        }

        if (dashControl)
        {
            dashTimer.UpdateTimer();
            dashTrailCrateTimer.UpdateTimer();

            transform.position = Vector3.Lerp(dashStartPos, new Vector3(dashDestination, transform.position.y, 0), dashTimer.NormalizedTime);
            if (dashTrailCrateTimer.Done())
            {
                GameObject trail = Instantiate(dashTrailSprite, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
                dashTrailList.Add(trail);
                dashTrailCrateTimer.ResetTimer();
            }
            if (dashTimer.Done())
            {
                dashControl = false;
                moveStop = 1;
                dashTimer.ResetTimer();
            }
        }

        if (!dashControl)
        {
            if (dashTrailList.Count > 0)
            {
                dashTrailCrateTimer.UpdateTimer();
                if (dashTrailCrateTimer.Done())
                {
                    GameObject tmp = dashTrailList[0];
                    dashTrailList.RemoveAt(0);
                    Destroy(tmp);
                    dashTrailCrateTimer.ResetTimer();
                }
            }
        }
    }
    public void PlayerAttacking()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right * transform.localScale.x, 4f, layer);
        if (hit.collider != null )
        {
            GameObject obj = hit.collider.gameObject;
            if (obj.GetComponent<EnemyHealth>() != null)
            {
                audioSourceEffects.PlayOneShot(audioClipPunchEffects);
                obj.GetComponent<EnemyHealth>().DecraseHealth(playerPunchAttackPower);
            }
            else if(obj.CompareTag("Chest"))
            {
                obj.GetComponent<AudioSource>().Play();
                obj.GetComponent<HealthChest>().openControl = true;
            }
        }
        else
        {
            audioSourceEffects.PlayOneShot(audioClipWhooshEffects);
        }
    }

    public void FireBall()
    {
        fireBullet = Instantiate(fireBallPrefab, throwableObjectsStartPos.position,Quaternion.identity);
        BulletMovement tmp = fireBullet.GetComponent<BulletMovement>();
        tmp.movementDir = (int)(Mathf.Abs(transform.localScale.x)/ transform.localScale.x);
        tmp.transform.localScale = new Vector2(((int)(Mathf.Abs(transform.localScale.x) / transform.localScale.x)) * fireBullet.GetComponent<BulletMovement>().transform.localScale.x, fireBullet.GetComponent<BulletMovement>().transform.localScale.y);
        tmp.damage = playerFireBulletAttackPower;
        fireBulletDestinationTimer.ResetTimer();
        attackCanvasControl.fireBallSliderControl = false;
        attackCanvasControl.smallFireBallSlider.maxValue = fireBulletDestinationTimer.GetTarget();
        attackCanvasControl.smallFireBallSlider.value = 0;
    }
    public void Ulti()
    {
        var spawnPos = throwableObjectsStartPos.position + new Vector3(0,0.5f,0);
        ultiBullet = Instantiate(ultiPrefab, spawnPos, Quaternion.identity);
        BulletMovement tmp = ultiBullet.GetComponent<BulletMovement>();
        tmp.movementDir = (int)(Mathf.Abs(transform.localScale.x)/ transform.localScale.x);
        tmp.transform.localScale = new Vector2(((int)(Mathf.Abs(transform.localScale.x) / transform.localScale.x)) * ultiBullet.GetComponent<BulletMovement>().transform.localScale.x, ultiBullet.GetComponent<BulletMovement>().transform.localScale.y);
        tmp.damage = playerUltiAttackPower;
        ultiDestinationTimer.ResetTimer();
        attackCanvasControl.ultiSliderControl = false;
        attackCanvasControl.ultiSlider.maxValue = ultiDestinationTimer.GetTarget();
        attackCanvasControl.ultiSlider.value = 0;
    }
    public void Punch()
    {
        attackCanvasControl.punchSliderControl = false;
        attackCanvasControl.punchSlider.maxValue = punchTimer.GetTarget();
        attackCanvasControl.punchSlider.value = 0;
        playerMovement.animator.SetTrigger("Attack");
        punchTimer.ResetTimer();
    }
    public void Dash(RaycastHit2D[] dashHitObject)
    {
        audioSourceDashEffects.PlayOneShot(audioClipDashEffect);

        attackCanvasControl.dashSliderControl = false;
        attackCanvasControl.dashSlider.maxValue = dashTimer.GetTarget();
        attackCanvasControl.dashSlider.value = 0;
        dashDirection = playerMovement.horizontalMove / playerMovement.runSpeed;
        bool canPlayerFullDash = true;
        GameObject destinationObj = null;
        foreach (var hit in dashHitObject)
        {
            GameObject hittedObj = hit.collider.gameObject;
            if (hittedObj == null || hittedObj.CompareTag("Enemy") || hittedObj.CompareTag("DNA") || hittedObj.CompareTag("Bullet"))
            {
                continue;
            }
            else
            {
                canPlayerFullDash = false;
                destinationObj = hit.collider.gameObject;
                break;
            }
        }
        if (canPlayerFullDash == true)
        {
            dashDestination = transform.position.x + (dashDirection * 20);
        }
        else
        {
            if (this.gameObject.transform.position.x - destinationObj.transform.position.x > 0)
                dashDestination = destinationObj.transform.position.x + 1;
            else
                dashDestination = destinationObj.transform.position.x - 1;
        }
        dashStartPos = transform.position;
        moveStop = 0;
        dashControl = true;
        dashTimer.ResetTimer();
    }
    public void Defence()
    {
        audioSourceShieldEffects.PlayOneShot(audioClipShieldEffects);

        playerMovement.animator.SetTrigger("Defence");
        attackCanvasControl.defenceSliderControl = false;
        attackCanvasControl.defenceSlider.maxValue = defenceTimer.GetTarget();
        attackCanvasControl.defenceSlider.value = 0;
        defenceTimer.ResetTimer();
    }

    public void DefenceStart()
    {
        
    }
    public void DefenceEnd()
    {
        Debug.Log("defence end");
    }
}
