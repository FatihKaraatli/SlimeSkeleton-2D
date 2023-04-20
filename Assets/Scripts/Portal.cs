using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject endGame;
    public GameObject[] allClosedObjects;

    private Camera cam;
    private GameObject player;
    private bool isPlayerInThePortal = false;
    private void Start()
    {
        cam = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerAttack>().enabled = false;
            cam.GetComponent<CameraFade>().enabled = true;
            isPlayerInThePortal = true;
        }
    }

    public void Update()
    {
        if (isPlayerInThePortal)
        {
            if (cam.GetComponent<CameraFade>()._alpha >= 1)
            {
                foreach (var item in allClosedObjects)
                {
                    item.SetActive(false);
                }
                cam.gameObject.SetActive(false);
                endGame.SetActive(true);
            }
        }
    }

}
