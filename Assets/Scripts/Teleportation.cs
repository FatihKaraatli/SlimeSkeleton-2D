using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CountDownTimer;

public class Teleportation : MonoBehaviour
{
    public GameObject player;
    public Transform destinationPoint;
    public Transform destinationHalfWayPoint;
    public Timer halfWayDestinationTimer;
    public Sprite teleportationSprite;
    public GameObject teleportationLightCapsule;
    public AudioSource audioSource;
    public AudioClip teleportationSoundClip;

    public GameObject darkEffect;
    public GameObject currentFloorLights;

    private bool canTeleportFirstHalfWay = false;
    private bool halfwayControl = false;
    private Vector3 playerStartScale;
    private Vector3 playerHalfWayScale;
    private Vector3 playerStartPos;

    
    void Update()
    {
        if (canTeleportFirstHalfWay)
        {
            halfWayDestinationTimer.UpdateTimer();
            player.transform.localScale = Vector3.Lerp(playerStartScale, playerHalfWayScale,halfWayDestinationTimer.NormalizedTime);
            player.transform.position = Vector3.Lerp(playerStartPos, destinationHalfWayPoint.position, halfWayDestinationTimer.NormalizedTime);
            if (halfWayDestinationTimer.Done())
            {
                canTeleportFirstHalfWay = false;
                halfwayControl = true;
                halfWayDestinationTimer.ResetTimer();
            }
        }
        if (halfwayControl)
        {
            halfWayDestinationTimer.UpdateTimer();
            player.transform.localScale = Vector3.Lerp(playerHalfWayScale, playerStartScale, halfWayDestinationTimer.NormalizedTime);
            player.transform.position = Vector3.Lerp(destinationHalfWayPoint.position, destinationPoint.position, halfWayDestinationTimer.NormalizedTime);
            if (halfWayDestinationTimer.Done())
            {
                player.GetComponent<SpriteRenderer>().sortingOrder = 6;
                player.GetComponent<PlayerMovement>().enabled = true;
                player.GetComponent<Animator>().enabled = true;
                player.GetComponent<PlayerHealth>().AddHealth();
                teleportationLightCapsule.SetActive(false);
                halfwayControl = false;
                halfWayDestinationTimer.ResetTimer();
            }
        }
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !canTeleportFirstHalfWay)
        {
            darkEffect.GetComponent<FadeEffect>().enabled = true;
            currentFloorLights.SetActive(true);
            audioSource.PlayOneShot(teleportationSoundClip);
            SpriteRenderer spriteRenderer = other.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = teleportationSprite;
            spriteRenderer.sortingOrder = 3;
            teleportationLightCapsule.SetActive(true);
            playerStartPos = other.transform.position;
            playerStartScale = player.transform.localScale;
            playerHalfWayScale = new Vector3(player.transform.localScale.x / 2, player.transform.localScale.y * 1.5f, player.transform.localScale.z);
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            other.GetComponent<PlayerMovement>().enabled = false;
            other.GetComponent<Animator>().enabled = false;
            canTeleportFirstHalfWay = true;
        }
    }
}
