using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CountDownTimer;

public class HealthChest : MonoBehaviour
{
    public Animator animator;
    public Timer chestOpenCloseTimer;
    public GameObject healthTube;
    public Transform healthTubeSpawnPoint;

    public AudioSource audioSourceEffects;
    public AudioClip audioClipEffects;

    public bool openControl = false;
    private bool closeControl = false;

    public void Update()
    {
        if (openControl)
        {
            //audioSourceEffects.PlayOneShot(audioClipEffects);

            animator.SetTrigger("Open");
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            chestOpenCloseTimer.UpdateTimer();
            if (chestOpenCloseTimer.Done())
            {
                Instantiate(healthTube, healthTubeSpawnPoint.position, Quaternion.identity);
                openControl = false;
                closeControl = true;
                chestOpenCloseTimer.ResetTimer();
            }
        }
        else if (closeControl)
        {
            animator.SetTrigger("Close");
            chestOpenCloseTimer.UpdateTimer();
            if (chestOpenCloseTimer.Done())
            {
                closeControl = false;
                this.gameObject.GetComponent<FadeEffect>().enabled = true;
            }
        }
    }
}
