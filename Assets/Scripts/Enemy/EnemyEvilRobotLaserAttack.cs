using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CountDownTimer;

public class EnemyEvilRobotLaserAttack : MonoBehaviour
{
    public GameObject bullet;
    public GameObject enemyHimself;
    public Transform spawnPoint;
    public float attackPower;
    public Timer timeBetweenTwoShoots;
    public Animator animator;

    public float gunAxis;

    private GameObject player;
    private int laserCount = 1;
    private bool laserMutipleAttack = false;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Update()
    {
        if (!enemyHimself.GetComponent<EnemyHealth>().isActiveAndEnabled)
        {
            animator.SetTrigger("Death");
            this.gameObject.GetComponent<EnemyEvilRobotLaserAttack>().enabled = false;
        }
        if (!player.GetComponent<PlayerHealth>().isActiveAndEnabled)
        {
            this.gameObject.GetComponent<EnemyEvilRobotLaserAttack>().enabled = false;
        }

        if (laserMutipleAttack)
        {
            timeBetweenTwoShoots.UpdateTimer();
            if (timeBetweenTwoShoots.Done())
            {
                EvilRobotLaserAttacking();
                laserCount--;
                timeBetweenTwoShoots.ResetTimer();
            }
            if (laserCount < 0)
            {
                laserCount = 1;
                laserMutipleAttack = false;
            }
        }


        Vector3 difference = this.transform.position - player.transform.position;
        float rotationZ = Mathf.Atan2(difference.y + gunAxis, difference.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
    }

    public void EvilRobotLaserAttacking()
    {
        GameObject laserBullet = Instantiate(bullet, spawnPoint.position, this.transform.rotation);
        BulletMovement tmp = laserBullet.GetComponent<BulletMovement>();
        tmp.movementDir = (int)(Mathf.Abs(this.transform.localScale.x) / this.transform.localScale.x) * -1;
        tmp.damage = attackPower;
        tmp.movVector = this.transform.position - player.transform.position;
        laserMutipleAttack = true;
    }
}
