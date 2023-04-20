using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public Animator animator;
    public Slider lifeSlider;
    public MonoBehaviour enemyMovementScript;//There are different types of Enemy Movement scripts
    public GameObject dnaCoin;
    public GameObject deadEnemy;

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
            GameObject fireBullet = Instantiate(dnaCoin, gameObject.transform.position, Quaternion.identity);
            GameObject dead = Instantiate(deadEnemy, gameObject.transform.position, Quaternion.identity);           
            Destroy(this.gameObject);
            /*this.gameObject.GetComponent<Collider2D>().enabled = false;
            lifeSlider.gameObject.SetActive(false);
            this.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            enemyMovementScript.enabled = false;
            this.gameObject.GetComponent<EnemyHealth>().enabled = false;
            this.gameObject.GetComponent<FadeEffect>().enabled = true;
            this.gameObject.GetComponent<ShadowCaster2D>().enabled = false;
            this.gameObject.transform.SetParent(null);
            this.gameObject.transform.GetChild(this.gameObject.transform.childCount - 1).gameObject.GetComponent<Light2D>().enabled = false;
            animator.SetTrigger("Death");*/
            return;
        }
        animator.SetTrigger("Hurt");
        lifeSlider.value = lifeSlider.value - damage;
        health -= damage;
        return;
    }
}
