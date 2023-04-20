using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float damage;
    public int movementDir;
    public float speed;
    public Vector3 movVector = new Vector3(1,0,0);
    public GameObject bulletImpact;
    public LayerMask layer;
    public bool playerHasThisBullet;
    public AudioSource audioSource;
    public AudioClip ricochetClip;

    private int count = 3;
    private Vector3 directionVector;
    public void Start()
    {
        directionVector = movVector * movementDir * speed;
    }
    public void Update()
    {
        transform.position += directionVector * Time.deltaTime;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (playerHasThisBullet)
        {
            if (other.GetComponent<EnemyHealth>() && other.GetComponent<EnemyHealth>().isActiveAndEnabled)
            {
                Instantiate(bulletImpact , other.ClosestPoint(transform.position), Quaternion.identity);
                other.GetComponent<EnemyHealth>().DecraseHealth(damage);
                if (!this.gameObject.name.Contains("UltiBullet")) 
                {
                    other.GetComponent<EnemyHealth>().DecraseHealth(damage);
                    Destroy(this.gameObject);
                }
                else
                {
                    if (count > 0)
                    {
                        other.GetComponent<EnemyHealth>().DecraseHealth(damage);
                        damage -= 10;
                        count--;
                        if (count == 0)
                        {
                            Destroy(this.gameObject);
                        }
                    }
                }
            }
        }
        else
        {
            if (other.GetComponent<PlayerHealth>() && other.GetComponent<PlayerHealth>().isActiveAndEnabled)
            {
                if (other.GetComponent<PlayerAttack>().isDefencing)
                {
                    audioSource.clip = ricochetClip;
                    audioSource.Play();
                    directionVector *= -1;
                    playerHasThisBullet = true;
                    Vector3 scale = this.gameObject.transform.localScale;
                    this.gameObject.transform.localScale = new Vector3(scale.x * -1, scale.y, scale.z);
                }
                else
                {
                    Instantiate(bulletImpact, other.ClosestPoint(transform.position), Quaternion.identity);
                    other.GetComponent<PlayerHealth>().DecraseHealth(damage);
                    Destroy(this.gameObject);
                }
            }
        }
        if (other.CompareTag("Wall"))
        {
            Instantiate(bulletImpact, other.ClosestPoint(transform.position), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
