using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public Animator animator;
    public Slider lifeSlider;
    public float teleportHealthAddAmount;

    public GameObject gameOverCanvas;

    public void Start()
    {
        lifeSlider.maxValue = health;
        lifeSlider.value = lifeSlider.maxValue;
    }

    public void DecraseHealth(float damage)
    {
        if (health - damage <= 0)
        {
            health = 0;
            lifeSlider.value = 0;
            animator.SetTrigger("Death");
            this.gameObject.GetComponent<PlayerMovement>().enabled = false;
            this.gameObject.GetComponent<PlayerHealth>().enabled = false;
            this.gameObject.GetComponent<PlayerAttack>().enabled = false;
            gameOverCanvas.SetActive(true);
            return;
        }
        animator.SetTrigger("Hurt");
        lifeSlider.value = lifeSlider.value - damage;
        health -= damage;
        return;
    }

    public void AddHealth()
    {
        if (lifeSlider.maxValue >= health + teleportHealthAddAmount)
        {
            lifeSlider.value = lifeSlider.value + teleportHealthAddAmount;
            health += teleportHealthAddAmount;
            return;
        }
        health = lifeSlider.maxValue;
        lifeSlider.value = health;
    }

}
