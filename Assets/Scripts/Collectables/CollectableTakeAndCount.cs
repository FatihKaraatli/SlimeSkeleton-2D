using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CountDownTimer;

public class CollectableTakeAndCount : MonoBehaviour
{
    Transform dnaIconTransform;
    GameObject DnaCanvas;
    public Timer dnaToIconTimer;
    private bool hasDnaTaken = false;
    private Vector3 dnaTakenPos;
    DnaCount dnaCount;

    public AudioSource audioSourceEffects;
    public AudioClip audioClipEffects;

    void Start()
    {
        dnaIconTransform = GameObject.FindGameObjectWithTag("DNAIcon").transform;
        DnaCanvas = GameObject.FindGameObjectWithTag("Canvas");
        dnaCount = DnaCanvas.GetComponent<DnaCount>();
    }

    void Update()
    {
        if (hasDnaTaken)
        {
            dnaToIconTimer.UpdateTimer();
            transform.position = Vector3.Lerp(dnaTakenPos, dnaIconTransform.position, dnaToIconTimer.NormalizedTime);
            if (dnaToIconTimer.Done())
            {
                dnaCount.DnaCountIncrease();
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            audioSourceEffects.PlayOneShot(audioClipEffects);
            hasDnaTaken = true;
            dnaTakenPos = this.gameObject.transform.position;
        }
    }



}
