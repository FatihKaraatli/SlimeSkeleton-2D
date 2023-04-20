using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTube : MonoBehaviour
{
    public float healthIncreaseAmount;
    public float flyPower;

    private Animator playerHealthTubeAnimator;
    private bool flyFromChestControl = true;
    private Rigidbody2D rb;

    public AudioSource audioSourceEffects;
    public AudioClip audioClipEffects;

    public void Start()
    {
        playerHealthTubeAnimator = GameObject.FindGameObjectWithTag("HealthTubeAnimOnPlayer").GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
    }
    public void FixedUpdate()
    {
        if (flyFromChestControl)
        {
            rb.AddForce((Vector2.up + Vector2.right) * flyPower * Time.fixedDeltaTime);
            flyFromChestControl = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetComponent<PlayerHealth>() != null && collision.GetComponent<PlayerHealth>().isActiveAndEnabled)
        {
            audioSourceEffects.PlayOneShot(audioClipEffects);

            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealthTubeAnimator.SetTrigger("Health");
                if (playerHealth.health + healthIncreaseAmount > playerHealth.lifeSlider.maxValue)
                {
                    playerHealth.lifeSlider.maxValue += healthIncreaseAmount;
                }
                playerHealth.health += healthIncreaseAmount;
                playerHealth.lifeSlider.value += healthIncreaseAmount;
            }

            this.gameObject.GetComponent<HealthTube>().enabled = false;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;

            Destroy(this.gameObject,1f);
        }
    }
}
