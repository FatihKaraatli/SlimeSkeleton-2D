using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CountDownTimer;

public class EnemyLavaMonsterAttack : MonoBehaviour
{
    public GameObject bullet;
    public GameObject enemyHimself;
    public Transform spawnPoint;
    public float attackPower;
    public Timer timeBetweenTwoShoots;
    public Animator animator;

    public float gunAxis;

    private GameObject player;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Update()
    {
        if (!enemyHimself.GetComponent<EnemyHealth>().isActiveAndEnabled)
        {
            animator.SetTrigger("Death");
            this.gameObject.GetComponent<EnemyLavaMonsterAttack>().enabled = false;
        }
        if (!player.GetComponent<PlayerHealth>().isActiveAndEnabled)
        {
            this.gameObject.GetComponent<EnemyLavaMonsterAttack>().enabled = false;
        }

        Vector3 difference = this.transform.position - player.transform.position;
        float rotationZ = Mathf.Atan2(difference.y + gunAxis, difference.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
    }

    public void LavaMonsterLavaAttacking()
    {
        GameObject lavaBullet = Instantiate(bullet, spawnPoint.position, this.transform.rotation);
        BulletMovement tmp = lavaBullet.GetComponent<BulletMovement>();
        tmp.transform.localScale = tmp.transform.localScale * -1;
        tmp.movementDir = (int)(Mathf.Abs(this.transform.localScale.x) / this.transform.localScale.x) * -1;
        tmp.damage = attackPower;
        tmp.movVector = this.transform.position - player.transform.position;
    }
}
